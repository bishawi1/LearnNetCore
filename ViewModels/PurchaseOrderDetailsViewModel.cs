using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MSIS.Models;
using Microsoft.AspNetCore.Http;

namespace MSIS.ViewModels
{
    [NotMapped]
    public class PurchaseOrderDetailsViewModel
    {
        public int Id { get; set; }
        [Required]
        public int PurchaseOrderYear { get; set; }
        [Required]
        public int PurchaseOrderNo { get; set; }
        public string PurchaseOrderCode { get; set; }
        [Required]
        public DateTime PurchaseOrderDate { get; set; }
       
        [Range(1, int.MaxValue, ErrorMessage = "Supplier,Please enter a value From List")]
        [Required]
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Branch,Please enter a value From List")]
        [Required]
        public int BranchId { get; set; }
        public string BranchName { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Project,Please enter a value From List")]
        [Required]
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
 
        [Range(1, int.MaxValue, ErrorMessage = "Employee,Please enter a value From List")]
        [Required]
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }

        public string Phone { get; set; }
        public string MobileNo { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int CurrencyId { get; set; }
        public float CurrencyRate { get; set; }
        public string Notes { get; set; }
        public DateTime Time_Stamp { get; set; }
        public string User_Name { get; set; }
        public string CurrencyCode { get; set; }
        public string CurrencyName { get; set; }
        public Double TotalPrice { get; set; }
        public int StateId { get; set; }
        public string StateName { get; set; }
        public string Description { get; set; }

        public Double SubtractionAmount { get; set; }
        [NotMapped]
        public string SubtractNotes { get; set; }
        public DateTime DeliveryDate { get; set; }

        [NotMapped]
        public List<PurchaseOrderItemsViewModel> OrderItems { get; set; }
        [NotMapped]
        public List<Models.Supplier> suppliers { get; set; }
        [NotMapped]
        public UserPermissionDetailsViewModel Permission { get; set; }
        [NotMapped]
        public PurchaseOrderPermission purchaseOrderPermission { get; set; }
        [NotMapped]
        public List<PurchaseOrderAttachment> Attachments { get; set; }

    }
}
