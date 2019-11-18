using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSIS.Models
{
    public class TaskAction
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int TaskId { get; set; }
        public int TaskStatusId { get; set; }
        public int CurrentTaskStatusId { get; set; }
        public DateTime ActionDate { get; set; }
        [Column(TypeName ="nvarchar(max)")]
        public string Description { get; set; }
        [Column(TypeName ="varchar(20)")]
        public string UserName { get; set; }
        public DateTime TimeStamp { get; set; }

    }
}
