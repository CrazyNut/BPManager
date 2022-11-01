using API.DTO;
using API.ProcessInstance.Objects;
using API.ProcessInstance.ProcessElements;

namespace API.ProcessExecutor.Interfaces
{
    public interface IProcessElementTypesService
    {
        public List<ProcessTypeDTO> GetProcessElementTypes();
        public Dictionary<string, List<ProcessParamDTO>> GetProcessElementsParamets();
        public List<ProcessParamDTO> GetProcessElementParamets(string processElementType);
        public BaseProcessElement InstantiateProcessElement(string processElementType);
        public BaseProcessElement InstantiateProcessElement(Type processElementType);
    }
}