using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class ProcessElementConnectionEntity
    {
        [Key]
        public int Id { get; set; }
        public ProcessEntity Process { get; set; }
        public int ProcessId { get; set; }
        public ProcessElementEntity OutElement { get; set; }
        public int? OutElementId { get; set; }
        public ProcessElementEntity InElement { get; set; }
        public int? InElementId { get; set; }
        public bool IsMain { get; set; }
        public string Condition { get; set; }
    }
}