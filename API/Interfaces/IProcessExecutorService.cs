using API.Entities;
using API.ProcessInstance.Objects;

namespace API.Interfaces
{
    public interface IProcessExecutorService
    {
        public Task<bool> BuildProcess(int processId);
        public bool BuildProcess(ProcessEntity processEntity);
        public Task<bool> StartProcess(int userId, int processId, ProcessIncomingMessage message);
        public Task<bool> NextStep(int userId, int processId, ProcessIncomingMessage message);
    }
}