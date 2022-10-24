using API.Interfaces;
using API.ProcessInstance.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.ProcessInstance.ProcessElements
{
    public abstract class BaseProcessElement
    {
        
        protected List<ProcessParam> _processParams;
        public string Code { get; set; }
        public bool IsExecuteAsync { get; protected set; }
        public abstract bool Execute(IMessagerService messager, ProcessInstanceContext instanceContext, ProcessIncomingMessage message);
        public abstract Task<bool> ExecuteAsync(IMessagerService messager, ProcessInstanceContext instanceContext, ProcessIncomingMessage message);
        public abstract bool Instantiate(List<ProcessParam> processParams);
    }
}