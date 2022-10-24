using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProcessExecutorTesterController : BaseContoller
    {
        private readonly IProcessExecutorService _processExecutorService;

        public ProcessExecutorTesterController(IProcessExecutorService processExecutorService)
        {
            _processExecutorService = processExecutorService;
        }
        [HttpGet]
        public async Task StartProcess(int userId, int processId, string text)
        {
           await _processExecutorService.NextStep(userId, processId, new ProcessInstance.Objects.ProcessIncomingMessage() { Text = text });
        }
    }
}
