using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TMS.Models;
using TMS.ViewModels;
using Newtonsoft.Json;

namespace TMS.Controllers
{
    public class PurchaseOrdersController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;

        public SQLPurchaseOrderRepository purchaseOrderRepository { get; }
        public IHostingEnvironment hostingEnvironment { get; }
       public PurchaseOrdersController(SQLPurchaseOrderRepository suppliersRepository, UserManager<ApplicationUser> userManager, IHostingEnvironment hostingEnvironment)
        {
            this.purchaseOrderRepository = suppliersRepository;
            this.userManager = userManager;
            this.hostingEnvironment = hostingEnvironment;
        }
        [HttpPost]
        public IActionResult ConfirmPurchaseOrder(int Id)
        {
            PurchaseOrder purchaseOrderChanges = purchaseOrderRepository.GetPurchaseOrder(Id);
            purchaseOrderRepository.ConfirmPurchaseOrder(purchaseOrderChanges);
            return RedirectToAction("Details", "PurchaseOrders",new { Id = Id});
        }
        [HttpPost]
        public IActionResult BackToNewPurchaseOrder(int Id)
        {
            PurchaseOrder purchaseOrderChanges = purchaseOrderRepository.GetPurchaseOrder(Id);
            purchaseOrderRepository.BackToNewPurchaseOrder(purchaseOrderChanges);
            return RedirectToAction("Details", "PurchaseOrders",new { Id = Id});
        }
        [HttpPost]
        public IActionResult waitPurchaseOrderForDelivery(int Id)
        {
            PurchaseOrder purchaseOrderChanges = purchaseOrderRepository.GetPurchaseOrder(Id);
            purchaseOrderRepository.waitPurchaseOrderForDelivery(purchaseOrderChanges);
            return RedirectToAction("ListPurchaseOrders", "PurchaseOrders");
        }
        [HttpPost]
        public IActionResult DeliverPurchaseOrder(int Id, int StateId,double Subtraction, string Description)
        {
            PurchaseOrder purchaseOrderChanges = purchaseOrderRepository.GetPurchaseOrder(Id);
            purchaseOrderChanges.SubtractionAmount = Subtraction;
            purchaseOrderChanges.SubtractNotes = Description;
            if (StateId == 0)
            {

                purchaseOrderRepository.DeleverPurchaseOrder(purchaseOrderChanges);
            }
            else if (StateId == 1)
            {
                purchaseOrderChanges.Description = Description;
                purchaseOrderRepository.DeleverPartialPurchaseOrder(purchaseOrderChanges);
            }
            else if (StateId == 2)
            {
                purchaseOrderChanges.Description = Description;
                purchaseOrderRepository.DeleverCancelPurchaseOrder(purchaseOrderChanges);
            }
            return RedirectToAction("ListPurchaseOrders", "PurchaseOrders");
        }
        [HttpPost]
        public IActionResult PayPurchaseOrder(int Id, int StateId,double Subtraction, string Description)
        {
            PurchaseOrder purchaseOrderChanges = purchaseOrderRepository.GetPurchaseOrder(Id);
            purchaseOrderChanges.SubtractionAmount = Subtraction;
            purchaseOrderChanges.SubtractNotes = Description;
            purchaseOrderRepository.PayPurchaseOrder(purchaseOrderChanges);

            return RedirectToAction("Edit", "PurchaseOrders", new { Id = Id });
        }

