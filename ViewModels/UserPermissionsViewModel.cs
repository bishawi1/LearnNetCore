using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TMS.Models;
namespace TMS.ViewModels
{
    public class UserPermissionsViewModel
    {
        public List<string> ParentMenus { get; set; }
        public List<UserPermissionDetailsViewModel> UserPermissions { get; set; }
        public List<MainMenu> Menus { get; set; }


    }
}
