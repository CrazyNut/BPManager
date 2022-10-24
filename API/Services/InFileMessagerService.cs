using API.Interfaces;
using API.ProcessInstance.Objects;

namespace API.Services
{
    public class InFileMessagerService : IMessagerService
    {
        public async Task SendMessageAsync(ProcessOutcomingMessage message)
        {
            using StreamWriter file = new("C:\\Temp\\BPManager.txt", append: true);
            await file.WriteLineAsync(message.Text);
        }
    }
}
