using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTO
{
    public class ProcessElementConnectionDTO : BaseDTO
    {
       [Key]
        public int Id {get;set;}
        public int OutElementId {get;set;}
        public int InElementId {get;set;}
        public bool IsMain {get;set;}
        public string Condition {get;set;}
    }
}