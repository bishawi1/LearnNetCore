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
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IEmployeeRepository employeeRepository;

        public AccountController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
                                 SignInManager<ApplicationUser> signInManager,
                                 IEmployeeRepository employeeRepository)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
            this.employeeRepository = employeeRepository;
        }

        List<Employee> getReadyEmployees()
        {
            var Employees = employeeRepository.GetAllEmployees().ToList().Where(x => !this.userManager.Users.Any(y => y.EmployeeId == x.Id)).ToList();
            return Employees;
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
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user =await userManager.FindByEmailAsync(model.Email);
                //var token1 = await userManager.GenerateEmailConfirmationTokenAsync(user);
                if(user !=null && await userManager.IsEmailConfirmedAsync(user))
                {
                    string token = "";
                    try
                    {
                        token =  userManager.GeneratePasswordResetTokenAsync(user).Result;
                        var passwordResetLink = Url.Action("ResetPassword", "Account", new { email = model.Email, token = token }, Request.Scheme);
                    } catch(Exception ex)
                    {
                        ModelState.AddModelError("",ex.Message);
                    }
                    return View("ForgotPasswordConfirmation");
                }
                return View("ForgotPasswordConfirmation");
            }
            return View(model);
        }

        //[HttpGet]
        //[AllowAnonymous]
        //public IActionResult ResetPassword(string token, string email)
        //{
        //    if (token == null || email == null)
        //    {
        //        ModelState.AddModelError("", "Invalid Password Reset token");
        //    }
        //    return View();
        //}
        private async Task <Models.ApplicationUser> GetUser(string UserId)
        {
            var user = await userManager.FindByIdAsync(UserId);
            return user;
        }
            
        //[HttpPost]
        //[AllowAnonymous]
        //public async Task <IActionResult> ResetPasswordInformal(ResetPasswordViewModel model)
        //{
        //    var user = await GetUser(model.UserId);// await userManager.FindByIdAsync(model.UserId);

        //    string strToken="";
        //     try
        //    {
        //        strToken= await userManager.GeneratePasswordResetTokenAsync(user);
        //    } catch(Exception ex)
        //    {

        //    }
                
        //    var result = await userManager.ResetPasswordAsync(user, strToken, model.Password);
        //    if (result.Succeeded)
        //    {
        //        return View("ResetPasswordConfirmation");
        //    }
        //    foreach (var error in result.Errors)
        //    {
        //        ModelState.AddModelError("", error.Description);
        //    }
        //    return View(model);
        //}

        [HttpGet]
        public IActionResult ResetPassword()
        {
            ResetPasswordViewModel model = new ResetPasswordViewModel();
            model.Users = userManager.Users.ToList();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult>  ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByIdAsync(model.UserId);
                if (user != null)
                {
                    model.Token=  userManager.GeneratePasswordResetTokenAsync(user).Result;
                    var result = await userManager.ResetPasswordAsync(user, model.Token, model.Password);
                    if (result.Succeeded)
                    {
                        return View("ResetPasswordConfirmation");
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("",error.Description);
                    }
                    return View(model);
                }
                return RedirectToAction("ListUsers", "Administration");
                //return View("ResetPasswordConfirmation");
            }
            return View(model);
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            var model = new RegisterViewModel();
            model.Employees = getReadyEmployees();// employeeRepository.GetAllEmployees().ToList();
            model.Employees.Insert(0,new Employee() { 
            Id=-1,
            Name="Select ...",
            });
            model.RoleList = roleManager.Roles.ToList();
            return View(model);
        }
        private async Task<bool> AddUserToRole(string RoleId,ApplicationUser user)
        {
            bool boolSave = false;
            IdentityResult Result;
            var role = await roleManager.FindByIdAsync(RoleId);
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
                        boolSave = true;
                        //Result = await userManager.AddToRoleAsync(user, role.Name);
                        //if (Result.Succeeded)
                        //{
                        //    boolSave = true;
                        //}
                        //else
                        //{
                        //    boolSave = false;
                        //    //foreach (var error in Result.Errors)
                        //    //{
                        //    //    ModelState.AddModelError("", error.Description);
                        //    //}
                        //    //return View(model);
                        //}
                    }
                    else
                    {
                        boolSave = false;
                        //foreach (var error in Result.Errors)
                        //{
                        //    ModelState.AddModelError("", error.Description);
                        //}
                        //return View(model);

                    }
                }
                else
                {
                    boolSave = true;
                    //Result = await userManager.AddToRoleAsync(user, role.Name);
                    //if (Result.Succeeded)
                    //{
                    //    boolSave = true;
                    //}
                    //else
                    //{
                    //    boolSave = false;
                    //    //foreach (var error in Result.Errors)
                    //    //{
                    //    //    ModelState.AddModelError("", error.Description);
                    //    //}
                    //    //return View(model);
                    //}

                }
                if (boolSave)
                {
                    Result = await userManager.AddToRoleAsync(user, role.Name);
                    if (Result.Succeeded)
                    {
                        boolSave = true;
                    }
                    else
                    {
                        boolSave = false;
                        //foreach (var error in Result.Errors)
                        //{
                        //    ModelState.AddModelError("", error.Description);
                        //}
                        //return View(model);
                    }
                }
            }
            return boolSave;
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    UserName = model.Email,
                    Email = model.Email,
                    City=model.City,
                    EmployeeId=model.EmployeeId
                };
               var result= await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var token = userManager.GenerateEmailConfirmationTokenAsync(user);

                    var role = await roleManager.FindByIdAsync(model.RoleId);
                    result = await userManager.AddToRoleAsync(user, role.Name);
                    if (signInManager.IsSignedIn(User) && User.IsInRole("Admin"))
                    {
                        return RedirectToAction("ListUsers", "Administration");
                    }
                    else
                    {
                        await signInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToAction("index", "home");
                    }
                }
                foreach(var err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        //[HttpPost][HttpGet]
        [AcceptVerbs("Get","Post")]
        [AllowAnonymous]
        public async Task<IActionResult> IsEmailInUse(string email)
        {
            var user= await userManager.FindByEmailAsync(email);
            if(user== null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Email {email} is already in use");
            } 
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await signInManager.PasswordSignInAsync(model.Email, model.Password,
                                                                            model.RememberMe, false);
                    if (result.Succeeded)
                    {;

                        if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                        {
                            //return LocalRedirect( ReturnUrl);
                            return LocalRedirect(ReturnUrl);
                        }
                        else
                        {
                            //return RedirectToAction("PortalIndex", "home");
                            return RedirectToAction("index", "home");
                        }

                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid Login Attempt");
                    }

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message.ToString());

                }

            }
            
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}