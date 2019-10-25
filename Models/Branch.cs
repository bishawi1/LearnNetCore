using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSIS.Models
{
    public class Branch
    {
    [Key]
    public int Id { get; set; }
    [Required]
    [Column(TypeName ="nvarchar(10)")]
    public string Code { get; set; }
    [Required]
        [Column(TypeName ="nvarchar(50)")]
    public string Name { get; set; }
        [Column(TypeName ="nvarchar(50)")]
    public string Phone { get; set; }
        [Column(TypeName ="nvarchar(50)")]
    public string Mobile { get; set; }
        [Column(TypeName ="nvarchar(300)")]
    public string Address { get; set; }

    }
}
