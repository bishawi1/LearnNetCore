using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MSIS.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MSIS.ViewModels
{
    [NotMapped]
    public class ChangeUserPasswordViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }

        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public List<UserViewModel> UserList { get; set; }

    }
}
