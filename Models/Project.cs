using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MSIS.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName ="varchar(4)")]
        public int ProjectYear { get; set; }
        [Required]
        public int ProjectSerial { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string ProjectName { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Owner,Please enter a value From List")]
        [Required]
        public int ProjectOwner { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string Fax { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string Email { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string MobileNo { get; set; }
        [Column(TypeName = "nvarchar(200)")]
        public string Address { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string OtheInformation { get; set; }
        [ForeignKey(nameof(ProjectOwner))]
        public Customer Customer { get; set; }
    }
}
