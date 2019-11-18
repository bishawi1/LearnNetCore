using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MSIS.Models
{
    public class Supplier
    {
        public int Id { get; set; }
        [Required]
        [Column(TypeName ="nvarchar(100)")]
        public string SupplierName { get; set; }
        public string Phone { get; set; }
        public string MobileNo { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string OtherInformation { get; set; }

    }
}
