using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSIS.Models
{
    [NotMapped]
    public class Page
    {
        [Key]
        public int Id { get; set; }
        public string ParentName { get; set; }
        public string PageName { get; set; }

    }
}