        [HttpPost]
        public IActionResult VerifyOrderExecution(int Id, int StateId, string Description)
        {
            PurchaseOrder purchaseOrderChanges = purchaseOrderRepository.GetPurchaseOrder(Id);
            if (StateId == 0)
            {
                purchaseOrderRepository.ApprovePurchaseOrder(purchaseOrderChanges);
            }
            else if (StateId == 1)
            {
                purchaseOrderChanges.Description = Description;
                purchaseOrderRepository.RejectPurchaseOrder(purchaseOrderChanges);
            }
            return RedirectToAction("ListPurchaseOrders", "PurchaseOrders");
        }
        [HttpGet]
        public IActionResult PurchaseOrderSearchAsync(string strGroupBy)
        {
            AppDBContext context = purchaseOrderRepository.getContext();
            TMS.ViewModels.PurchaseOrderSearchViewModel model = new ViewModels.PurchaseOrderSearchViewModel();
            SQLSettingsRepository settingRepository = new SQLSettingsRepository(context);
            SQLProjectRepository projectRepository = new SQLProjectRepository(context);
            SQLEmployeeRepository employeeRepository = new SQLEmployeeRepository(context);
            SQLBranchRepository branchRepository = new SQLBranchRepository(context);
            model.Projects = projectRepository.GetAllProjects().ToList();
            model.Projects.Insert(0, new Project
            {
                Id = -1,
                ProjectName = "Select Project"
            });
            model.Employees = employeeRepository.GetAllEmployees().ToList();
            model.Employees.Insert(0, new Employee
            {
                Id = -1,
                Name = "Select ..."
            });
            model.Branches = branchRepository.GetAllBranches().ToList();
            model.Branches.Insert(0, new Branch
            {
                Id = -1,
                Name = "Select ..."
            });
            model.suppliers =context.Suppliers.ToList();
            model.suppliers.Insert(0, new Supplier
            {
                Id = -1,
                SupplierName = "Select ..."
            });
            model.CurrencyList = context.Currency.ToList();
            model.CurrencyList.Insert(0, new Currency
            {
                Id = -1,
                CurrencyName = "Select ..."
            });

            model.PurchaseOrderStates = context.PurchaseOrderStates.ToList();
            model.PurchaseOrderStates.Insert(0, new PurchaseOrderState()
            {
                Id = -1,
                StateName = "Select ..."
            });
            model.FromDate = new DateTime(2019, 1, 1);
            model.ToDate = DateTime.Today;
            if (strGroupBy == null)
            {
                model.strGroupBy = "";
            }
            else
            {
                model.strGroupBy = strGroupBy;
            }

            return PartialView("_PurchaseOrderSearch", model);

        }
        [HttpPost]
        public async Task<IActionResult> PurchaseOrderSearchAsync(int CurrencyId,int StateId, int BranchId, int ProjectId, int SupplierId,int OrderNo,int OrderYear, DateTime FromDate, DateTime ToDate, string strGroupBy)
        {
            List<PurchaseOrderDetailsViewModel> model=new List<PurchaseOrderDetailsViewModel>();
            try
            {
                TMS.ViewModels.PurchaseOrderSearchViewModel criteria = new PurchaseOrderSearchViewModel();
                criteria.StateId = StateId;                
                criteria.ProjectId = ProjectId;
                criteria.BranchId = BranchId;
                criteria.CurrencyId = CurrencyId;
                criteria.SupplierId = SupplierId;
                criteria.OrderNo = OrderNo;
                criteria.OrderYear = OrderYear;          
                criteria.FromDate = FromDate;
                criteria.ToDate = ToDate;
                criteria.strGroupBy = strGroupBy;
                HttpContext.Session.SetString("searchOrderCriteria", JsonConvert.SerializeObject(criteria));

                model = purchaseOrderRepository.getAllPurchaseOrderDetails(criteria).ToList();
                //return new JsonResult( model);
                return PartialView("_PrintPurchaseOrderList", model);

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("PurchaseOrderSearchAsync", ex.Message.ToString());
                
            }

            return View(model);
        }
        private List<Supplier> retSuppliersList()
        {
            AppDBContext context = purchaseOrderRepository.getContext();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var user = userManager.Users.ToList().Where(x => x.Id == userId).FirstOrDefault();
            List<Supplier> SupplierList = null;
            //if (user.Category.ToLower() == "0")//employee
            //{
                SupplierList = context.Suppliers.ToList();
                SupplierList.Insert(0, new Supplier
                {
                    Id = -1,
                    SupplierName = "Select Supplier"
                });
                return SupplierList;


        }
        private List<Project> retProjectsList()
        {
            AppDBContext context = purchaseOrderRepository.getContext();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = userManager.Users.ToList().Where(x => x.Id == userId).FirstOrDefault();
            List<Project> ProjectList = null;
            if (user.Category.ToLower() == "0")//employee
            {
                ProjectList = context.Projects.ToList();
                ProjectList.Insert(0, new Project
                {
                    Id = -1,
                    ProjectName = "Select Project"
                });
                return ProjectList;
            }
            else
            {
                var result = (from project in context.Projects
                              join userProjects in context.UserProjects
                              on project.Id equals userProjects.ProjectId
                              where userProjects.UserId == userId
                              select new Project()
                              {
                                  Address = project.Address,
                                  Customer = project.Customer,
                                  Email = project.Email,
                                  EndDate = project.EndDate,
                                  Fax = project.Fax,
                                  Id = project.Id,
                                  MobileNo = project.MobileNo,
                                  OtheInformation = project.OtheInformation,
                                  ProjectName = project.ProjectName,
                                  ProjectOwner = project.ProjectOwner,
                                  ProjectSerial = project.ProjectSerial,
                                  ProjectYear = project.ProjectYear,
                                  StartDate = project.StartDate
                              }).ToList();
                return result;
            }

        } 
        private List<Branch> retBranchesList()
        {
            AppDBContext context = purchaseOrderRepository.getContext();
            int BranchId=0;
            string BranchName = "";
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = userManager.Users.ToList().Where(x => x.Id == userId).FirstOrDefault();
            List<Branch> BranchList = null;
            var purchaseOrderPermission = purchaseOrderRepository.context.PurchaseOrderPermissions.ToList().Where(x => x.UserId == userId).FirstOrDefault();
            if (purchaseOrderPermission != null)
            {
                BranchId = purchaseOrderPermission.BranchId;
            }
            if (BranchId == 0)
            {
                BranchList = context.Branches.ToList();
                BranchList.Insert(0, new Branch
                {
                    Id = -1,
                    Name = "Select Branch"
                });
                return BranchList;
            }
            else
            {
                BranchList = context.Branches.Where(x=>x.Id==BranchId).ToList();
                return BranchList;
            }
            //if (user.Category.ToLower() == "0")
            //{
            //    BranchList = context.Branches.ToList();
            //    BranchList.Insert(0, new Branch
            //    {
            //        Id = 0,
            //        Name = "Select Branch"
            //    });
            //    return BranchList;
            //}
            //else
            //{
            //    var result = (from branch in context.Branches
            //                  join userBranches in context.UserBranches
            //                  on branch.Id equals userBranches.BranchId
            //                  where userBranches.UserId == userId
            //                  select new Branch()
            //                  {
            //                      Address = branch.Address,
            //                      Code = branch.Code,
            //                      Id = branch.Id,
            //                      Mobile = branch.Mobile,
            //                      Name = branch.Name,
            //                      Phone = branch.Phone
            //                  }).ToList();
            //    return result;
            //}

        }
        private List<Employee> retEmployeesList()
        {
            AppDBContext context = purchaseOrderRepository.getContext();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = userManager.Users.ToList().Where(x => x.Id == userId).FirstOrDefault();
            List<Employee> employeeList = null;
            if (user.Category.ToLower() == "0")
            {
                employeeList = context.Employees.ToList();
                employeeList.Insert(0, new Employee
                {
                    Id = -1,
                    Name = "Select ...s"
                });
                return employeeList;
            }
            else
            {
                var result = (from employee in context.Employees
                              join userEmployees in context.UserEmployees
                              on employee.Id equals userEmployees.EmployeeId
                              where userEmployees.UserId == userId
                              select new Employee()
                              {
                                  Id = employee.Id,
                                  Name = employee.Name,
                                  Active = employee.Active,
                                  Address = employee.Address,
                                  Department = employee.Department,
                                  EducationDegree = employee.EducationDegree,
                                  Email = employee.Email,
                                  EmployeeNo = employee.EmployeeNo,
                                  IdentityNo = employee.IdentityNo,
                                  JobDescription = employee.JobDescription,
                                  MobileNo = employee.MobileNo,
                                  OtherInformation = employee.OtherInformation,
                                  PhoneNo = employee.PhoneNo,
                                  PhotoPath = employee.PhotoPath,
                                  Specialization = employee.Specialization,
                                  WorkMobileNo = employee.WorkMobileNo
                              }).ToList();
                return result;
            }

        }

