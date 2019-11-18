using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MSIS.ViewModels
{
    [NotMapped]
    public class PurchaseOrderDetailsViewModel
    {
        public int Id { get; set; }
        public int PurchaseOrderYear { get; set; }
        public int PurchaseOrderNo { get; set; }
        public string PurchaseOrderCode { get; set; }
        public DateTime PurchaseOrderDate { get; set; }
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
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
        [NotMapped]
        public List<PurchaseOrderItemsViewModel> OrderItems { get; set; }
        [NotMapped]
        public List<Models.Supplier> suppliers { get; set; }
    }
}
