using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MSIS.ViewModels
{
    public class SQLUserBranchesViewModel
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Category { get; set; }
        public int BranchId { get; set; }
        public string BranchName { get; set; }

    }
}
