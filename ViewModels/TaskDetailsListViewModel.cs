using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MSIS.ViewModels
{
    public class TaskDetailsListViewModel
    {
        public List<TaskDetailsViewModel> TaskDetails { get; set; }

        public TaskCountByStatusViewModel CountByStatus { get; set; }
        public SearchTaskViewModel criteria { get; set; }

    }
}
