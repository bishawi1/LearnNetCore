using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSIS.ViewModels
{
    [NotMapped]
    public class OfferSearchViewModel
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int CurrencyId { get; set; }
        public int CustomerId { get; set; }
        public string OtherInformation { get; set; }
        public string strGroupBy { get; set; }

        public List<Models.Currency> CurrencyList { get; set; }

        public List<Models.Customer> Customers { get; set; }

    }
}
