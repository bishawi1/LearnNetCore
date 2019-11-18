using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MSIS.Models
{
    public class PurchaseOrder
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int PurchaseOrderYear { get; set; }
        [Required]
        public int PurchaseOrderNo { get; set; }
        [Required]
        public string PurchaseOrderCode { get; set; }
        [Required]
        public DateTime PurchaseOrderDate { get; set; }
        [Required]
        public int SupplierId { get; set; }
        public int CurrencyId { get; set; }
        public float CurrencyRate { get; set; }
        public string Notes { get; set; }
        [Required]
        public DateTime Time_Stamp { get; set; }
        [Required]
        public string User_Name { get; set; }
    }
}
