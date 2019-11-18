using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MSIS.Models
{
    public class Offer
    {
        [Key]
        public int Id { get; set; }
        public DateTime OfferDate { get; set; }
        public int CurrencyId { get; set; }
        public int CustomerId { get; set; }
        public string OtherInformation { get; set; }

    }
}
