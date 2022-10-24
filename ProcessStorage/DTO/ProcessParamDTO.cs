using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using API.ENUMS;

namespace API.DTO
{
    public class ProcessParamDTO : BaseDTO
    {
        [Key]
        public int Id { get; set; }
        public string Name {get;set;}
        public string Code {get;set;}
        public ProcessParamRouteType ParamRouteType {get;set;}
        public ProcessParamType ParamType {get;set;} 
        public string Condition {get;set;}
        public string stringParam {get;set;}
        public int? intParam {get;set;}
        public double? doubleParam {get;set;}
        public bool? boolParam {get;set;}
    }
}