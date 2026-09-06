using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TMS.Models;
namespace TMS.ViewModels
{
    public class ListProjectsViewModels
    {
        public List<ProjectDetailViewModel> Projects { get; set; }
        [NotMapped]
        public UserPermissionDetailsViewModel userPermission { get; set; }
    }
}
