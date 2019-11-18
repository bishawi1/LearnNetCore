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
    public class PurchaseOrdersController : Controller
    {
        public SQLPurchaseOrderRepository purchaseOrderRepository { get; }
        public IHostingEnvironment hostingEnvironment { get; }
       public PurchaseOrdersController(SQLPurchaseOrderRepository suppliersRepository, IHostingEnvironment hostingEnvironment)
        {
            this.purchaseOrderRepository = suppliersRepository;
            this.hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public IActionResult ListPurchaseOrders()
        { 
            List<ListPurchaseOrderDetailsViewModel> model= purchaseOrderRepository.getPurchaseOrderList();
            return View(model);
        }
        [HttpGet]
        public IActionResult Details(int Id)
        {
            var model = purchaseOrderRepository.getPurchaseOrderDetails(Id);
            return View(model);
        }
        [HttpGet]
        public IActionResult Create()
        {
            CreatePurchaseOrderViewModel purchaseOrder =  purchaseOrderRepository.getCreatePurchaseOrderDetails();
            if (purchaseOrder == null)
            {
                return Redirect("NotFound");
            }
            else
            {
                return View(purchaseOrder);
            }

            return View();
        }
        [HttpPost]
        public IActionResult Create(CreatePurchaseOrderViewModel purchaseOrder)
        {
            if (ModelState.IsValid)
            {
                PurchaseOrder model = new PurchaseOrder();
                model.CurrencyId = purchaseOrder.PurchaseOrderDetails.CurrencyId;
                model.CurrencyRate = purchaseOrder.PurchaseOrderDetails.CurrencyRate;
                model.PurchaseOrderNo = purchaseOrderRepository.retPurchaseOrderNo();
                model.PurchaseOrderDate = purchaseOrder.PurchaseOrderDetails.PurchaseOrderDate;
                model.PurchaseOrderYear = DateTime.Today.Year;
                model.PurchaseOrderCode = model.PurchaseOrderYear + "/" + model.PurchaseOrderNo;
                model.SupplierId = purchaseOrder.PurchaseOrderDetails.SupplierId;
                model.Notes = purchaseOrder.PurchaseOrderDetails.Notes;
                model.User_Name = User.Identity.Name;
                var newModel= purchaseOrderRepository.Add(model);
                return RedirectToAction("Edit", "PurchaseOrders",new { Id=newModel.Id});
            }
            return View();
        }


        public IActionResult Edit(int Id)
        {
            EditPurchaseOrderViewModel purchaseOrder = purchaseOrderRepository.getEditPurchaseOrderDetails(Id);             
            if (purchaseOrder == null)
            {
                return Redirect("NotFound");
            }
            else
            {
                return View(purchaseOrder);
            }
        }
        [HttpPost]
        public IActionResult Edit1111(EditPurchaseOrderViewModel purchaseOrderChanges)
        {
            if (ModelState.IsValid)
            {
                PurchaseOrder purchaseOrder = purchaseOrderRepository.GetPurchaseOrder(purchaseOrderChanges.PurchaseOrderDetails.Id);
                if (purchaseOrder == null)
                {
                    return Redirect("NotFound");
                }
                else
                {

                    purchaseOrder.CurrencyId = purchaseOrderChanges.PurchaseOrderDetails.CurrencyId;
                    purchaseOrder.CurrencyRate = purchaseOrderChanges.PurchaseOrderDetails.CurrencyRate;
                    purchaseOrder.Notes = purchaseOrderChanges.PurchaseOrderDetails.Notes;
                    //purchaseOrder.PurchaseOrderNo = purchaseOrderChanges.PurchaseOrderDetails.PurchaseOrderNo;
                    //purchaseOrder.PurchaseOrderYear = purchaseOrderChanges.PurchaseOrderDetails.PurchaseOrderYear;
                    purchaseOrder.PurchaseOrderDate = purchaseOrderChanges.PurchaseOrderDetails.PurchaseOrderDate;
                    //purchaseOrder.PurchaseOrderCode = purchaseOrderChanges.PurchaseOrderDetails.PurchaseOrderYear.ToString()+purchaseOrderChanges.PurchaseOrderDetails.PurchaseOrderNo.ToString();

                    purchaseOrder.SupplierId = purchaseOrderChanges.PurchaseOrderDetails.SupplierId;
                    purchaseOrderRepository.Update(purchaseOrder);

                    return RedirectToAction("ListPurchaseOrders", "PurchaseOrders");
                }
            }
            return View(purchaseOrderChanges);
        }
        [HttpPost]
        public IActionResult Edit(EditPurchaseOrderViewModel purchaseOrderChanges)
        {
            {
                if (ModelState.IsValid)
                {
                    PurchaseOrder purchaseOrder = purchaseOrderRepository.GetPurchaseOrder(purchaseOrderChanges.PurchaseOrderDetails.Id);
                    if (purchaseOrder == null)
                    {
                        return Redirect("NotFound");
                    }
                    else
                    {

                        purchaseOrder.CurrencyId = purchaseOrderChanges.PurchaseOrderDetails.CurrencyId;
                        purchaseOrder.CurrencyRate = purchaseOrderChanges.PurchaseOrderDetails.CurrencyRate;
                        purchaseOrder.Notes = purchaseOrderChanges.PurchaseOrderDetails.Notes;
                        //purchaseOrder.PurchaseOrderNo = purchaseOrderChanges.PurchaseOrderDetails.PurchaseOrderNo;
                        //purchaseOrder.PurchaseOrderYear = purchaseOrderChanges.PurchaseOrderDetails.PurchaseOrderYear;
                        purchaseOrder.PurchaseOrderDate = purchaseOrderChanges.PurchaseOrderDetails.PurchaseOrderDate;
                        //purchaseOrder.PurchaseOrderCode = purchaseOrderChanges.PurchaseOrderDetails.PurchaseOrderYear.ToString()+purchaseOrderChanges.PurchaseOrderDetails.PurchaseOrderNo.ToString();

                        purchaseOrder.SupplierId = purchaseOrderChanges.PurchaseOrderDetails.SupplierId;
                        purchaseOrderRepository.Update(purchaseOrder);

                        return RedirectToAction("ListPurchaseOrders", "PurchaseOrders");
                    }
                }
                return View(purchaseOrderChanges);
            }
        }
        [HttpPost]
        public IActionResult EditPurchase(int Id,DateTime PurchaseOrderDate,int SupplierId, int CurrencyId, float CurrencyRate,string Notes,List<Models.PurchaseOrderDetails> data)
        {
            if (ModelState.IsValid)
            {
                PurchaseOrder model = purchaseOrderRepository.GetPurchaseOrder(Id);
                if (model == null)
                {

                }
                else
                {
                    model.CurrencyId = CurrencyId;
                    model.CurrencyRate = CurrencyRate;
                    model.Notes = Notes;
                    model.PurchaseOrderDate = PurchaseOrderDate;
                    model.SupplierId = SupplierId;
                    purchaseOrderRepository.Update(model);
                }
                return View();
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public IActionResult AddPurchase( DateTime PurchaseOrderDate, int SupplierId, int CurrencyId, float CurrencyRate, string Notes, List<Models.PurchaseOrderDetails> data)
        {
            if (ModelState.IsValid)
            {
                PurchaseOrder savedModel=null;
                PurchaseOrder model = new PurchaseOrder();
                if (model == null)
                {

                }
                else
                {
                    model.CurrencyId = CurrencyId;
                    model.CurrencyRate = CurrencyRate;
                    model.Notes = Notes;
                    model.PurchaseOrderDate = PurchaseOrderDate;
                    model.SupplierId = SupplierId;
                    savedModel= purchaseOrderRepository.Add(model);
                }
                return new JsonResult("{Id : "+ savedModel.Id + ",Done:" + true + "}");
            }
            else
            {
                return new JsonResult(false);
            }
        }

        [HttpGet]
        //[Route("PurchaceOrderItems/{PurchaseOrderId}")]
        public IActionResult PurchaceOrderItems(int PurchaseOrderId)
        {
            PurchaseOrderDetails model = purchaseOrderRepository.GetPurchaseOrderItem(PurchaseOrderId);
            return new JsonResult(model);
        }
        [HttpPost]
        public IActionResult EditItem(int Id,string Description, string ItemName, int ItemUnitId, int PuchaseOrderId, float QNT, float UnitPrice )
        {
            if (ModelState.IsValid)
            {
                PurchaseOrderDetails purchaseOrder = purchaseOrderRepository.GetPurchaseOrderItem(Id);
                if (purchaseOrder == null)
                {
                    return Redirect("NotFound");
                }
                else
                {
                    purchaseOrder.Description = Description;
                    purchaseOrder.ItemName = ItemName;
                    purchaseOrder.ItemUnitId = ItemUnitId;
                    //purchaseOrder.PuchaseOrderId = PuchaseOrderId;
                    purchaseOrder.QNT = QNT;
                    purchaseOrder.UnitPrice = UnitPrice;
                    purchaseOrderRepository.UpdatePurchaseOrderItem(purchaseOrder);
                    
                    
                    List<PurchaseOrderItemsViewModel> model = purchaseOrderRepository.getPurchaseOrderItems(PuchaseOrderId);
                    return new JsonResult( model);
                    //return PartialView("_PurchaseOrderItems", model);

                }
            }
            return new JsonResult(false);
        }
        [HttpPost]
        public IActionResult DeleteItem(int Id)
        {
                //PurchaseOrderDetails purchaseOrder = purchaseOrderRepository.DeletePurchaseOrderItem(Id);
                PurchaseOrderDetails purchase = purchaseOrderRepository.DeletePurchaseOrderItem(Id);
                if (purchase == null)
                {
                    return Redirect("NotFound");
                }
                else
                {
                //return new JsonResult("{Deleted:true,ErrorText:''}");
                List<PurchaseOrderItemsViewModel> model = purchaseOrderRepository.getPurchaseOrderItems(purchase.PuchaseOrderId);
                return PartialView("_PurchaseOrderItems", model);

                }
        }

        [HttpPost]
        public IActionResult AddItem(int Id, string Description, string ItemName, int ItemUnitId, int PuchaseOrderId, float QNT, float UnitPrice)
        {
                PurchaseOrderDetails purchaseOrder = new PurchaseOrderDetails();
                    purchaseOrder.Description = Description;
                    purchaseOrder.ItemName = ItemName;
                    purchaseOrder.ItemUnitId = ItemUnitId;
                    purchaseOrder.PuchaseOrderId = PuchaseOrderId;
                    purchaseOrder.QNT = QNT;
                    purchaseOrder.UnitPrice = UnitPrice;
                    purchaseOrderRepository.AddPurchaseOrderItem(purchaseOrder);


                    List<PurchaseOrderItemsViewModel> model = purchaseOrderRepository.getPurchaseOrderItems(PuchaseOrderId);
                    return new JsonResult(model);
                    //return PartialView("_PurchaseOrderItems", model);
        }
        [HttpGet]
        public IActionResult getItem(int Id)
        {
            PurchaseOrderItemsViewModel model;
            model= purchaseOrderRepository.getPurchaseOrderItemDetails(Id);
            return new JsonResult(model);
        }

    }
}