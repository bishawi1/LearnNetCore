using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MSIS.Models;
using MSIS.ViewModels;
namespace MSIS.Controllers
{
    public class OffersController : Controller
    {
        public SQLOffersRepository OffersRepository { get; }
        public IHostingEnvironment HostingEnvironment { get; }
        public OffersController(SQLOffersRepository offersRepository, IHostingEnvironment hostingEnvironment)
        {
            this.OffersRepository = offersRepository;
            this.HostingEnvironment = hostingEnvironment;
        }


        [HttpGet]
        public IActionResult ListOffers()
        {
            List<OfferViewModel> model = OffersRepository.getOfferList().ToList();
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            CreateOfferViewModel model = OffersRepository.NewOffer();
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(CreateOfferViewModel offer)
        {
            if (ModelState.IsValid)
            {
                Offer model = new Offer();
                model.CurrencyId = offer.CurrencyId;
                model.CustomerId = offer.CustomerId;
                model.OfferDate = offer.OfferDate;
                model.OtherInformation = offer.OtherInformation;
                var newModel = OffersRepository.Add(model);
                return RedirectToAction("Edit", "Offers", new { Id = newModel.Id });
            }
            return View();
        }

        [HttpGet]
        public IActionResult Details(int Id)
        {
            OfferDetailsViewModel model = OffersRepository.getOffersDetails(Id);
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            EditOfferViewModel offer = OffersRepository.getEditOffers(Id);
            if (offer == null)
            {
                return Redirect("NotFound");
            }
            else
            {
                return View(offer);
            }
        }
        [HttpPost]
        public IActionResult EditOffer(int Id, DateTime OfferDate, int CustomerId, int CurrencyId, string Notes)
        {
            if (ModelState.IsValid)
            {
                Offer model = OffersRepository.GetOffer(Id);
                if (model == null)
                {

                }
                else
                {
                    model.CurrencyId = CurrencyId;
                    model.OtherInformation = Notes;
                    model.OfferDate = OfferDate;
                    model.CustomerId = CustomerId;
                    OffersRepository.Update(model);
                }
                return View();
            }
            else
            {
                return View();
            }
        }


    }
}