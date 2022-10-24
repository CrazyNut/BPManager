using API.Entities;
using API.Interfaces;
using API.ProcessExecutor.Interfaces;
using API.ProcessInstance.Objects;
using API.ProcessInstance.ProcessElements;
using API.UnitsOfWork;
using AutoMapper;
using Microsoft.Extensions.Localization;
using System.Diagnostics;
using static API.ProcessInstance.Objects.ProcessInstanceContext;

namespace API.Services
{
    public class ProcessExecutorService : IProcessExecutorService
    {
        private readonly AppUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IProcessElementTypesService _processElementTypesService;
        private readonly IMessagerService _messagerService;
        private static readonly Dictionary<int, ProcessInstance.ProcessInstance> _processInstances = new();
        private static readonly Dictionary<int, UserProcessInstanses> _processInstanceStatuses = new();
        public ProcessExecutorService(AppUnitOfWork unitOfWork,
            IMapper mapper,
            IProcessElementTypesService processElementTypesService,
            IMessagerService messagerService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _processElementTypesService = processElementTypesService;
            _messagerService = messagerService;
        }

        public async Task<bool> BuildProcess(int processId)
        {
            ProcessEntity entity = await _unitOfWork.ProcessRepository.GetAsync(processId, false);
            if(entity == null)
            {
                return false;
            }
            return BuildProcess(entity);
        }

        public bool BuildProcess(ProcessEntity processEntity)
        {
            Dictionary<string, BaseProcessElement> processElements = new();
            Dictionary<string, List<ProcessConnection>> processConnections = new();
            
            foreach (var item in processEntity.ProcessElements)
            {
                BaseProcessElement processElement = _processElementTypesService.InstantiateProcessElement(item.ProcessElementInstanseType);
                processElement.Code = item.Code;
                processElement.Instantiate(_mapper.Map<List<ProcessParamEntity>, List<ProcessParam>>(item.ProcessElementParams));
                processElements.Add(item.Code, processElement);
            }

            foreach (var item in processEntity.ProcessElementsConnections)
            {
                string inElementCode = processEntity.ProcessElements.Where(pe => pe.Id == item.InElementId).First().Code;
                string outElementCode = processEntity.ProcessElements.Where(pe => pe.Id == item.OutElementId).First().Code;
                
                if(!processConnections.ContainsKey(inElementCode))
                {
                    processConnections.Add(inElementCode, new List<ProcessConnection>());
                }

                processConnections[inElementCode].Add(new ProcessConnection()
                {
                    InElement = processElements[inElementCode],
                    OutElement = processElements[outElementCode],
                });
            }
            _processInstances[processEntity.Id] = new()
            {
                ProcessElements = processElements,
                ProcessConnections = processConnections,
                InitialProcessParams = processEntity.ProcessParams,
            };

            return true;
        }

        public async Task<bool> NextStep(int userId, int processId, ProcessIncomingMessage message)
        {
            if(!_processInstanceStatuses.ContainsKey(userId) || _processInstanceStatuses[userId].GetProcess(processId) == null)
            {
                return await StartProcess(userId, processId, message);
            }
            else if (!_processInstances.ContainsKey(processId))
            {
                await BuildProcess(processId);
            }
            ProcessInstance.ProcessInstance processInstance = _processInstances[processId];

            ProcessInstanceContext instanceContext = _processInstanceStatuses[userId].GetProcess(processId);
            if (instanceContext == null)
            {
                return false;
            }

            BaseProcessElement element = processInstance.ProcessElements[instanceContext.CurrentElementCode];

            if(element == null)
            {
                return false;
            }
            bool result = true;
            while(result)
            {
                if (element.IsExecuteAsync)
                {
                    result = await element.ExecuteAsync(_messagerService, instanceContext, message);
                }
                else
                {
                    result = element.Execute(_messagerService, instanceContext, message);
                }

                if (result)
                {
                    if(element.Code == "EndProcessElement")
                    {
                        break;
                    }
                    element = processInstance.ProcessConnections[instanceContext.CurrentElementCode].First().OutElement;
                    instanceContext.CurrentElementCode = element.Code;
                    instanceContext.CurrentElementStage = 0;
                }
            }

            return true;
        }

        public async Task<bool> StartProcess(int userId, int processId, ProcessIncomingMessage message)
        {
            if(!_processInstances.ContainsKey(processId))
            {
                await BuildProcess(processId);
            }
            ProcessInstance.ProcessInstance processInstance = _processInstances[processId];


            if (!_processInstanceStatuses.ContainsKey(userId))
            {
                _processInstanceStatuses.Add(userId, new UserProcessInstanses());
            }
            _processInstanceStatuses[userId].AddProcess(new ProcessInstanceContext()
            {
                ProcessId = processId,
                UserId = userId,
                ProcessParamList = _mapper.Map<List<ProcessParamEntity>, List<ProcessParam>>(processInstance.InitialProcessParams),
                CurrentElementCode = "StartProcessElement",
                destroyContext = DestroyContext
            });

            return await NextStep(userId, processId, message);
        }

        public void DestroyContext(int userId, int processId)
        {
            if (!_processInstanceStatuses.ContainsKey(userId))
            {
                _processInstanceStatuses.Add(userId, new UserProcessInstanses());
            }
            _processInstanceStatuses[userId].RemoveProcess(processId);
        }
    }
}
