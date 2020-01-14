using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSIS.ViewModels
{
    [NotMapped]
    public class SQLOfferDetailsViewModel
    {
        [Key]
        public int Id { get; set; }
        public int OfferId { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string CategoryName { get; set; }
        public string MainItemName { get; set; }
        public int ItemUnitId { get; set; }
        public string ItemUnitName { get; set; }
        public double UnitPrice { get; set; }
        public double QNT { get; set; }
        public string Description { get; set; }
        public double TotalPrice { get; set; }

    }
}
