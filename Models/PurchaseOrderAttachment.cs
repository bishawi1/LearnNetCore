using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MSIS.Models
{
    public class PurchaseOrderAttachment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int PurchaseOrderId { get; set; }
        public string URL { get; set; }
        public string Caption { get; set; }
        public string Description { get; set; }

    }
}
