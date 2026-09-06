using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TMS.Models
{
    public class MainMenu
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string MainMenuText { get; set; }
        [Required]
        public string MainMenuUri { get; set; }
        public List<SubMenu> SubMenus { get; set; }

    }
}
