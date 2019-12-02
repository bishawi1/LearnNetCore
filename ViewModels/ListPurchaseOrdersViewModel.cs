using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MSIS.ViewModels
{
    public class ListPurchaseOrdersViewModel
    {
        public List<ListPurchaseOrderDetailsViewModel> ListPurchaseOrders { get; set; }
        [NotMapped]
        public UserPermissionDetailsViewModel userPermission { get; set; }

    }
}
