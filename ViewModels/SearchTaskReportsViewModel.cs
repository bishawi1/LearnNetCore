using MSIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSIS.ViewModels
{
    public class SearchTaskReportsViewModel
    {
        //[Column(TypeName = "Date")]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //[DataType(DataType.Date)]
        public DateTime FromTaskDate { get; set; }
        //[Column(TypeName = "Date")]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //[DataType(DataType.Date)]

        public DateTime ToTaskDate { get; set; }
        public int TaskOwnerId { get; set; }
        public int TaskResponsibleId { get; set; }
        public int ProjectId { get; set; }
        public int TaskStatusId { get; set; }
        public string strGroupBy { get; set; }
        public int BranchId { get; set; }

        public List<Project> Projects { get; set; }
        public List<Employee> Employees { get; set; }
        public List<Branch> Branches { get; set; }
        public List<MSIS.Models.TaskStatus> TaskStatsus { get; set; }

    }
}