        private PurchaseOrderSearchCriteriaViewModel retCriteria(PurchaseOrderSearchCriteriaViewModel criteria)
        {
            AppDBContext context = purchaseOrderRepository.getContext();
            TMS.ViewModels.PurchaseOrderSearchCriteriaViewModel model = new ViewModels.PurchaseOrderSearchCriteriaViewModel();
            SQLCustomerRepository customerRepository = new SQLCustomerRepository(context);
            SQLProjectRepository projectRepository = new SQLProjectRepository(context);
            SQLEmployeeRepository employeeRepository = new SQLEmployeeRepository(context);
            SQLBranchRepository branchRepository = new SQLBranchRepository(context);
            if (criteria != null)
            {
                model.Projects = retProjectsList();
                model.Suppliers = retSuppliersList();
                model.Branches = retBranchesList();
                model.Employees = retEmployeesList();
                model.FromPurchaseOrderDate = new DateTime(2019, 1, 1);
                //model.ContinuousTask = criteria.ContinuousTask;
                if (criteria.ProjectId > 0)
                {
                    model.ProjectId = criteria.ProjectId;
                }
                if (criteria.EmployeeId > 0)
                {
                    model.EmployeeId = criteria.EmployeeId;
                }
                if (criteria.SupplierId > 0)
                {
                    model.SupplierId = criteria.SupplierId;
                }
                if (criteria.BranchId > 0)
                {
                    model.BranchId = criteria.BranchId;
                }
                model.StateId = criteria.StateId;
                if (criteria.strGroupBy == null)
                {
                    model.strGroupBy = "";
                }
                else
                {
                    model.strGroupBy = criteria.strGroupBy;
                }
                if (criteria.FromPurchaseOrderDate.Year == 1)
                {
                    model.FromPurchaseOrderDate = new DateTime(2019, 1, 1);
                }
                if (criteria.ToPurchaseOrderDate.Year == 1)
                {
                    model.ToPurchaseOrderDate = DateTime.Today;
                }
                model.ToPurchaseOrderDate = DateTime.Today;
                return model;
            }
            else
            {
                if (criteria == null)
                {
                    criteria = new PurchaseOrderSearchCriteriaViewModel();
                }
                criteria.Projects = retProjectsList();
                criteria.Branches = retBranchesList();
                criteria.Employees = retEmployeesList();
                criteria.Suppliers = retSuppliersList();
                criteria.FromPurchaseOrderDate = new DateTime(2019, 1, 1);
                if (criteria.Projects.Count > 0)
                {
                    criteria.ProjectId = criteria.Projects[0].Id;
                }
                if (criteria.Branches.Count > 0)
                {
                    criteria.BranchId = criteria.Branches[0].Id;
                }
                if (criteria.Employees.Count > 0)
                {
                    criteria.EmployeeId = criteria.Employees[0].Id;
                }
                if (criteria.Suppliers.Count > 0)
                {
                    criteria.SupplierId = criteria.Suppliers[0].Id;
                }
                //criteria.TaskResponsibleId = criteria.Employees[0].Id;
                criteria.FromPurchaseOrderDate = new DateTime(2019, 1, 1);
                criteria.ToPurchaseOrderDate = DateTime.Today;
                return criteria;
            }
        }


