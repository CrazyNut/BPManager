using System.ComponentModel.DataAnnotations;
using API.Entities.ProcessExecutor;
using API.Validation;

namespace API.DTO
{
    public class ProcessSampleDTO
    {
        public Guid Id {get;set;}
        public string Name {get;set;}
        [Unique(ErrorMessage = "Process with same Code already exist !!", TargetModelType = typeof(ProcessSample), TargetPropertyName = "Code")]
        public string Code {get;set;}
        public List<ProcessElementSampleDTO>? ProcessElements {get;set;}
        public List<ProcessParamDTO>? ProcessParams{get;set;} 
    }
}