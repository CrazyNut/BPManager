using System.ComponentModel.DataAnnotations;
using API.Entities;
using API.Validation;

namespace API.DTO
{
    public class ProcessDTO : BaseDTO
    {
        [Key]
        public int Id { get; set; }
        public string Name {get;set;}
        [Unique(ErrorMessage = "Process with same Code already exist !!", TargetModelType = typeof(ProcessEntity), TargetPropertyName = "Code")]
        public string Code {get;set;}
        public List<ProcessParamDTO> ProcessParams{get;set; }
        public List<ProcessElementDTO> ProcessElements { get; set; }
        public List<ProcessElementConnectionDTO> ProcessElementsConnections { get; set; }
    }
}