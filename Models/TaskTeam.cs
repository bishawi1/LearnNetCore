using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MSIS.Models
{
    public class TaskTeam
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int TaskId { get; set; }

        [Required]
        public int EmployeeId { get; set; }
        public DateTime TimeStamp { get; set; }
        [Column(TypeName ="varchar(20)")]
        public string UserName { get; set; }


    }
}
