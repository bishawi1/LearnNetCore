using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MSIS.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName ="nvarchar(150)")]
        public string CustomerName { get; set; }
        [Column(TypeName ="varchar(20)")]
        public string MobileNo { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string Email { get; set; }
        [Column(TypeName = "nvarchar(300)")]
        public string Address { get; set; }
        public string OtherInformation { get; set; }
        public bool Active { get; set; }
    }
}
