using API.ProcessExecutor.Interfaces;
using API.DTO;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProcessRedactorController : BaseContoller
    {
        private readonly IEntityService<ProcessDTO> _processEntityService;
        private readonly IProcessElementTypesService _processElementTypesService;
        private readonly IProcessExecutorService _processExecutorService;

        public ProcessRedactorController(
            IEntityService<ProcessDTO> processEntityService,
            IProcessElementTypesService processElementTypesService,
            IProcessExecutorService processExecutorService)
        {
            _processEntityService = processEntityService;
            _processElementTypesService = processElementTypesService;
            _processExecutorService = processExecutorService;
        }

        [HttpGet("types")]
        public IEnumerable<ProcessTypeDTO> GetProcessElementTypes()
        {
            return _processElementTypesService.GetProcessElementTypes();
        }

        [HttpGet("typeParams")]
        public Dictionary<string, List<ProcessParamDTO>> GetProcessElementsParams()
        {
            return _processElementTypesService.GetProcessElementsParamets();
        }

        [HttpGet]
        public async Task<IEnumerable<ProcessDTO>> GetProcesses()
        {
            return await _processEntityService.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ProcessDTO> GetProcessById(int id)
        {
            return await _processEntityService.GetAsync(id);
        }

        [HttpPost]
        public async Task<ProcessDTO> AddProcess(ProcessDTO process)
        {
            var processDTO =  await _processEntityService.AddAsync(process);
            await _processExecutorService.BuildProcess(processDTO.Id);
            return processDTO;
        }

        [HttpPut]
        public async Task<ProcessDTO> UpdateProcess(ProcessDTO process)
        {
            var processDTO = await _processEntityService.UpdateAsync(process);
            await _processExecutorService.BuildProcess(processDTO.Id);
            return processDTO;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProcess(int id)
        {
            await _processEntityService.DeleteAsync(id);
            return Ok();
        }
    }
}