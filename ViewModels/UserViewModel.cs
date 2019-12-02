using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MSIS.Models;
namespace MSIS.ViewModels
{
    public class UserViewModel:ApplicationUser
    {
        public string EmployeeName { get; set; }
        public string AppRoleId { get; set; }
        public string AppRoleName { get; set; }


    }
}
