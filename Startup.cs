using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using MyCourse.Models.Services.Application;

namespace MyCourse
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddMvc(); //per aggiungere servizio route  
            services.AddMvc();  //.SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            //services.AddTransient<CourseService>();
            services.AddTransient<ICourseService,CourseService>(); //non posso implementare solo l'interfaccia
            //AddTransient Crea un componente ogni volta che ne a bisogno e poi lo distrugge (usare quando la classe e relativamente semplice)
            //AddScoped la crea e l'utilizza finchè siamo nella stessa richiesta http e poi la distrugge (usare qunado il servizio è pesante esempio entityframework o quando devo passare info da middleware a controller)
            //AddSingleton crea un'istanza e la inietta in tutti i componenti anche in richieste http differenti (una in tutta l'applicazione (quando il servizio deve spedire email)
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifetime)
        {
            //qui inserisco i middleware
            //help a video (mostra gli errori se sono in development)
            //if (env.IsDevelopment())
            if (env.IsEnvironment("Development"))  //definito in launchsettings.json
            {
                app.UseDeveloperExceptionPage();        

                //Aggiorniamo un file per notificare al BrowserSync che deve aggiornare la pagina
                lifetime.ApplicationStarted.Register(()=>                            
                {
                    string filePath= Path.Combine(env.ContentRootPath,"bin/reload.txt");
                    File.WriteAllText(filePath,DateTime.Now.ToString());
                });  
            }
            //permette utilizzo file statici nella cartella wwwroot
            app.UseStaticFiles();

            // app.Run(async (context) =>
            // {
            //     string nome = context.Request.Query["nome"];
            //     if (nome==null)
            //     {
            //         nome="NESSUNO";
            //     }
            //     //await context.Response.WriteAsync("Ciao Mondo Cane!!");
            //     await context.Response.WriteAsync($"Ciao Mondo {nome.ToUpper()}!");
            //     //context.Request.Path;
            //     //context.Request.Host;
            //     //context.Request.Query["id"];                                    
            // });

            //app.UseMvcWithDefaultRoute();

            app.UseMvc(routeBuilder =>{
                //routeBuilder.MapRoute("default","{controller}/{action}/{id}");
                routeBuilder.MapRoute("default","{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
