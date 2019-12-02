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
    public class ItemDetailsViewModels:Item
    {
        public string CurrencyCode { get; set; }

        public string CurrencyName { get; set; }
        public string ItemUnitName { get; set; }


        public string MainItemName { get; set; }
        public List<MainItemViewModel> MainItems { get; set; }
        public List<ItemUnit> ItemUnits { get; set; }
        public List<Currency> CurrencyList { get; set; }
        public List<IFormFile> Photos { get; set; }

    }
}
