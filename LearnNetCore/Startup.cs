using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using LearnNetCore.Models;
using Microsoft.EntityFrameworkCore;

namespace LearnNetCore
{
    public class Startup
    {
        private IConfiguration _config;

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<AppDBContext>(options => options.UseSqlServer(_config.GetConnectionString("EmployeeDBConnectionString")));


           services.AddMvc().AddXmlSerializerFormatters();
            //services.AddSingleton<IEmployeeRepository, MockEmployeeRepository>();
           //services.AddScoped<IEmployeeRepository, MockEmployeeRepository>();
           services.AddScoped<IEmployeeRepository, SQLEmployeeRepository>();

        }
        public Startup(IConfiguration config)
        {
            _config = config;
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                 app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            //app.UseMvcWithDefaultRoute();
            app.UseMvc(Routes =>
            {
                Routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
