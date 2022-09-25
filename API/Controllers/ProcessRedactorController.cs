using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTO;
using API.Entities.ProcessExecutor;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/processes")]
    public class ProcessRedactorController : ControllerBase
    {
        private readonly IProcessSampleRepository processSampleRepository;
        private readonly IMapper mapper;
        private readonly IProcessElementTypesService processElementTypesService;

        public ProcessRedactorController(IProcessSampleRepository processSampleRepository, IMapper mapper,IProcessElementTypesService processElementTypesService)
        {
            this.processSampleRepository = processSampleRepository;
            this.mapper = mapper;
            this.processElementTypesService = processElementTypesService;
        }

        [HttpGet]
        public async Task<ActionResult<ResponceDTO<IEnumerable<ProcessSampleDTO>>>> GetAllProcessSamples()
        {
            return new ResponceDTO<IEnumerable<ProcessSampleDTO>>
            {
                 code = 200,
                result = "success",
                resultObject = await processSampleRepository.GetProcessSampleDTOsAsync()
            };
        }

        [HttpPost]
        public async Task<ActionResult<ResponceDTO<ProcessSampleDTO>>> AddProcessSample(ProcessSampleDTO sample)
        {
            ProcessSample processSample = mapper.Map<ProcessSample>(sample);
            processSampleRepository.Create(processSample);
            if(await processSampleRepository.SaveAllAsync())
            {
                sample.Id = processSample.Id;
                return new ResponceDTO<ProcessSampleDTO>
                {
                    code = 200,
                    result = "success",
                    resultObject = sample
                };
            }
            return new ResponceDTO<ProcessSampleDTO>
                {
                    code = 500,
                    result = "error occured during saving",
                    resultObject = sample
                };
        }
        
        [HttpPut]
        public async Task<ActionResult<ResponceDTO<ProcessSampleDTO>>> UpdateProcessSample(ProcessSampleDTO sample)
        {
            ProcessSample processSample = mapper.Map<ProcessSample>(sample);
            processSampleRepository.Update(processSample);
            if(await processSampleRepository.SaveAllAsync())
            {
                sample.Id = processSample.Id;
                return new ResponceDTO<ProcessSampleDTO>
                {
                    code = 200,
                    result = "success",
                    resultObject = sample
                };
            }
            return new ResponceDTO<ProcessSampleDTO>
                {
                    code = 500,
                    result = "error occured during saving",
                    resultObject = sample
                };
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteProcessSample(Guid Id)
        {
            var process = await processSampleRepository.GetProcessSampleByIdAsync(Id);
            if(process == null)
                return Ok();
            processSampleRepository.Delete(process);
            if(await processSampleRepository.SaveAllAsync())
                return Ok();
            return BadRequest("One or more error occured");
        }
        
    }
}