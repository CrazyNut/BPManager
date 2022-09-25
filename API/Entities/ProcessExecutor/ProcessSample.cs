using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities.ProcessExecutor
{
    public class ProcessSample : BaseEntity
    {
        public List<ProcessElementSample>? ProcessElements {get;set;}
        public List<ProcessParam>? ProcessParams{get;set;} 
    }
}