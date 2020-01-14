using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MSIS.Models
{
    [NotMapped]
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
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value From List")]
        [Required]
        public int SupplierId { get; set; }
        [Required]
        public int BranchId { get; set; }
        [Required]
        public int ProjectId { get; set; }
        [Required]
        public int EmployeeId { get; set; }
        [Required]
        public int CurrencyId { get; set; }
        public float CurrencyRate { get; set; }
        public string Notes { get; set; }
        [Required]
        public DateTime Time_Stamp { get; set; }
        [Required]
        public string User_Name { get; set; }
        [Required]
        public int StateId { get; set; }
        public string Description { get; set; }

        [NotMapped]
        public Double SubtractionAmount { get; set; }
        [NotMapped]
        public string SubtractNotes { get; set; }
        public DateTime DeliveryDate { get; set; }

    }
}
