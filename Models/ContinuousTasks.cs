using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MSIS.Models
{
    public class ContinuousTask
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int TaskId { get; set; }
        public DateTime ContinueFromDate { get; set; }
        public DateTime ContinueToDate { get; set; }
        public Byte PeriodId { get; set; }
        public string Notes { get; set; }
        public DateTime NextTaskDate { get; set; }

    }
}
