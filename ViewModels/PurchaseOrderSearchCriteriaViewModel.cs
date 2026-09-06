using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.CodeAnalysis;
using TMS.Models;

namespace TMS.ViewModels
{
    public class PurchaseOrderSearchCriteriaViewModel
    {
        public DateTime FromPurchaseOrderDate { get; set; }
        //[Column(TypeName = "Date")]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //[DataType(DataType.Date)]

        public DateTime ToPurchaseOrderDate { get; set; }
        public int EmployeeId { get; set; }
        public int SupplierId { get; set; }
        public int ProjectId { get; set; }
        public int BranchId { get; set; }
        public int StateId { get; set; }
        public string strGroupBy { get; set; }
        public int PurchaseOrderYear { get; set; }
        public int PurchaseOrderNo { get; set; }

        public bool ContinuousTask { get; set; }

        public List<Models.Project> Projects { get; set; }
        public List<Employee> Employees { get; set; }
        public List<Branch> Branches { get; set; }
        public List<Supplier> Suppliers { get; set; }
        public List<TMS.Models.PurchaseOrderStates> PurchaseOrderStates { get; set; }

    }
}
