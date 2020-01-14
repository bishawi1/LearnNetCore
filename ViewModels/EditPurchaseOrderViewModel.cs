using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MSIS.Models;

namespace MSIS.ViewModels
{
    [NotMapped]
    public class EditPurchaseOrderViewModel
    {
        public PurchaseOrderDetailsViewModel PurchaseOrderDetails { get; set; }

        [NotMapped]
        public List<Models.Supplier> suppliers { get; set; }
        public List<Models.Project> Projects { get; set; }
        public List<Models.Branch> Branches { get; set; }
        public List<Models.Employee> Employees { get; set; }

        [NotMapped]
        public List<Models.Currency> CurrencyList { get; set; }

        [NotMapped]
        public List<Models.ItemUnit> Units { get; set; }
        [NotMapped]
        public PurchaseOrderPermission purchaseOrderPermission { get; set; }

    }

}
