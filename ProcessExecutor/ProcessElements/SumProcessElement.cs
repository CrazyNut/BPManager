using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.ProcessExecutor.Abstractions;

namespace API.ProcessExecutor.ProcessElements
{
    public class SumProcessElement : BaseProcessElement
    {
        public SumProcessElement()
        {
        }

        public override bool Execute()
        {
            return true;
        }
    }
}