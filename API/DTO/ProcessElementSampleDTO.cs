using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTO
{
    public class ProcessElementSampleDTO
    {
       [Key]
        public Guid Id {get;set;}
        public string Name {get;set;}
        public string Code {get;set;}
        public ProcessSampleDTO Process {get;set;}
        public string ProcessElementInstanseType {get;set;}
        public List<ProcessParamDTO>? ProcessElementParams {get;set;}
        public List<ProcessElementConnectionDTO>? OutConnections {get;set;}
        public List<ProcessElementConnectionDTO>? InConnections {get;set;}
    }
}