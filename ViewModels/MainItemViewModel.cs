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
    public class MainItemViewModel:MainItem
    {
        public string CategoryName { get; set; }
        public List<ItemCategory> ItemCategories { get; set; }
    }
}
