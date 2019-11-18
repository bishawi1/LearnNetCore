using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MSIS.ViewModels
{
    [NotMapped]
    public class EditPurchaseOrderViewModel
    {
        public PurchaseOrderDetailsViewModel PurchaseOrderDetails { get; set; }

        [NotMapped]
        public List<Models.Supplier> suppliers { get; set; }

        [NotMapped]
        public List<Models.Currency> CurrencyList { get; set; }

        [NotMapped]
        public List<Models.ItemUnit> Units { get; set; }
    }

}
