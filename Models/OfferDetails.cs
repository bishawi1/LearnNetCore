using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSIS.Models
{
    public class OfferDetail
    {
        [Key]
        public int Id { get; set; }
        public int OfferId { get; set; }
        public int ItemId { get; set; }
        public int ItemUnitId { get; set; }
        public Double UnitPrice { get; set; }
        public Double QNT { get; set; }
        public string Description { get; set; }

    }
}
