using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using API.Validation;

namespace API.DTO
{
    public class ProcessElementDTO : BaseDTO
    {
        [Key]
        public int Id { get; set; }
        public string Name {get;set;}
        public string Code {get;set;}
        [StringIsTypeValidationAttribute(ErrorMessage = "Specified string cannot be converted to type")]
        public string ProcessElementInstanseType {get;set;}
        public List<ProcessParamDTO> ProcessElementParams {get;set;}
    }
}