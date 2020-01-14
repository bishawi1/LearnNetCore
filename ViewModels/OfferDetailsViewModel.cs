using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSIS.ViewModels
{
    public class OfferDetailsViewModel:OfferViewModel
    {
        public List<OfferItemsViewModel> OfferItems { get; set; }
        [NotMapped]
        public UserPermissionDetailsViewModel Permission { get; set; }

        public double TotalAmount { get; set; }

    }
}
