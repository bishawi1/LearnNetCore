using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TMS.Models;

namespace TMS.ViewModels
{
    public class UserProjectCreateViewModel: UserProject
    {
        public List<ApplicationUser> Users { get; set; }
        public List<Project> Projects { get; set; }

    }
}
