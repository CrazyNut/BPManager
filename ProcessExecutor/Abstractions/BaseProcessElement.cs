using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.ProcessExecutor.Abstractions
{
    public abstract class BaseProcessElement
    {
        public abstract bool Execute();
    }
}