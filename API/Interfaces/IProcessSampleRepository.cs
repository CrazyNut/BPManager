using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTO;
using API.Entities.ProcessExecutor;

namespace API.Interfaces
{
    public interface IProcessSampleRepository
    {
        void Create(ProcessSample processSample);
        Task Update(ProcessSample processSample);
        void Delete(ProcessSample processSample);
        Task<bool> SaveAllAsync();
        Task<IEnumerable<ProcessSample>> GetProcessSamplesAsync();
        Task<IEnumerable<ProcessSampleDTO>> GetProcessSampleDTOsAsync();
        Task<ProcessSample> GetProcessSampleByIdAsync(Guid id);
        Task<ProcessSample> GetProcessSampleByCodeAsync(string code);
    }
}