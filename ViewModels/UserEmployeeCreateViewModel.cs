using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TMS.Models;

namespace TMS.ViewModels
{
    public class UserEmployeeCreateViewModel:UserEmployee
    {
        public List<ApplicationUser> Users { get; set; }
        public List<Employee> Employees { get; set; }
    }
}
