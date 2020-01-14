using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MSIS.ViewModels
{
    public class PurchaseOrderTotalsViewModel
    {
        [Key]
        public int CurrencyId { get; set; }
        public string CurrencyCode { get; set; }

        public string CurrencyName { get; set; }
        public Double TotalAmount { get; set; }

    }
}
