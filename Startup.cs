﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace MyCourse
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //qui inserisco i middleware
            //help a video (mostra gli errori se sono in development)
            //if (env.IsDevelopment())
            if (env.IsEnvironment("Development"))  //definito in launchsettings.json
            {
                app.UseDeveloperExceptionPage();        
            }
            //permette utilizzo file statici nella cartella wwwroot
            app.UseStaticFiles();

            app.Run(async (context) =>
            {
                string nome = context.Request.Query["nome"];
                if (nome==null)
                {
                    nome="NESSUNO";
                }
                //await context.Response.WriteAsync("Ciao Mondo Cane!!");
                await context.Response.WriteAsync($"Ciao Mondo {nome.ToUpper()}!");
                //context.Request.Path;
                //context.Request.Host;
                //context.Request.Query["id"];                                    
            });
        }
    }
}
