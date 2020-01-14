using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MSIS.ViewModels
{
    [NotMapped]
    public class SQLActiveContinuousTasks
    {
        [Key]
        public int Id { get; set; }
        public int TaskId { get; set; }
        public DateTime TaskDate { get; set; }
        public DateTime TaskStartDate { get; set; }
        public DateTime TaskEndDate { get; set; }
        public int TaskOwnerId { get; set; }
        public string TaskSubject { get; set; }
        public int BranchId { get; set; }
        public int ProjectId { get; set; }
        public int TaskStatusId { get; set; }
        public int TaskResponsibleId { get; set; }
        public string OtherInformation { get; set; }
        public string TaskResultDescription { get; set; }
        public string UserName { get; set; }
        public DateTime Time_Stamp { get; set; }
        public string Description { get; set; }
        public string TaskStartTime { get; set; }
        public string TaskEndTime { get; set; }
        public bool ContinuousTask { get; set; }
        public DateTime ContinueFromDate { get; set; }
        public DateTime ContinueToDate { get; set; }
        public byte PeriodId { get; set; }
        public string Notes { get; set; }
        public DateTime NextTaskDate { get; set; }

    }
}
