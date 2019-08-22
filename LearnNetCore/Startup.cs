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

namespace LearnNetCore
{
    public class Startup
    {
        private IConfiguration _config;

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }
        public Startup(IConfiguration config)
        {
            _config = config;
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
                              ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Use(async (context,NextDel) =>
            {
                logger.LogInformation("MW1: Incoming Request");
                //await context.Response.WriteAsync("Hello From 1st MiddleWare! ");
                await NextDel();
                logger.LogInformation("MW1: Outgoing Response");
            });

            app.Use(async (context, NextDel) =>
            {
                logger.LogInformation("MW2: Incoming Request");
                //await context.Response.WriteAsync("Hello From 1st MiddleWare! ");
                await NextDel();
                logger.LogInformation("MW2: Outgoing Response");
            });

            app.Run(async (context) =>
            {
                //string strMessage = System.Diagnostics.Process.GetCurrentProcess().ProcessName;

                //if (env.IsDevelopment())
                //{
                //    strMessage = _config["MyKey"];
                //}
                //else if (env.IsStaging()) 
                //{
                //    strMessage = "Staging";
                //}
                //await context.Response.WriteAsync("Hello World! " + System.Environment.NewLine + strMessage);
                await context.Response.WriteAsync("MW3: Request handled and Response produced");
                logger.LogInformation("MW3: Request handled and Response produced");
            });
        }
    }
}
