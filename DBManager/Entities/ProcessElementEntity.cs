using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class ProcessElementEntity : BaseEntity
    {
        public ProcessEntity Process { get; set; }
        public int ProcessId { get; set; }
        public Type ProcessElementInstanseType { get; set; }
        public List<ProcessParamEntity> ProcessElementParams { get; set; }
        public List<ProcessElementConnectionEntity> OutConnections { get; set; }
        public List<ProcessElementConnectionEntity> InConnections { get; set; }
    }
}