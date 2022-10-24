using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using API.ProcessExecutor.Abstractions;
using API.ProcessExecutor.Interfaces;
using API.ProcessExecutor.Objects;

namespace API.ProcessExecutor.Services
{
    public class ProcessElementTypesService : IProcessElementTypesService
    {
        private readonly Dictionary<string, string> _types;

        public ProcessElementTypesService()
        {
            _types = GetTypes();
        }

        public Dictionary<string, string> GetProcessElementTypes()
        {
            return _types;
        }

        public static Dictionary<string,string> GetTypes()
        {
            return Assembly.GetAssembly(typeof(BaseProcessElement)).GetTypes()
            .Where(TheType => TheType.IsClass && !TheType.IsAbstract && TheType.IsSubclassOf(typeof(BaseProcessElement)))
            .ToDictionary(t => t.Name, t => t.AssemblyQualifiedName);
        }

        public Dictionary<string, List<ProcessParam>> GetProcessElementsParamets()
        {
            throw new NotImplementedException();
        }

        public List<ProcessParam> GetProcessElementParamets()
        {
            throw new NotImplementedException();
        }
    }
}