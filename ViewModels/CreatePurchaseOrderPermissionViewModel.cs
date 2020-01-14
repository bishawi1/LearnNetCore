using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MSIS.Models;

namespace MSIS.ViewModels
{
    public class CreatePurchaseOrderPermissionViewModel:PurchaseOrderPermission
    {
        public List<Models.ApplicationUser> Users { get; set; }
        public List<Models.Branch> Branches { get; set; }

    }
}
