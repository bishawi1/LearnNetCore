using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using Microsoft.Extensions.Logging;
using TMS.Models;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;


namespace TMS
{
    public class Startup
    {
        private IConfiguration _config;

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<AppDBContext>(options => options.UseSqlServer(_config.GetConnectionString("EmployeeDBConnectionString")));
            //services.AddDbContextPool<AppDBContext>(options => options.UseMySQL(_config.GetConnectionString("TMSDBConnectionString")));
            
            services.AddControllersWithViews(); // or AddRazorPages(), depending on your app

            services.AddIdentity<ApplicationUser, IdentityRole>(options => {
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
            }).AddEntityFrameworkStores<AppDBContext>()
            .AddDefaultTokenProviders();
            //services.Configure<IdentityOptions>(options => {
            //    options.Password.RequiredLength = 10;
            //    options.Password.RequiredUniqueChars = 3;
            //    options.Password.RequireNonAlphanumeric = false;
            //});
            services.Configure<DataProtectionTokenProviderOptions>(o => o.TokenLifespan = TimeSpan.FromHours(5));
            //services.AddMvc(options=> {
            //    var policy = new AuthorizationPolicyBuilder()
            //                   .RequireAuthenticatedUser()
            //                   .Build();
            //    options.Filters.Add(new AuthorizeFilter(policy));

            //}).AddXmlSerializerFormatters()
            //.AddJsonOptions(Options =>
            // {
            //     var resolver = Options.SerializerSettings.ContractResolver;
            //     if (resolver != null)
            //     {
            //         (resolver as DefaultContractResolver).NamingStrategy = null;
            //     }
            // });
            services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            })
            .AddXmlSerializerFormatters()
            .AddNewtonsoftJson(options =>
            {
                if (options.SerializerSettings.ContractResolver is DefaultContractResolver resolver)
                {
                    resolver.NamingStrategy = null; // Preserves property name casing
                }
            });
            services.AddSession(options=> {
                options.IdleTimeout = TimeSpan.FromSeconds(3600);
            });
            //services.AddSingleton<IEmployeeRepository, MockEmployeeRepository>();
            //services.AddScoped<IEmployeeRepository, MockEmployeeRepository>();
            services.AddScoped<IEmployeeRepository, SQLEmployeeRepository>();
            services.AddScoped<SQLBranchRepository>();
            services.AddScoped<SQLProjectRepository>();
            services.AddScoped<SQLCustomerRepository>();
            services.AddScoped<SQLTasksRepository>();
            services.AddScoped<SQLSupplierRepository>();
            services.AddScoped<SQLPurchaseOrderRepository>();
            services.AddScoped<SQLSettingsRepository>();
            services.AddScoped<SQLOffersRepository>();

        }
        public Startup(IConfiguration config)
        {
            _config = config;
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                 app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
            }
            app.UseStaticFiles();
            //app.UseStaticFiles(new StaticFileOptions()
            //{
            //    FileProvider = new PhysicalFileProvider(
            //                    Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images")),
            //                    RequestPath = new PathString("/images")
            //});
            //app.UseDirectoryBrowser(new DirectoryBrowserOptions()
            //{
            //    FileProvider = new PhysicalFileProvider(
            //        Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images")),
            //    RequestPath = new PathString("/images")
            //});
            app.UseAuthentication();
            app.UseCookiePolicy();
            app.UseSession();

            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });







            //app.UseMvc(Routes =>
            //{
            //    Routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            //});
            ////app.UseMvcWithDefaultRoute();
        }
    }
}
