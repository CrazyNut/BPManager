using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Interfaces;
using API.ProcessInstance.Objects;

namespace API.ProcessInstance.ProcessElements
{
    public class StartProcessElement : BaseProcessElement
    {
        public StartProcessElement()
            : base()
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
                Text = $"Process start with message {message.Text}",
                IsLog = true
            });

            if(message.Text == "/start")
            {
                instanceContext.LastProcessElementsResults[this.GetType().Name] = new()
                {
                    ParamType = ENUMS.ProcessParamType.boolParam,
                    boolParam = true,
                };
                return true;
            }
            return false;
        }

        public override bool Instantiate(List<ProcessParam> processParams)
        {
            _processParams = processParams;
            return true;
        }
    }
}