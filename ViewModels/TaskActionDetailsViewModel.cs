using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MSIS.ViewModels
{
    [NotMapped]
    public class TaskActionDetailsViewModel:Models.TaskAction
    {
        public string TaskStatusName { get; set; }
        public string CurrentTaskStatusName { get; set; }

    }
}
