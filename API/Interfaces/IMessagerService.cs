using API.ProcessInstance.Objects;

namespace API.Interfaces
{
    public interface IMessagerService
    {
        public Task SendMessageAsync(ProcessOutcomingMessage message);
    }
}
