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
        [Required]
        public DateTime OfferDate { get; set; }
        public int CurrencyId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Customer,Please enter a value From List")]
        [Required]
        public int CustomerId { get; set; }
        public string OtherInformation { get; set; }

    }
}
