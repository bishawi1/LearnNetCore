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

        public AdministrationController(RoleManager<IdentityRole> roleManager,
                                 UserManager<ApplicationUser> userManager,
                                 IEmployeeRepository employeeRepository)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.employeeRepository = employeeRepository;
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
                RoleName = role.Name
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

        [HttpGet]
        [Authorize(Roles = "Admin")]
     //[Authorize]
        public IActionResult ListUsers()
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
                return RedirectToAction("Home", "Index");
            }
            else
            {
                ModelState.AddModelError("passwordError", result.Errors.ToList().ToString());
                return View();
            }
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
                return View("NotFound");
            }
            var userClaims = await userManager.GetClaimsAsync(user);
            var userRoles = await userManager.GetRolesAsync(user);
            var employees = employeeRepository.GetAllEmployees().ToList();

            var model = new EditUserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                City = user.City,
                Claims = userClaims.Select(c => c.Value).ToList(),
                Employees=employees,
                EmployeeId=user.EmployeeId,
                Roles = userRoles
            };

            return View(model);
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
       // [Authorize]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            var user = await userManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {model.Id} cannot be found";
                return View("NotFound");
            }
            else
            {
                user.UserName = model.UserName;
                user.City = model.City;
                user.Email = model.Email;
                user.EmployeeId = model.EmployeeId;
                var result = await userManager.UpdateAsync(user);
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
                    return View(model);
                };
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

    }
}
