using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MSIS.ViewModels
{
    [NotMapped]
    public class TaskCountByStatusViewModel
    {
        [Key]
        public int TaskId { get; set; }
        public int InProgressCount { get; set; }
        public int WaitingCount { get; set; }
        public int ApprovedCount { get; set; }
        public int RejectedCount { get; set; }
        public int DoneCount { get; set; }
    }
}
