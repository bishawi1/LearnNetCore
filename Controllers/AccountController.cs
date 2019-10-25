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
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IEmployeeRepository employeeRepository;

        public AccountController(UserManager<ApplicationUser> userManager,
                                 SignInManager<ApplicationUser> signInManager,
                                 IEmployeeRepository employeeRepository)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.employeeRepository = employeeRepository;
        }

        

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            var model = new RegisterViewModel();
            model.Employees = employeeRepository.GetAllEmployees().ToList();
            
            return View(model);
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
                    if(signInManager.IsSignedIn(User) && User.IsInRole("Admin"))
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
                    {
                        if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                        {
                            //return LocalRedirect( ReturnUrl);
                            return LocalRedirect(ReturnUrl);
                        }
                        else
                        {
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