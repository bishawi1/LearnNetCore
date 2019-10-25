using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MSIS.ViewModels
{
    public class EmployeesInTaskViewModel:Models.TaskTeam
    {
        public string EmployeeName { get; set; }
        public bool IsSelected { get; set; }

    }
}
