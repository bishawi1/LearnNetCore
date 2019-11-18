using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MSIS.Models
{
    [NotMapped]
    public class Item
    {
        public int Id { get; set; }
        public int MainItemId { get; set; }
        public string ItemName { get; set; }
        public int ItemUnitId { get; set; }
        public Double UnitPrice { get; set; }
        public string OtherInformation { get; set; }
        public string PhotoPath { get; set; }
        public int CurrencyId { get; set; }

    }
}
