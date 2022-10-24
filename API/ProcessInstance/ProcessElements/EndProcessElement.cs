using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Interfaces;
using API.ProcessInstance.Objects;

namespace API.ProcessInstance.ProcessElements
{
    public class EndProcessElement : BaseProcessElement
    {
        public EndProcessElement()
            :base()
        {
            this.IsExecuteAsync = true;
        }

        public override bool Execute(IMessagerService messager, ProcessInstanceContext instanceContext, ProcessIncomingMessage message)
        {
            throw new NotImplementedException();
        }

        public override async Task<bool> ExecuteAsync(IMessagerService messager, ProcessInstanceContext instanceContext, ProcessIncomingMessage message)
        {
            await messager.SendMessageAsync(new ProcessOutcomingMessage()
            {
                Text = $"Process ended with message {message.Text}",
                IsLog = true
            });
            instanceContext.DestroyThisContext();
            return true;
        }

        public override bool Instantiate(List<ProcessParam> processParams)
        {
            _processParams = processParams;
            return true;
        }
    }
}