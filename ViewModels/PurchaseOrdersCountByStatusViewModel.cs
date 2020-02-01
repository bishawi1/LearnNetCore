using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MSIS.ViewModels
{
    public class PurchaseOrdersCountByStatusViewModel
    {
        [Key]
        public  int Id { get; set; }
        public int NewOrdersCount { get; set; }
        public int ConfirmedOrdersCount { get; set; }
        public int RejectedOrdersCount { get; set; }
        public int ApprovedOrdersCount { get; set; }
        public int WaitForDeliveryCount { get; set; }
        public int DeliveredCount { get; set; }
        public int DeliveredPartialyCount { get; set; }
        public int PayedOrderCount { get; set; }

    }
}
