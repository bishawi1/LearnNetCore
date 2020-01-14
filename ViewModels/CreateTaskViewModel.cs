using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MSIS.Models;
namespace MSIS.ViewModels
{
    public class CreateTaskViewModel:Tasks
    {
        [Column(TypeName = "Date")]
        public DateTime ContinueFromDate { get; set; }
        [Column(TypeName = "Date")]
        public DateTime ContinueToDate { get; set; }
        public byte PeriodId { get; set; }

        public List<Customer> Customers { get; set; }
        public List<Branch> Branches { get; set; }
        public List<Project> Projects { get; set; }
        public List<Employee> Employees { get; set; }
        public List<MSIS.Models.TaskStatus> TaskStatusList { get; set; }
        [NotMapped]
        public List<PeriodTypeModel> PeriodTypes { get; set; }


    }
}
