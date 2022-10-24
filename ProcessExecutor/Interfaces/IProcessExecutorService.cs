using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.ProcessExecutor.Interfaces
{
    public interface IProcessExecutorService
    {
        public bool StartProcess(int processId);
        public bool StopProcess(int processId);
        public bool NextStep(int processInstanseId);
    }
}