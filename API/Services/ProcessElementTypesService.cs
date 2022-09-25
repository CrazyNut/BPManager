using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using API.Abstractions;
using API.Interfaces;

namespace API.Services
{
    public class ProcessElementTypesService : IProcessElementTypesService
    {
        public Dictionary<Type, string?> GetProcessElementTypes()
        {
            Type t = typeof(BaseProcessElement);
            return Assembly.GetAssembly(t).GetTypes()
            .Where(TheType => TheType.IsClass && !TheType.IsAbstract && TheType.IsSubclassOf(t))
            .ToDictionary(t => t, t => t.AssemblyQualifiedName);
        }
    }
}