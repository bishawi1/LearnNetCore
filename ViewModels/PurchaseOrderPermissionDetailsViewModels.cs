using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MSIS.Models;

namespace MSIS.ViewModels
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
