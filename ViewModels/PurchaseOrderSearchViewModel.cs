using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MSIS.ViewModels
{
    [NotMapped]
    public class PurchaseOrderSearchViewModel
    {
        [NotMapped]
        public int SupplierId { get; set; }
        [NotMapped]
        public int ProjectId { get; set; }
        [NotMapped]
        public int BranchId { get; set; }
        [NotMapped]
        public int EmployeeId { get; set; }
        [NotMapped]
        public int CurrencyId { get; set; }
        [NotMapped]
        public int OrderYear { get; set; }
        [NotMapped]
        public int OrderNo { get; set; }
        [NotMapped]
        public DateTime FromDate { get; set; }
        [NotMapped]
        public DateTime ToDate { get; set; }
        [NotMapped]
        public string strGroupBy { get; set; }
        [NotMapped]
        public int StateId { get; set; }

        [NotMapped]
        public List<Models.Supplier> suppliers { get; set; }
        public List<Models.Project> Projects { get; set; }
        public List<Models.Branch> Branches { get; set; }
        public List<Models.Employee> Employees { get; set; }
        public List<Models.PurchaseOrderState> PurchaseOrderStates { get; set; }

        [NotMapped]
        public List<Models.Currency> CurrencyList { get; set; }
    }
}
