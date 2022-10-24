using API.ProcessExecutor.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.ProcessExecutor.Interfaces
{
    public interface IProcessElementTypesService
    {
        public Dictionary<string,string> GetProcessElementTypes();
        public Dictionary<string, List<ProcessParam>> GetProcessElementsParamets();
        public List<ProcessParam> GetProcessElementParamets();
    }
}