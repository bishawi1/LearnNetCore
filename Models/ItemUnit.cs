using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace TMS.Models
{
    public class ItemUnit
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ItemUnitName { get; set; }

    }
}
