using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MSIS.ViewModels;
namespace MSIS.Controllers
{
    public class UtilityController : Controller
    {
        public IActionResult showModal()
        {
            DialogModalViewModel model = new DialogModalViewModel()
            {
                CancelButtonText = "cancel",
                Message = "Delete this Record",
                OkButtonText = "Delete",
                Title = "confirm delete"
            };
            return PartialView("_showModal", model);
        }
    }
}