using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TMS.Models;
using TMS.ViewModels;
using System;
using System.Collections.Generic;

using System.Security.Claims;
using System.Threading.Tasks;

namespace TMS.Components
{

    public class DefaultViewComponent:ViewComponent
    {
        private readonly SQLSettingsRepository settingsRepository;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly UserManager<ApplicationUser> userManager;

        public DefaultViewComponent(SQLSettingsRepository settingsRepository, IHostingEnvironment hostingEnvironment, UserManager<ApplicationUser> userManager)
        {
            this.settingsRepository = settingsRepository;
            this.hostingEnvironment = hostingEnvironment;
            this.userManager = userManager;
        }

        public IViewComponentResult Invoke()
        {
            using (var db = settingsRepository.getContext())
            {
                try
                {
                    //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                  List<SQLMainMenuPermissions> model =   db.MainMenuPermissions
                    .FromSqlInterpolated($"SELECT * FROM dbo.MainMenuPermissions WHERE UserName = {User.Identity.Name}")
                    .ToList();

                    //List<SQLMainMenuPermissions> model = db.MainMenuPermissions.FromSql("SELECT * FROM dbo.MainMenuPermissions ").Where(x => x.UserName == User.Identity.Name).ToList();
                    foreach(var menu in model)
                    {
                        menu.SubMenus = db.SubMenus.FromSqlInterpolated($"Select * from SubMenus Where MainMenuId = {menu.Id.ToString()}").ToList();
                    }
                    return View(model);
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
    }
}
