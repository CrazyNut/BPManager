using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities.ProcessExecutor
{
    public class ProcessElementConnection 
    {
        [Key]
        public Guid Id {get;set;}
        public ProcessElementSample OutElement {get;set;}
        public ProcessElementSample InElement {get;set;}
        public bool IsMain {get;set;}
        public string? Condition {get;set;}
    }
}