using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MSIS.ViewModels
{
    public class RolePagesViewModels
    {
        public int PageId { get; set; }
        public int RolePageId { get; set; }
        public string ParentName { get; set; }
        public string PageName { get; set; }
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public Boolean CanAdd { get; set; }
        public Boolean CanView { get; set; }
        public Boolean CanEdit { get; set; }
        public bool CanDelete { get; set; }

    }
}
