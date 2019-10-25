using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSIS.ViewModels
{
    public class TaskCountByStatusViewModel
    {
        public int InProgressCount { get; set; }
        public int WaitingCount { get; set; }
        public int ApprovedCount { get; set; }
        public int RejectedCount { get; set; }
        public int DoneCount { get; set; }
    }
}
