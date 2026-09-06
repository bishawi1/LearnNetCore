using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TMS.Models;

namespace TMS.ViewModels
{
    public class PurchaseOrderPermissionDetailsViewModels
    {
        public PurchaseOrderPermission purchaseOrderPermission { get; set; }
        [NotMapped]
        public UserPermissionDetailsViewModel Permission { get; set; }
        public string UserName { get; set; }
        public string BranchName { get; set; }

    }
}
