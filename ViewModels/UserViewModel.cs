using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMS.Models;
namespace TMS.ViewModels
{
    public class UserViewModel:ApplicationUser
    {
        public string EmployeeName { get; set; }
        public string AppRoleId { get; set; }
        public string AppRoleName { get; set; }


    }
}
