using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Interfaces;
using API.ProcessInstance.Objects;

namespace API.ProcessInstance.ProcessElements
{
    public class SumProcessElement : BaseProcessElement
    {
        public SumProcessElement()
            : base()
        {
            this.IsExecuteAsync = true;
        }

        public int FirstValue { get; set; }
        public int SecondValue { get; set; }

        public override bool Execute(IMessagerService messager, ProcessInstanceContext instanceContext, ProcessIncomingMessage message)
        {
            throw new NotImplementedException();
        }

        public override async Task<bool> ExecuteAsync(IMessagerService messager, ProcessInstanceContext instanceContext, ProcessIncomingMessage message)
        {
            await messager.SendMessageAsync(new ProcessOutcomingMessage()
            {
                Text = $"Process sum with result {FirstValue + SecondValue}",
                IsLog = true
            });

            instanceContext.LastProcessElementsResults[this.GetType().Name] = new()
            {
                ParamType = ENUMS.ProcessParamType.intParam,
                intParam = FirstValue + SecondValue,
            };
            return true;
        }

        public override bool Instantiate(List<ProcessParam> processParams)
        {
            _processParams = processParams;

            FirstValue = processParams.Where(pp => pp.Code == "FirstValue" && pp.ParamType == ENUMS.ProcessParamType.intParam).FirstOrDefault().intParam;
            SecondValue = processParams.Where(pp => pp.Code == "SecondValue" && pp.ParamType == ENUMS.ProcessParamType.intParam).FirstOrDefault().intParam;

            return true;
        }
    }
}