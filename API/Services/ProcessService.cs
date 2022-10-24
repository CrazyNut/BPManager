using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTO;
using API.Entities;
using API.Interfaces;
using API.UnitsOfWork;
using AutoMapper;

namespace API.Services
{
    public class ProcessService : IEntityService<ProcessDTO>
    {
        private readonly IMapper _mapper;
        private readonly AppUnitOfWork _unitOfWork;

        public ProcessService(AppUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ProcessDTO> AddAsync(ProcessDTO item)
        {

            ProcessEntity process = CreateProcess(item);

            await _unitOfWork.SaveAsync();

            return _mapper.Map<ProcessDTO>(process);
        }

        public async Task<List<ProcessDTO>> AddManyAsync(List<ProcessDTO> items)
        {
            List<ProcessEntity> processes = new();
            foreach (var processDTO in items)
            {
                processes.Add(CreateProcess(processDTO));
            }

            await _unitOfWork.SaveAsync();

            return _mapper.Map<List<ProcessEntity>, List<ProcessDTO>>(processes);
        }

        public async Task<ProcessDTO> GetAsync(int id)
        {
            var process = await _unitOfWork.ProcessRepository.GetAsync(id, false);

            if (process == null)
            {
                return null;
            }

            return _mapper.Map<ProcessDTO>(process);
        }

        public async Task<IEnumerable<ProcessDTO>> GetAllAsync()
        {
            var processes = await _unitOfWork.ProcessRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<ProcessEntity>, IEnumerable<ProcessDTO>>(processes);
        }

        public async Task<ProcessDTO> UpdateAsync(ProcessDTO item)
        {
            if (await _unitOfWork.ProcessRepository.GetAsync(item.Id) == null)
            {
                return null;
            }

            var process = await UpdateProcess(item);
            _unitOfWork.ApplicationContext.ChangeTracker.DetectChanges();
            Console.WriteLine(_unitOfWork.ApplicationContext.ChangeTracker.DebugView.LongView);

            await _unitOfWork.SaveAsync();

            return _mapper.Map<ProcessDTO>(process);
        }

        public async Task<string> DeleteAsync(int id)
        {
            ProcessEntity process = await _unitOfWork.ProcessRepository.GetAsync(id);
            if (process == null)
            {
                return null;
            }

            _unitOfWork.ProcessRepository.Delete(process);
            await _unitOfWork.SaveAsync();

            return "Deleted";
        }

        public async Task<string> DeleteManyAsync(int[] entitiesId)
        {
            List<ProcessEntity> entities = await _unitOfWork.ProcessRepository.GetManyAsync(entitiesId);
            if (entities == null || entities.Count == 0)
            {
                return null;
            }

            _unitOfWork.ProcessRepository.DeleteMany(entities);
            await _unitOfWork.SaveAsync();

            return "Deleted";
        }



        #region private
        private ProcessEntity CreateProcess(ProcessDTO processDTO)
        {
            ProcessEntity process = _mapper.Map<ProcessEntity>(processDTO);
            _unitOfWork.ProcessRepository.Create(process);

            List<ProcessElementEntity> processElements = _mapper.Map<List<ProcessElementDTO>, List<ProcessElementEntity>>(processDTO.ProcessElements);
            for (int i = 0; i < processElements.Count; i++)
            {
                processElements[i].Process = process;
                _unitOfWork.ProcessElementRepository.Create(processElements[i]);
            }
            if(processDTO.ProcessElementsConnections != null)
            {
                foreach (var processElementConnectionDTO in processDTO.ProcessElementsConnections)
                {
                    var processElementConnectionEntity = new ProcessElementConnectionEntity()
                    {
                        Process = process,
                        InElement = processElements[processElementConnectionDTO.InElementId],
                        OutElement = processElements[processElementConnectionDTO.OutElementId],
                        IsMain = processElementConnectionDTO.IsMain,
                        Condition = processElementConnectionDTO.Condition
                    };
                    _unitOfWork.ProcessElementConnectionRepository.Create(processElementConnectionEntity);
                }
            }
           
            return process;
        }
        private async Task<ProcessEntity> UpdateProcess(ProcessDTO processDTO)
        {
            ProcessEntity process = _mapper.Map<ProcessEntity>(processDTO);
            if (processDTO.Status == ENUMS.DTOStatus.Updated)
            {
                await _unitOfWork.ProcessRepository.UpdateAsync(process);
            }

            List<ProcessElementEntity> processElements = _mapper.Map<List<ProcessElementDTO>, List<ProcessElementEntity>>(processDTO.ProcessElements);
            for (int i = 0; i < processElements.Count; i++)
            {
                processElements[i].ProcessId = process.Id;
                switch (processDTO.ProcessElements[i].Status)
                {
                    case ENUMS.DTOStatus.Created:
                        _unitOfWork.ProcessElementRepository.Create(processElements[i]);
                        break;
                    case ENUMS.DTOStatus.Updated:
                        await _unitOfWork.ProcessElementRepository.UpdateAsync(processElements[i]);
                        break;
                    case ENUMS.DTOStatus.Deleted:
                        _unitOfWork.ProcessElementRepository.Delete(processElements[i]);
                        break;
                    default:
                        break;
                }
            }

            if (processDTO.ProcessElementsConnections != null)
            {
                foreach (var processElementConnectionDTO in processDTO.ProcessElementsConnections)
                {
                    if(processElementConnectionDTO.Status == ENUMS.DTOStatus.Untouched)
                    {
                        continue;
                    }    
                    var processElementConnectionEntity = new ProcessElementConnectionEntity()
                    {
                        ProcessId = process.Id,
                        IsMain = processElementConnectionDTO.IsMain,
                        Condition = processElementConnectionDTO.Condition
                    };
                    ENUMS.DTOStatus status = processDTO.ProcessElements[processElementConnectionDTO.InElementId].Status;
                    if (status == ENUMS.DTOStatus.Untouched || status == ENUMS.DTOStatus.Updated)
                    {
                        processElementConnectionEntity.InElementId = processDTO.ProcessElements[processElementConnectionDTO.InElementId].Id;
                    }
                    else
                    {
                        processElementConnectionEntity.InElement = processElements[processElementConnectionDTO.InElementId];
                    }
                    status = processDTO.ProcessElements[processElementConnectionDTO.OutElementId].Status;
                    if (status == ENUMS.DTOStatus.Untouched || status == ENUMS.DTOStatus.Updated)
                    {
                        processElementConnectionEntity.OutElementId = processDTO.ProcessElements[processElementConnectionDTO.OutElementId].Id;
                    }
                    else
                    {
                        processElementConnectionEntity.OutElement = processElements[processElementConnectionDTO.OutElementId];
                    }

                    switch (processElementConnectionDTO.Status)
                    {
                        case ENUMS.DTOStatus.Created:
                            _unitOfWork.ProcessElementConnectionRepository.Create(processElementConnectionEntity);
                            break;
                        case ENUMS.DTOStatus.Updated:
                            processElementConnectionEntity.Id = processElementConnectionDTO.Id;
                            _unitOfWork.ProcessElementConnectionRepository.Update(processElementConnectionEntity);
                            break;
                        case ENUMS.DTOStatus.Deleted:
                            processElementConnectionEntity.Id = processElementConnectionDTO.Id;
                            _unitOfWork.ProcessElementConnectionRepository.Delete(processElementConnectionEntity);
                            break;
                        default:
                            break;
                    }
                }
            }
            return process;
        }

        #endregion
    }
}