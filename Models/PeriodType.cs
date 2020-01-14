using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSIS.Models
{
    public class PeriodTypeModel
    {
        [Key]
        public byte PeriodId { get; set; }
        [Required]
        public string PeriodType { get; set; }

    }
}
