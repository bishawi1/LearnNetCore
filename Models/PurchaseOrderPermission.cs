using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MSIS.Models
{
    public class PurchaseOrderPermission
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        public bool AllowConfirm { get; set; }
        public bool AllowPrint { get; set; }
        public bool AllowVerify { get; set; }
        public bool AllowDelivery { get; set; }
        public string Notes { get; set; }
        public int BranchId { get; set; }

    }
}
