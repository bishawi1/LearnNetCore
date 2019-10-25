using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSIS.ViewModels
{
    public class EditTaskStatusViewModel
    {
        public int Id { get; set; }
        public DateTime TransactionDate { get; set; }
        public int TaskStatusID { get; set; }
        public string UserName { get; set; }
        public string TaskOperation { get; set; }

        public string Description { get; set; }

    }
}
