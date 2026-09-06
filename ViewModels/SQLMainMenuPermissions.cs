using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TMS.Models;
namespace TMS.ViewModels
{
    public class SQLMainMenuPermissions 
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string MainMenuText { get; set; }
        [Required]
        public string MainMenuUri { get; set; }
        [NotMapped]
        public List<SubMenu> SubMenus { get; set; }
        public string RoleId { get; set; }
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int EmployeeId { get; set; }
        public string Category { get; set; }

    }
}
