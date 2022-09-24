using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Abstractions;

namespace API.ProcessElements
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