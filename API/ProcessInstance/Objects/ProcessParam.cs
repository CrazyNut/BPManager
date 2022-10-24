using API.ENUMS;

namespace API.ProcessInstance.Objects
{
    public class ProcessParam
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public ProcessParamType ParamType { get; set; }
        public string Condition { get; set; }
        public string stringParam { get; set; }
        public int intParam { get; set; }
        public double doubleParam { get; set; }
        public bool boolParam { get; set; }
    }
}
