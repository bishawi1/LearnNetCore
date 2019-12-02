using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MSIS.Models;
using MSIS.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MSIS.Controllers
{
    //[Authorize(Roles ="Admin,User")]
    //[Authorize(Roles = "User")]
    //[Authorize(Roles = "Admin")]
    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IEmployeeRepository employeeRepository;
        private readonly SQLSettingsRepository settingsRepository;

        public AdministrationController(RoleManager<IdentityRole> roleManager,
                                 UserManager<ApplicationUser> userManager,
                                 IEmployeeRepository employeeRepository, SQLSettingsRepository settingsRepository)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.employeeRepository = employeeRepository;
            this.settingsRepository = settingsRepository;
        }
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        public async Task <IActionResult> CreateRole(CreateRoleViewModal model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };
                IdentityResult result=await roleManager.CreateAsync(identityRole);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles", "administration");
                }
                foreach(IdentityError err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
            }

            return View(model);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        //[Authorize]
        public IActionResult ListRoles()
        {
            var roles = roleManager.Roles.ToList();
            return View(roles);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
                return View("NotFound");
            }
            else
            {
                var result = await roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles", "Administration");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View("ListRoles");
                }
            }
        }
        [HttpGet]
        public async Task<IActionResult> EditRole(string id) 
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role With Id {id} cannot be found";
                return View("notFound");
            }
            EditRoleViewModel model = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name,
                Pages= settingsRepository.getPages().ToList()
            };
            foreach(var user in userManager.Users)
            {
                if(await userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            };
            
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await roleManager.FindByIdAsync(model.Id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role With Id {model.Id} cannot be found";
                return View("notFound");
            }
            else
            {
                role.Name = model.RoleName;
                IdentityResult result = await roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles", "Administration");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(model);
                }
            }
        }
        [HttpGet]
        public async Task<IActionResult> EditUserInRole(string roleId)
        {
            ViewBag.RoleID = roleId;
            var role= await roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
                return View("NotFound");
            }
            var model = new List<UserRoleViewModel>();
            foreach (var user in userManager.Users)
            {
                var userRoleViewModel = new UserRoleViewModel()
                {
                    UserID=user.Id,
                    UserName=user.UserName
                };
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else {
                    userRoleViewModel.IsSelected = false;
                };
                model.Add(userRoleViewModel);
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditUserInRole(List<UserRoleViewModel> model, string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
                return View("NotFound");
            }
            else
            {
                for(int i=0; i < model.Count; i++)
                {
                    IdentityResult result = null;
                    var user=await userManager.FindByIdAsync(model[i].UserID);
                    if (model[i].IsSelected && !(await userManager.IsInRoleAsync(user,role.Name)))
                    {
                        result = await userManager.AddToRoleAsync(user, role.Name);

                    }
                    else if (!model[i].IsSelected && (await userManager.IsInRoleAsync(user, role.Name)))
                    {
                        result= await userManager.RemoveFromRoleAsync(user, role.Name);
                    }
                    else
                    {
                        continue;
                    }
                    if (result.Succeeded)
                    {
                        if (i < model.Count - 1)
                        {
                            continue;
                        }
                        else
                        {
                            return RedirectToAction("EditRole", "Administration",new { id=roleId});
                        };
                    };
                };

                return RedirectToAction("EditRole", "Administration", new { id = roleId });
            }
        }
        private async Task<UserViewModel>  getUserRole(UserViewModel user)
        {
            bool Found = false;
            foreach(var role in roleManager.Roles)
            {
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    Found = true;
                }
                if (Found)
                {
                    user. AppRoleId = role.Id;
                    user.AppRoleName = role.Name;
                    break;
                }

            }

            return user;
        }
        private async Task<UserViewModel> getUserRole(string UserId)
        {
            bool Found = false;
            var user = await userManager.FindByIdAsync(UserId);
            UserViewModel result = new UserViewModel();
            foreach (var role in roleManager.Roles)
            {
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    Found = true;
                }
                if (Found)
                {
                    result.AppRoleId = role.Id;
                    result.AppRoleName = role.Name;
                    break;
                }

            }

            return result;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
     //[Authorize]
        public async Task<IActionResult> ListUsers()
        {
            var users =(from user in userManager.Users 
                        join employee in employeeRepository.GetAllEmployees()
                        on user.EmployeeId equals employee.Id
                        select new UserViewModel { 
                        City=user.City,
                        Email=user.Email,
                        EmployeeId=user.EmployeeId,
                        EmployeeName=employee.Name,
                        Id=user.Id, 
                        UserName=user.UserName
                        } ).ToList();
            foreach(UserViewModel user in users)
            {
                UserViewModel oUser;
                oUser = await getUserRole(user);
                user.AppRoleId = oUser.AppRoleId;
                user.AppRoleName = oUser.AppRoleName;
            };
            return View(users);
        }
        [HttpGet]
        public IActionResult ChangeUserPassword()
        {
            var users = (from user in userManager.Users
                         join employee in employeeRepository.GetAllEmployees()
                         on user.EmployeeId equals employee.Id
                         select new UserViewModel
                         {
                             City = user.City,
                             Email = user.Email,
                             EmployeeId = user.EmployeeId,
                             EmployeeName = employee.Name,
                             Id = user.Id,
                             UserName = user.UserName
                         }).ToList();
            ChangeUserPasswordViewModel model = new ChangeUserPasswordViewModel();
            model.UserList = users;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ChangeUserPassword(ChangeUserPasswordViewModel model)
        {
            var user =await userManager.FindByEmailAsync(model.Email);
            var result= await userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            if (result.Succeeded)
            {
                return RedirectToAction( "Index","Home");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                //ModelState.AddModelError("passwordError", result.Errors.ToList().ToString());
                return View();
            }
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditUser(string id)
        {
            ApplicationUser user=null;

            try
            {
            user = await userManager.FindByIdAsync(id);

            }catch(Exception ex)
            {
                throw ex;
            };
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
                return View("NotFound");
            }
            var userClaims = await userManager.GetClaimsAsync(user);
            var userRoles = await userManager.GetRolesAsync(user);
            var employees = employeeRepository.GetAllEmployees().ToList();
            var roleList= roleManager.Roles.ToList();
            UserViewModel ouser = await getUserRole(user.Id);
            var model = new EditUserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                City = user.City,
                Claims = userClaims.Select(c => c.Value).ToList(),
                Employees = employees,
                EmployeeId = user.EmployeeId,
                RoleList = roleList,
                RoleId = ouser.AppRoleId,
                Roles = userRoles
                
            };

            return View(model);
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        // [Authorize]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            var boolSave = false;
            IdentityResult Result;
            
            var user = await userManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {model.Id} cannot be found";
                return View("NotFound");
            }
            else
            {
                var role = await roleManager.FindByIdAsync(model.RoleId);
                var boolUserInRole = await userManager.IsInRoleAsync(user, role.Name);
                if (boolUserInRole)
                {
                    boolSave = true;
                }
                else
                {
                    var CurrentUserRole = await getUserRole(user.Id);
                    if (CurrentUserRole.AppRoleId != null)
                    {
                        Result = await userManager.RemoveFromRoleAsync(user, CurrentUserRole.AppRoleName);
                        if (Result.Succeeded)
                        {
                            Result = await userManager.AddToRoleAsync(user, role.Name);
                            if (Result.Succeeded)
                            {
                                boolSave = true;
                            }
                            else
                            {
                                foreach (var error in Result.Errors)
                                {
                                    ModelState.AddModelError("", error.Description);
                                }
                                return View(model);
                            }
                        }
                        else
                        {
                            foreach (var error in Result.Errors)
                            {
                                ModelState.AddModelError("", error.Description);
                            }
                            return View(model);

                        }
                    }
                    else
                    {
                        Result = await userManager.AddToRoleAsync(user, role.Name);
                        if (Result.Succeeded)
                        {
                            boolSave = true;
                        }
                        else
                        {
                            foreach (var error in Result.Errors)
                            {
                                ModelState.AddModelError("", error.Description);
                            }
                            return View(model);
                        }

                    }

                }
            }
            if (boolSave)
            {
                user.UserName = model.UserName;
                user.City = model.City;
                user.Email = model.Email;
                user.EmployeeId = model.EmployeeId;
                Result = await userManager.UpdateAsync(user);
                if (Result.Succeeded)
                {
                    return RedirectToAction("ListUsers", "Administration");
                }
                else
                {
                    foreach (var error in Result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }
        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
                return View("NotFound");
            }
            else
            {
                var result= await userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListUsers", "Administration");
                }
                else
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View("ListUsers");
                }
            }
        }


        [HttpGet]
        public IActionResult RolePages(string RoleId)
        {
            List<RolePagesViewModels> model = settingsRepository.getRolePages(RoleId).ToList();
            return PartialView("_RolePages",model);
        }

        [HttpPost]
        public IActionResult AddRolePage(string RoleId, int PageId,Boolean CanView, Boolean CanAdd, Boolean CanEdit, Boolean CanDelete )
        {
            RolePage rolePage = new RolePage();
            rolePage.CanAdd = CanAdd;
            rolePage.CanDelete = CanDelete;
            rolePage.CanEdit = CanEdit;
            rolePage.CanView = CanView;
            rolePage.PageId = PageId;
            rolePage.RoleId = RoleId;
            settingsRepository.AddRolePage(rolePage);

            List<RolePagesViewModels> model = settingsRepository.getRolePages(RoleId).ToList();
            return PartialView("_RolePages", model);
            //return PartialView("_PurchaseOrderItems", model);
        }

        [HttpPost]
        public IActionResult verifyRolePages( string RoleId)
        {
            Boolean roleVerified = settingsRepository.verifyRolePages(RoleId);
            if (!roleVerified )
            {
                ViewBag.ErrorMessage = $"RolePAge with Id = {RoleId} cannot be Verified";
                return View("NotFound");
            }
            else
            {
                List<RolePagesViewModels> model = settingsRepository.getRolePages(RoleId).ToList();
                return PartialView("_RolePages", model);
            }

            //return PartialView("_PurchaseOrderItems", model);
        }
        [HttpPost]
        public IActionResult EditRolePage(int Id, string RoleId, int PageId,Boolean CanView, Boolean CanAdd, Boolean CanEdit, Boolean CanDelete )
        {
            RolePage rolePage = settingsRepository.GetRolePage(Id);
            if (rolePage == null)
            {
                ViewBag.ErrorMessage = $"RolePAge with Id = {Id} cannot be found";
                return View("NotFound");
            }
            else
            {
                rolePage.CanAdd = CanAdd;
                rolePage.CanDelete = CanDelete;
                rolePage.CanEdit = CanEdit;
                rolePage.CanView = CanView;
                rolePage.PageId = PageId;
                rolePage.RoleId = RoleId;
                settingsRepository.UpdateRolePage(rolePage);
                List<RolePagesViewModels> model = settingsRepository.getRolePages(RoleId).ToList();
                return PartialView("_RolePages", model);
            }

            //return PartialView("_PurchaseOrderItems", model);
        }

        [HttpGet]
        public IActionResult getRolePage(int Id,string RoleId)
        {
            RolePagesViewModels model = settingsRepository.getRolePageViewModel(Id, RoleId);
            return new JsonResult(model);
        }

    }
}
