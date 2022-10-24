using API.Entities;
using API.ProcessInstance.Objects;
using API.ProcessInstance.ProcessElements;

namespace API.ProcessInstance
{
    public class ProcessInstance
    {
        public List<ProcessParamEntity> InitialProcessParams { get;set; } 
        public Dictionary<string, BaseProcessElement> ProcessElements { get; set; }
        public Dictionary<string, List<ProcessConnection>> ProcessConnections { get; set; }
    }
}
