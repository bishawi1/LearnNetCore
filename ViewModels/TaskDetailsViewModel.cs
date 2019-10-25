using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSIS.ViewModels
{
    public class TaskDetailsViewModel
    {
        public int Id { get; set; }
        [Column(TypeName = "Date")]
        public DateTime TaskDate { get; set; }
        [Column(TypeName = "Date")]
        public DateTime TaskStartDate { get; set; }
        [Column(TypeName = "Date")]
        public DateTime TaskEndDate { get; set; }

        public int TaskOwnerName { get; set; }
        [Column(TypeName = "nvarchar(300)")]
        public string TaskSubject { get; set; }
        public string BranchName { get; set; }
        public string ProjectName { get; set; }
        public string TaskStatus { get; set; }
        public int TaskStatusId { get; set; }
        public string TaskResponsibleName { get; set; }
        [Column(TypeName = "nvarchar(Max)")]
        public string OtherInformation { get; set; }
        [Column(TypeName = "nvarchar(Max)")]
        public string TaskResultDescription { get; set; }
        public string UserName { get; set; }
        public DateTime Time_Stamp { get; set; }
        [Column(TypeName = "nvarchar(Max)")]
        public string Description { get; set; }

        public string TaskActionDetails { get; set; }
        public string TaskOperation { get; set; }


        public List<EmployeesInTaskViewModel> TaskTeam { get; set; }

        
 }
}
