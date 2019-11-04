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
        public DateTime TaskDate { get; set; }
        public DateTime TaskStartDate { get; set; }
        public DateTime TaskEndDate { get; set; }
        public int TaskOwnerId { get; set; }

        public string TaskOwnerName { get; set; }
        public string TaskSubject { get; set; }
        public int BranchId { get; set; }
        public string BranchCode { get; set; }
        public string BranchName { get; set; }
        public int ProjectId { get; set; }
        public string ProjectYear { get; set; }
        public int ProjectSerial { get; set; }
        public string ProjectName { get; set; }
        public int TaskStatusId { get; set; }
        public string TaskStatusCode { get; set; }
        public string StatusName { get; set; }
        public int TaskResponsibleId { get; set; }
        public string TaskResponsibleName { get; set; }
        public string OtherInformation { get; set; }
        public string TaskResultDescription { get; set; }
        public string UserName { get; set; }
        public DateTime Time_Stamp { get; set; }
        public string Description { get; set; }

        public string TaskActionDetails { get; set; }
        public string TaskOperation { get; set; }
        public string TaskOwnerUserName { get; set; }

        public string strGroupBy { get; set; }

        public List<EmployeesInTaskViewModel> TaskTeam { get; set; }


    }
}
