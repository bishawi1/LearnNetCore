using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TMS.ViewModels
{
    public class CreateRoleViewModal
    {
        [Required]
        public string RoleName { get; set; }

    }
}
