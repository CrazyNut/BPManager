using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTO
{
    public class ResponceDTO<T>
    {
        public int code {get;set;}
        public string result {get;set;}
        public T resultObject {get;set;}
    }
}