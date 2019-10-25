using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSIS.Models
{
    public class Tasks
    {
        [Key]
        public int Id { get; set; }
        
        [Column(TypeName ="Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime TaskDate { get; set; }
        
        [Column(TypeName ="Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime TaskStartDate { get; set; }
        
        [Column(TypeName ="Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime TaskEndDate { get; set; }

        [Required]
        public int TaskOwnerId { get; set; }
        [Column(TypeName ="nvarchar(300)")]
        public string TaskSubject { get; set; }
        public int BranchId { get; set; }
        public int ProjectId { get; set; }
        public int TaskStatusId { get; set; }
        public int TaskResponsibleId { get; set;}
        [Column(TypeName ="nvarchar(Max)")]
        public string OtherInformation { get; set; }
        [Column(TypeName = "nvarchar(Max)")]
        public string TaskResultDescription { get; set; }
        public string UserName { get; set; }
        public DateTime Time_Stamp { get; set; }
        [Column(TypeName ="nvarchar(Max)")]
        public string Description { get; set; }


    }
}
