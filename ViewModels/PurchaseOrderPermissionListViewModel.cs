using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MSIS.Models;

namespace MSIS.ViewModels
{
    public class PurchaseOrderPermissionListViewModel:PurchaseOrderPermission
    {
        public string UserName { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string BranchName { get; set; }


    }
}
