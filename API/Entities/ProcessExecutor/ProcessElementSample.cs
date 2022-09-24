using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities.ProcessExecutor
{
    public class ProcessElementSample : BaseEntity
    {
        public Type ProcessElementInstanseType {get;set;}
        public List<ProcessParam> ProcessElementParams {get;set;}
        public List<ProcessElementConnection> OutConnections {get;set;}
        public List<ProcessElementConnection> InConnections {get;set;}
    }
}