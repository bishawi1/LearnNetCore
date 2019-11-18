using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MSIS.Models;

namespace MSIS.ViewModels
{
    public class CreateOfferViewModel:Offer
    {
        public string CurrencyName { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public List<Currency> CurrencyList { get; set; }
        public List<Customer> Customers { get; set; }

    }
}
