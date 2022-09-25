using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTO;
using API.Entities.ProcessExecutor;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class ProcessSampleRepository : IProcessSampleRepository
    {
        private readonly ApplicationContext applicationContext;
        private readonly IMapper mapper;

        public ProcessSampleRepository(ApplicationContext applicationContext, IMapper mapper)
        {
            this.applicationContext = applicationContext;
            this.mapper = mapper;
        }
        public void Create(ProcessSample processSample)
        {
            applicationContext.ProcessSamples.Add(processSample);
        }

        public void Delete(ProcessSample processSample)
        {
            applicationContext.ProcessSamples.Remove(processSample);
        }

        public async Task<IEnumerable<ProcessSample>> GetProcessSamplesAsync()
        {
            return await applicationContext.ProcessSamples.ToListAsync();
        }

        public async Task<ProcessSample> GetProcessSampleByCodeAsync(string code)
        {
            return await applicationContext.ProcessSamples.SingleOrDefaultAsync(s => s.Code == code);
        }

        public async Task<ProcessSample> GetProcessSampleByIdAsync(Guid id)
        {
            return await applicationContext.ProcessSamples.SingleOrDefaultAsync(s => s.Id == id);
        }

        public async Task<bool> SaveAllAsync()
        {
           return await applicationContext.SaveChangesAsync() > 0;
        }

        public async Task Update(ProcessSample processSample)
        {
            var entity = await applicationContext.ProcessSamples.SingleOrDefaultAsync(s => s.Id == processSample.Id);
            if (entity == null)
            {
                return;
            }

            applicationContext.Entry(entity).CurrentValues.SetValues(processSample);
        }

        public async Task<IEnumerable<ProcessSampleDTO>> GetProcessSampleDTOsAsync()
        {
             return await applicationContext.ProcessSamples
            .ProjectTo<ProcessSampleDTO>(mapper.ConfigurationProvider)
            .ToListAsync();
        }
    }
}