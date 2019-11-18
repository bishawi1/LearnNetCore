using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MSIS.Models
{
    public class PurchaseOrderDetails
    {
        [Key]
        public int Id { get; set; }        
        public int PuchaseOrderId { get; set; }
        [Required]
        public string ItemName { get; set; }
        [Required]
        public int ItemUnitId { get; set; }
        [Required]
        public float QNT { get; set; }

        [Required]
        public float UnitPrice { get; set; }
        public string Description { get; set; }

    }
}
