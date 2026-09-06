using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TMS.Models;
namespace TMS.ViewModels
{
    public class MainItemsDetailViewModel
    {

        public MainItemViewModel MainItem { get; set; }
        [NotMapped]
        public UserPermissionDetailsViewModel Permission { get; set; }
    }
}
