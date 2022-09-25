using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTO
{
    public class ProcessElementConnectionDTO
    {
       [Key]
        public Guid Id {get;set;}
        public ProcessElementSampleDTO OutElement {get;set;}
        public ProcessElementSampleDTO InElement {get;set;}
        public bool IsMain {get;set;}
        public string? Condition {get;set;}
    }
}