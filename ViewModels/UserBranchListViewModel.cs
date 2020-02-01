using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MSIS.Models;

namespace MSIS.ViewModels
{
    public class UserBranchListViewModel
    {
        public string UserId { get; set; }

        public List<SQLUserBranchesViewModel> UserBranches { get; set; }
        [NotMapped]
        public UserPermissionDetailsViewModel userPermission { get; set; }
        public List<Branch> Branches { get; set; }
    }
}