        [HttpGet]
        public IActionResult ListPurchaseOrders()
        {
            bool hasCriteria = true;
            PurchaseOrderSearchCriteriaViewModel criteria = null;

            if (HttpContext.Session.GetString("searchOrderCriteria") == null)
            {
                hasCriteria = false;
            }
            else
            {
                criteria = JsonConvert.DeserializeObject<PurchaseOrderSearchCriteriaViewModel>(HttpContext.Session.GetString("searchOrderCriteria"));
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
//            TMS.ViewModels.UserPermissionsViewModel permission = purchaseOrderRepository.GetUserParentMenuPermission(userId, "All Purchase Orders");
            var purchaseOrderPermission = purchaseOrderRepository.context.PurchaseOrderPermissions.ToList().Where(x => x.UserId == userId).FirstOrDefault();
            var BranchId = 0;
            if (purchaseOrderPermission != null)
            {
                BranchId = purchaseOrderPermission.BranchId;
            }
            if (hasCriteria)
            {
                ListPurchaseOrdersViewModel model = purchaseOrderRepository.getPurchaseOrderList(criteria, userId, "All Purchase Orders");
                //model.userPermission = permission.UserPermissions[0];
                model.criteria = retCriteria(criteria);
                return View(model);
            }
            else
            {
                criteria= retCriteria(criteria);
                ListPurchaseOrdersViewModel model = purchaseOrderRepository.getPurchaseOrderList(criteria, userId, "All Purchase Orders");
                model.criteria = retCriteria(criteria);
                //model.userPermission = permission.UserPermissions[0];
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PurchaseOrdersSearchAsync(int EmployeeId, int SupplierId, int ProjectId, DateTime FromPurchaseOrderDate, DateTime ToPurchaseOrderDate, int BranchId)
        {
            TMS.ViewModels.PurchaseOrderSearchCriteriaViewModel criteria = new PurchaseOrderSearchCriteriaViewModel();
            criteria.ProjectId = ProjectId;
            criteria.BranchId = BranchId;
            criteria.EmployeeId = EmployeeId;
            criteria.SupplierId = SupplierId;
            criteria.FromPurchaseOrderDate = FromPurchaseOrderDate;
            criteria.ToPurchaseOrderDate = ToPurchaseOrderDate;
            //criteria.ContinuousTask = ContinuousTask;
            //criteria.strGroupBy = strGroupBy;

            HttpContext.Session.SetString("searchOrderCriteria", JsonConvert.SerializeObject(criteria));

            //ListPurchaseOrdersViewModel model = new ListPurchaseOrdersViewModel();
            var user = await userManager.FindByNameAsync(User.Identity.Name);

//            TMS.ViewModels.UserPermissionsViewModel permission = purchaseOrderRepository.GetUserParentMenuPermission(user.Id, "All Purchase Orders");
            var purchaseOrderPermission = purchaseOrderRepository.context.PurchaseOrderPermissions.ToList().Where(x => x.UserId == user.Id).FirstOrDefault();
            //var BranchId = 0;
            //if (purchaseOrderPermission != null)
            //{
            //    BranchId = purchaseOrderPermission.BranchId;
            //}


            //if (await userManager.IsInRoleAsync(user, "Admin"))
            //{
            //    model.TaskDetails = purchaseOrderRepository.getPurchaseOrderList(criteria, true).ToList();
            //    model.CountByStatus = model.CountByStatus = await getTaskStatusCount(EmployeeId, SupplierId, ProjectId, BranchId, FromPurchaseOrderDate, ToPurchaseOrderDate, strGroupBy);
            //    return PartialView("_ListTasks", model);
            //}
            //else
            //{
            ListPurchaseOrdersViewModel model = purchaseOrderRepository.getPurchaseOrderList(criteria,  user.Id, "All Purchase Orders");
            //model.userPermission = permission.UserPermissions[0];
            model.criteria = criteria;
            return PartialView("_ListPurchaseOrders", model);
            //}

        }



        [HttpGet]
        public IActionResult ListNewPurchaseOrders(int EmployeeId, int SupplierId, int ProjectId, DateTime FromPurchaseOrderDate, DateTime ToPurchaseOrderDate, int BranchId)
        {
            TMS.ViewModels.PurchaseOrderSearchCriteriaViewModel criteria = new PurchaseOrderSearchCriteriaViewModel();
            criteria.ProjectId = ProjectId;
            criteria.EmployeeId = EmployeeId;
            criteria.SupplierId = SupplierId;
            criteria.FromPurchaseOrderDate = FromPurchaseOrderDate;
            criteria.ToPurchaseOrderDate = ToPurchaseOrderDate;
            criteria.BranchId = BranchId;

            criteria.StateId = 1;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
//            TMS.ViewModels.UserPermissionsViewModel permission = purchaseOrderRepository.GetUserParentMenuPermission(userId, "All Purchase Orders");
            var purchaseOrderPermission = purchaseOrderRepository.context.PurchaseOrderPermissions.ToList().Where(x => x.UserId == userId).FirstOrDefault();
            if (purchaseOrderPermission != null)
            {
                BranchId = purchaseOrderPermission.BranchId;
            }
            ListPurchaseOrdersViewModel model = purchaseOrderRepository.getPurchaseOrderList(criteria, userId, "All Purchase Orders");
//            model.userPermission = permission.UserPermissions[0];
            return PartialView("_ListPurchaseOrders", model);
        }

        [HttpGet]
        public IActionResult ListConfirmedPurchaseOrders(int EmployeeId, int SupplierId, int ProjectId, DateTime FromPurchaseOrderDate, DateTime ToPurchaseOrderDate, int BranchId)
        {
            TMS.ViewModels.PurchaseOrderSearchCriteriaViewModel criteria = new PurchaseOrderSearchCriteriaViewModel();
            criteria.ProjectId = ProjectId;
            criteria.EmployeeId = EmployeeId;
            criteria.SupplierId = SupplierId;
            criteria.FromPurchaseOrderDate = FromPurchaseOrderDate;
            criteria.ToPurchaseOrderDate = ToPurchaseOrderDate;
            criteria.BranchId = BranchId;
            criteria.StateId = 2;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
//            TMS.ViewModels.UserPermissionsViewModel permission = purchaseOrderRepository.GetUserParentMenuPermission(userId, "All Purchase Orders");
            var purchaseOrderPermission = purchaseOrderRepository.context.PurchaseOrderPermissions.ToList().Where(x => x.UserId == userId).FirstOrDefault();
            if (purchaseOrderPermission != null)
            {
                BranchId = purchaseOrderPermission.BranchId;
            }
            ListPurchaseOrdersViewModel model = purchaseOrderRepository.getPurchaseOrderList(criteria, userId, "All Purchase Orders");
//            model.userPermission = permission.UserPermissions[0];
            return PartialView("_ListPurchaseOrders", model);
        }

        [HttpGet]
        public IActionResult ListRejectedPurchaseOrders(int EmployeeId, int SupplierId, int ProjectId, DateTime FromPurchaseOrderDate, DateTime ToPurchaseOrderDate, int BranchId)
        {
            TMS.ViewModels.PurchaseOrderSearchCriteriaViewModel criteria = new PurchaseOrderSearchCriteriaViewModel();
            criteria.ProjectId = ProjectId;
            criteria.EmployeeId = EmployeeId;
            criteria.SupplierId = SupplierId;
            criteria.FromPurchaseOrderDate = FromPurchaseOrderDate;
            criteria.ToPurchaseOrderDate = ToPurchaseOrderDate;
            criteria.BranchId = BranchId;
            criteria.StateId = 3;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
//            TMS.ViewModels.UserPermissionsViewModel permission = purchaseOrderRepository.GetUserParentMenuPermission(userId, "All Purchase Orders");
            var purchaseOrderPermission = purchaseOrderRepository.context.PurchaseOrderPermissions.ToList().Where(x => x.UserId == userId).FirstOrDefault();
            if (purchaseOrderPermission != null)
            {
                BranchId = purchaseOrderPermission.BranchId;
            }
            ListPurchaseOrdersViewModel model = purchaseOrderRepository.getPurchaseOrderList(criteria,  userId, "All Purchase Orders");
//            model.userPermission = permission.UserPermissions[0];
            return PartialView("_ListPurchaseOrders", model);
        }
        [HttpGet]
        public IActionResult ListApprovedPurchaseOrders(int EmployeeId, int SupplierId, int ProjectId, DateTime FromPurchaseOrderDate, DateTime ToPurchaseOrderDate, int BranchId)
        {
            TMS.ViewModels.PurchaseOrderSearchCriteriaViewModel criteria = new PurchaseOrderSearchCriteriaViewModel();
            criteria.ProjectId = ProjectId;
            criteria.EmployeeId = EmployeeId;
            criteria.SupplierId = SupplierId;
            criteria.FromPurchaseOrderDate = FromPurchaseOrderDate;
            criteria.ToPurchaseOrderDate = ToPurchaseOrderDate;
            criteria.BranchId = BranchId;
            criteria.StateId = 4;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
//            TMS.ViewModels.UserPermissionsViewModel permission = purchaseOrderRepository.GetUserParentMenuPermission(userId, "All Purchase Orders");
            var purchaseOrderPermission = purchaseOrderRepository.context.PurchaseOrderPermissions.ToList().Where(x => x.UserId == userId).FirstOrDefault();
            if (purchaseOrderPermission != null)
            {
                BranchId = purchaseOrderPermission.BranchId;
            }
            ListPurchaseOrdersViewModel model = purchaseOrderRepository.getPurchaseOrderList(criteria, userId, "All Purchase Orders");
//            model.userPermission = permission.UserPermissions[0];
            return PartialView("_ListPurchaseOrders", model);
        }
        [HttpGet]
        public IActionResult ListDeliveredPurchaseOrders(int EmployeeId, int SupplierId, int ProjectId, DateTime FromPurchaseOrderDate, DateTime ToPurchaseOrderDate, int BranchId)
        {
            TMS.ViewModels.PurchaseOrderSearchCriteriaViewModel criteria = new PurchaseOrderSearchCriteriaViewModel();
            criteria.ProjectId = ProjectId;
            criteria.EmployeeId = EmployeeId;
            criteria.SupplierId = SupplierId;
            criteria.FromPurchaseOrderDate = FromPurchaseOrderDate;
            criteria.ToPurchaseOrderDate = ToPurchaseOrderDate;
            criteria.BranchId = BranchId;
            criteria.StateId = 6;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //TMS.ViewModels.UserPermissionsViewModel permission = purchaseOrderRepository.GetUserParentMenuPermission(userId, "All Purchase Orders");
            var purchaseOrderPermission = purchaseOrderRepository.context.PurchaseOrderPermissions.ToList().Where(x => x.UserId == userId).FirstOrDefault();
            if (purchaseOrderPermission != null)
            {
                BranchId = purchaseOrderPermission.BranchId;
            }
            ListPurchaseOrdersViewModel model = purchaseOrderRepository.getPurchaseOrderList(criteria, userId, "All Purchase Orders");
//            model.userPermission = permission.UserPermissions[0];
            return PartialView("_ListPurchaseOrders", model);
        }
        [HttpGet]
        public IActionResult ListPayedPurchaseOrders(int EmployeeId, int SupplierId, int ProjectId, DateTime FromPurchaseOrderDate, DateTime ToPurchaseOrderDate, int BranchId)
        {
            TMS.ViewModels.PurchaseOrderSearchCriteriaViewModel criteria = new PurchaseOrderSearchCriteriaViewModel();
            criteria.ProjectId = ProjectId;
            criteria.EmployeeId = EmployeeId;
            criteria.SupplierId = SupplierId;
            criteria.FromPurchaseOrderDate = FromPurchaseOrderDate;
            criteria.ToPurchaseOrderDate = ToPurchaseOrderDate;
            criteria.BranchId = BranchId;
            criteria.StateId = 9;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
//            TMS.ViewModels.UserPermissionsViewModel permission = purchaseOrderRepository.GetUserParentMenuPermission(userId, "All Purchase Orders");
            var purchaseOrderPermission = purchaseOrderRepository.context.PurchaseOrderPermissions.ToList().Where(x => x.UserId == userId).FirstOrDefault();
            if (purchaseOrderPermission != null)
            {
                BranchId = purchaseOrderPermission.BranchId;
            }
            ListPurchaseOrdersViewModel model = purchaseOrderRepository.getPurchaseOrderList(criteria,  userId, "All Purchase Orders");
//            model.userPermission = permission.UserPermissions[0];
            return PartialView("_ListPurchaseOrders", model);
        }


        [HttpGet]
        public IActionResult ListDeliveredPartialyPurchaseOrders(int EmployeeId, int SupplierId, int ProjectId, DateTime FromPurchaseOrderDate, DateTime ToPurchaseOrderDate, int BranchId)
        {
            TMS.ViewModels.PurchaseOrderSearchCriteriaViewModel criteria = new PurchaseOrderSearchCriteriaViewModel();
            criteria.ProjectId = ProjectId;
            criteria.EmployeeId = EmployeeId;
            criteria.SupplierId = SupplierId;
            criteria.FromPurchaseOrderDate = FromPurchaseOrderDate;
            criteria.ToPurchaseOrderDate = ToPurchaseOrderDate;
            criteria.BranchId = BranchId;
            criteria.StateId = 7;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
//            TMS.ViewModels.UserPermissionsViewModel permission = purchaseOrderRepository.GetUserParentMenuPermission(userId, "All Purchase Orders");
            var purchaseOrderPermission = purchaseOrderRepository.context.PurchaseOrderPermissions.ToList().Where(x => x.UserId == userId).FirstOrDefault();
            if (purchaseOrderPermission != null)
            {
                BranchId = purchaseOrderPermission.BranchId;
            }
            ListPurchaseOrdersViewModel model = purchaseOrderRepository.getPurchaseOrderList(criteria,userId, "All Purchase Orders");
//            model.userPermission = permission.UserPermissions[0];
            return PartialView("_ListPurchaseOrders", model);
        }
        [HttpGet]
        public IActionResult ListWaitingForDeliveryPurchaseOrders(int EmployeeId, int SupplierId, int ProjectId, DateTime FromPurchaseOrderDate, DateTime ToPurchaseOrderDate, int BranchId)
        {
            TMS.ViewModels.PurchaseOrderSearchCriteriaViewModel criteria = new PurchaseOrderSearchCriteriaViewModel();
            criteria.ProjectId = ProjectId;
            criteria.EmployeeId = EmployeeId;
            criteria.SupplierId = SupplierId;
            criteria.FromPurchaseOrderDate = FromPurchaseOrderDate;
            criteria.ToPurchaseOrderDate = ToPurchaseOrderDate;
            criteria.BranchId = BranchId;
            criteria.StateId = 5;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
//            TMS.ViewModels.UserPermissionsViewModel permission = purchaseOrderRepository.GetUserParentMenuPermission(userId, "All Purchase Orders");
            var purchaseOrderPermission = purchaseOrderRepository.context.PurchaseOrderPermissions.ToList().Where(x => x.UserId == userId).FirstOrDefault();
            if (purchaseOrderPermission != null)
            {
                BranchId = purchaseOrderPermission.BranchId;
            }
            ListPurchaseOrdersViewModel model = purchaseOrderRepository.getPurchaseOrderList(criteria,userId, "All Purchase Orders");

//            model.userPermission = permission.UserPermissions[0];
            return PartialView("_ListPurchaseOrders", model);
        }
        [HttpGet]
        public IActionResult ListAllPurchaseOrders()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
//            TMS.ViewModels.UserPermissionsViewModel permission = purchaseOrderRepository.GetUserParentMenuPermission(userId, "All Purchase Orders");
            var purchaseOrderPermission = purchaseOrderRepository.context.PurchaseOrderPermissions.ToList().Where(x => x.UserId == userId).FirstOrDefault();
            var BranchId = 0;
            if (purchaseOrderPermission != null)
            {
                BranchId = purchaseOrderPermission.BranchId;
            }
            ListPurchaseOrdersViewModel model = purchaseOrderRepository.getPurchaseOrderList(new TMS.ViewModels.PurchaseOrderSearchCriteriaViewModel { BranchId = -1, EmployeeId = -1, SupplierId = -1 }, userId, "All Purchase Orders");
//            model.userPermission = permission.UserPermissions[0];
            return PartialView("_ListPurchaseOrders", model);
        }
        [HttpPost]
        public IActionResult Delete(int Id)
        {
            ListPurchaseOrdersViewModel model = new ListPurchaseOrdersViewModel();
            PurchaseOrder purchaseOrder = purchaseOrderRepository.GetPurchaseOrder(Id);
            if(purchaseOrder == null){
                return View("NotFound");
            }else
            {
                purchaseOrderRepository.DeletePurchaseOrder(Id);
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
//                TMS.ViewModels.UserPermissionsViewModel permission = purchaseOrderRepository.GetUserParentMenuPermission(userId, "All Purchase Orders");
                var purchaseOrderPermission = purchaseOrderRepository.context.PurchaseOrderPermissions.ToList().Where(x => x.UserId == userId).FirstOrDefault();
                var BranchId = 0;
                if (purchaseOrderPermission != null)
                {
                    BranchId = purchaseOrderPermission.BranchId;
                }
                model = purchaseOrderRepository.getPurchaseOrderList(new TMS.ViewModels.PurchaseOrderSearchCriteriaViewModel { BranchId = -1, EmployeeId = -1, SupplierId = -1 }, userId, "All Purchase Orders");
//                model.userPermission = permission.UserPermissions[0];
            }

            return new JsonResult(model);// RedirectToAction("ListPurchaseOrders","PurchaseOrders" );
        }
        [HttpGet]
        public IActionResult Details(int Id)
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            TMS.ViewModels.UserPermissionsViewModel permission= purchaseOrderRepository.GetUserParentMenuPermission(userId, "All Purchase Orders");
            var model = purchaseOrderRepository.getPurchaseOrderDetails(Id);
            if (permission.UserPermissions.Count > 0)
            {
                model.Permission = permission.UserPermissions[0];
            }
            var context = purchaseOrderRepository.context;
            PurchaseOrderPermission purchaseOrderPermission = context.PurchaseOrderPermissions.Where(x => x.UserId == userId).FirstOrDefault();
            model.purchaseOrderPermission = purchaseOrderPermission;
            model.Attachments = context.PurchaseOrderAttachments.Where(x => x.PurchaseOrderId == Id).ToList();
            return View(model);
        }
        [HttpGet]
        public IActionResult printPurchaseOrderForm(int Id)
        {
            PurchaseOrder purchaseOrderChanges = purchaseOrderRepository.GetPurchaseOrder(Id);
            if (purchaseOrderChanges.StateId == 4) { 
                purchaseOrderRepository.waitPurchaseOrderForDelivery(purchaseOrderChanges);
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            TMS.ViewModels.UserPermissionsViewModel permission = purchaseOrderRepository.GetUserParentMenuPermission(userId, "All Purchase Orders");
            var model = purchaseOrderRepository.getPurchaseOrderDetails(Id);
            if (permission.UserPermissions.Count > 0)
            {
                model.Permission = permission.UserPermissions[0];
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult printPurchaseOrderList(int Id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
//            TMS.ViewModels.UserPermissionsViewModel permission = purchaseOrderRepository.GetUserParentMenuPermission(userId, "All Purchase Orders");
            var purchaseOrderPermission = purchaseOrderRepository.context.PurchaseOrderPermissions.ToList().Where(x => x.UserId == userId).FirstOrDefault();
            var BranchId = 0;
            if (purchaseOrderPermission != null)
            {
                BranchId = purchaseOrderPermission.BranchId;
            }

            ListPurchaseOrdersViewModel model = purchaseOrderRepository.getPurchaseOrderList(new TMS.ViewModels.PurchaseOrderSearchCriteriaViewModel { BranchId = -1, EmployeeId = -1, SupplierId = -1 }, userId, "All Purchase Orders");
//            model.userPermission = permission.UserPermissions[0];
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            CreatePurchaseOrderViewModel purchaseOrder =  purchaseOrderRepository.getCreatePurchaseOrderDetails(userId);
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
                model.StateId = 1;
                model.PurchaseOrderYear = DateTime.Today.Year;
                model.PurchaseOrderCode = model.PurchaseOrderYear + "/" + model.PurchaseOrderNo;
                model.SupplierId = purchaseOrder.PurchaseOrderDetails.SupplierId;
                model.BranchId = purchaseOrder.PurchaseOrderDetails.BranchId;
                model.ProjectId = purchaseOrder.PurchaseOrderDetails.ProjectId;
                model.EmployeeId = purchaseOrder.PurchaseOrderDetails.EmployeeId;
                model.Notes = purchaseOrder.PurchaseOrderDetails.Notes;
                model.User_Name = User.Identity.Name;
                model.DeliveryDate = purchaseOrder.PurchaseOrderDetails.DeliveryDate;
                var newModel= purchaseOrderRepository.Add(model);
                return RedirectToAction("Edit", "PurchaseOrders",new { Id=newModel.Id});
            }
            return View();
        }

        [HttpPost]
        public IActionResult CreateAjax(CreatePurchaseOrderViewModel purchaseOrder)
        {
            if (ModelState.IsValid)
            {
                PurchaseOrder model = new PurchaseOrder();
                model.CurrencyId = purchaseOrder.PurchaseOrderDetails.CurrencyId;
                model.CurrencyRate = purchaseOrder.PurchaseOrderDetails.CurrencyRate;
                model.PurchaseOrderNo = purchaseOrderRepository.retPurchaseOrderNo();
                model.PurchaseOrderDate = purchaseOrder.PurchaseOrderDetails.PurchaseOrderDate;
                model.StateId = 1;
                model.PurchaseOrderYear = DateTime.Today.Year;
                model.PurchaseOrderCode = model.PurchaseOrderYear + "/" + model.PurchaseOrderNo;
                model.SupplierId = purchaseOrder.PurchaseOrderDetails.SupplierId;
                model.BranchId = purchaseOrder.PurchaseOrderDetails.BranchId;
                model.ProjectId = purchaseOrder.PurchaseOrderDetails.ProjectId;
                model.EmployeeId= purchaseOrder.PurchaseOrderDetails.EmployeeId;
                model.Notes = purchaseOrder.PurchaseOrderDetails.Notes;
                model.User_Name = User.Identity.Name;
                model.DeliveryDate = purchaseOrder.PurchaseOrderDetails.DeliveryDate;
                var newModel = purchaseOrderRepository.Add(model);
                return new JsonResult(newModel.Id);
                //return RedirectToAction("Edit", "PurchaseOrders", new { Id = newModel.Id });
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
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var context = purchaseOrderRepository.context;
                PurchaseOrderPermission purchaseOrderPermission = context.PurchaseOrderPermissions.Where(x => x.UserId == userId).FirstOrDefault();
                purchaseOrder.purchaseOrderPermission = purchaseOrderPermission;

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
        public IActionResult EditPurchase(int Id,DateTime PurchaseOrderDate,int SupplierId, int BranchId,int ProjectId, int EmployeeId, int CurrencyId, float CurrencyRate,string Notes,List<Models.PurchaseOrderDetails> data)
        {
            if (ModelState.IsValid)
            {
                PurchaseOrder model = purchaseOrderRepository.GetPurchaseOrder(Id);
                if (model == null)
                {

                }
                else
                {
                    model.ProjectId = ProjectId;
                    model.BranchId = BranchId;
                    model.EmployeeId = EmployeeId;
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
        public IActionResult AddPurchase( DateTime PurchaseOrderDate, int SupplierId, int BranchId,int ProjectId, int EmployeeId, int CurrencyId, float CurrencyRate, string Notes, List<Models.PurchaseOrderDetails> data)
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
                    model.BranchId = BranchId;
                    model.EmployeeId = EmployeeId;
                    model.ProjectId = ProjectId;
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
                return new JsonResult(model);

                //return PartialView("_PurchaseOrderItems", model);

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


        //--------------------------  Attachments
        [HttpGet]
        public IActionResult AddAttachment(int Id)
        {
            PurchaseOrderAddAttachmentViewModel model = new PurchaseOrderAddAttachmentViewModel();
            model.PurchaseOrderId = Id;
            return View(model);
        }
        [HttpPost]
        public IActionResult AddAttachment(PurchaseOrderAddAttachmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                    string uniqueFileName = null;
                    if (model.Photos != null && model.Photos.Count > 0)
                    {
                        foreach (IFormFile photo in model.Photos)
                        {
                            string UploadFolders = Path.Combine(hostingEnvironment.WebRootPath, "images/PurchaeOrders");
                            uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(photo.FileName);// model.Photo.Name;
                            string filePath = Path.Combine(UploadFolders, uniqueFileName);
                            photo.CopyTo(new FileStream(filePath, FileMode.Create));
                        }
                    }

                    PurchaseOrderAttachment NewAttachment = new PurchaseOrderAttachment()
                    {
                        Caption = model.Caption,
                        Description = model.Description,
                        PurchaseOrderId = model.PurchaseOrderId,
                        URL = uniqueFileName
                    };
                    purchaseOrderRepository.AddAttachment(NewAttachment);
                    return RedirectToAction("Details", new { Id = NewAttachment.PurchaseOrderId });

            }
            return View();
        }

    }
}