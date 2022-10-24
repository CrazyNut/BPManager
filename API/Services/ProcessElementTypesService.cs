using System.Reflection;
using API.DTO;
using API.Entities;
using API.Interfaces;
using API.ProcessExecutor.Interfaces;
using API.ProcessInstance.Objects;
using API.ProcessInstance.ProcessElements;
using Microsoft.Extensions.Localization;

namespace API.Services
{
    public class ProcessElementTypesService : IProcessElementTypesService
    {
        private readonly Dictionary<string, string> _types;
        private readonly Dictionary<string, List<ProcessParamDTO>> _processParams;
        private readonly IStringLocalizer<ProcessElementTypesService> _localizer;

        public ProcessElementTypesService(
            IStringLocalizer<ProcessElementTypesService> localizer)
        {
            _localizer = localizer;
            _types = GetTypes();
            _processParams = GetTypeParams();
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

        private Dictionary<string, List<ProcessParamDTO>> GetTypeParams()
        {
            List<Type> types = Assembly.GetAssembly(typeof(BaseProcessElement)).GetTypes()
            .Where(TheType => TheType.IsClass && !TheType.IsAbstract && TheType.IsSubclassOf(typeof(BaseProcessElement)))
            .ToList();
            Dictionary<string, List<ProcessParamDTO>> props = new();
            string[] ignoredParams = new[] { "Code", "IsExecuteAsync" };
            foreach (var item in types)
            {
                props.Add(item.Name, new List<ProcessParamDTO>());
                PropertyInfo[] propInfos = item.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach(var propInfo in propInfos)
                {
                    if(!ignoredParams.Contains(propInfo.Name))
                    {
                        props[item.Name].Add(new ProcessParamDTO()
                        {
                            ParamType = propInfo.Name.EndsWith("Condition") ? ENUMS.ProcessParamType.conditionParam : GetParamType(propInfo.PropertyType.Name),
                            Code = propInfo.Name,
                            Name = _localizer[$"{item.Name}.{propInfo.Name}"]
                        });
                    }
                }
            }

            return props;
        }

        private static ENUMS.ProcessParamType GetParamType(string typeName)
        {
            switch (typeName)
            {
                case "Int32":
                    return ENUMS.ProcessParamType.intParam;
                case "Double":
                    return ENUMS.ProcessParamType.doubleParam;
                case "Boolean":
                    return ENUMS.ProcessParamType.boolParam;
                case "String":
                    return ENUMS.ProcessParamType.stringParam;
                default:
                    return ENUMS.ProcessParamType.stringParam;
            }
        }

        public Dictionary<string, List<ProcessParamDTO>> GetProcessElementsParamets()
        {
            return _processParams;
        }

        public List<ProcessParamDTO> GetProcessElementParamets(string processElementType)
        {
            return _processParams[processElementType];
        }

        public BaseProcessElement InstantiateProcessElement(string processElementType)
        {
            Type elementType = Type.GetType(_types[processElementType], true);
            return InstantiateProcessElement(elementType);
        }

        public BaseProcessElement InstantiateProcessElement(Type processElementType)
        {
            return Activator.CreateInstance(processElementType) as BaseProcessElement;
        }
    }
}