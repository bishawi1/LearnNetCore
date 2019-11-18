using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MSIS.ViewModels
{
    [NotMapped]
    public class PurchaseOrderItemsViewModel
    {
        public int Id { get; set; }
        public int PuchaseOrderId { get; set; }
        public string ItemName { get; set; }
        public int ItemUnitId { get; set; }
        public float QNT { get; set; }

        public float UnitPrice { get; set; }
        public string Description { get; set; }
        public string ItemUnitName { get; set; }
        public Single TotalPrice { get; set; }

    }
}
