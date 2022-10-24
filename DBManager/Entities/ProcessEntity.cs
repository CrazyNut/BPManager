using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class ProcessEntity : BaseEntity
    {
        public List<ProcessElementEntity> ProcessElements { get; set; }
        public List<ProcessParamEntity> ProcessParams { get; set; }
        public List<ProcessElementConnectionEntity> ProcessElementsConnections { get; set; }
    }
}