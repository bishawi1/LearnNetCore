using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MSIS.ViewModels
{
    public class OfferListViewModels
    {
        public List<MSIS.ViewModels.SQLOffersViewModel> OfferList { get; set; }
        //public List<OfferViewModel> OfferList { get; set; }
        [NotMapped]
        public UserPermissionDetailsViewModel userPermission { get; set; }
        public List<OffersTotalsViewModel> OffersTotals { get; set; }

    }
}
