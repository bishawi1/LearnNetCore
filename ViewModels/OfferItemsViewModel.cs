using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MSIS.Models;

namespace MSIS.ViewModels
{
    public class OfferItemsViewModel:OfferDetail
    {
        public string ItemName { get; set; }
        public string ItemUnitName { get; set; }
        public string MainItemName { get; set; }
        public string MyProperty { get; set; }
        public string CategoryName { get; set; }

        public Double TotalPrice { get; set; }

    }
}
