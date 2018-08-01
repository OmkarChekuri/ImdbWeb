using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Cfg;
using NHibernate;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using SimpleInjector.Integration.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using ImdbWeb.Infrastructure;

namespace ImdbWeb
{
    public class Startup
    {

        private Container container = new Container();

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                // No cookie options being customized at the moment!
                options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
            });
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
            services.AddSingleton<IControllerActivator>(new SimpleInjectorControllerActivator(container));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IViewComponentActivator>(new SimpleInjectorViewComponentActivator(container));
            services.UseSimpleInjectorAspNetRequestScoping(container);
            services.AddRouting();
            
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //pending
            container.RegisterInstance(typeof(ICurrentUserContext), new AspNetCurrentUserContext(GetHttpContextProvider(app)));

           // var configuration = new ConfigurationBuilder()
           //    .AddEnvironmentVariables()
           //    .AddJsonFile(env.ContentRootPath + "/config.json")
           //    .AddJsonFile(env.ContentRootPath + "/config.development.json",true)  //true to indicate this file is optional
           //    .Build();

            //if (configuration.GetValue<bool>("FeatureToggles:EnableDeveloperExceptions"))
            if(env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            var SessionFactory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012.ConnectionString(Configuration["Imdb"]))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Startup>()).BuildSessionFactory();

            container.RegisterInstance(typeof(ISessionFactory), SessionFactory);

            // Register how to create an ISession using an ISessionFactory.       
            container.Register(typeof(NHibernate.ISession), () => container.GetService<ISessionFactory>().OpenSession(), Lifestyle.Scoped);
                                       
            app.UseStaticFiles();

            app.UseAuthentication();
            
            app.UseMvc(routes =>
            {
                routes.MapRoute(

                    name: "default",
                    //template: "{controller=Movies}/{action=Index}/{id?}");
                    template: "{controller=Account}/{action=Login}/{id?}");

        });
        }
        
        // Delegate function passed to AspNetPrincipalContext class which it 
        // can use to retrieve the HttpContext at a later time.
        private Func<ClaimsPrincipal> GetHttpContextProvider(IApplicationBuilder app)
        {
            var accessor = app.ApplicationServices.GetRequiredService<IHttpContextAccessor>();
            return () =>
            {
                if (accessor.HttpContext == null)
                {
                    throw new InvalidOperationException("No HttpContext");
                }
                return accessor.HttpContext.User;
            };
        }
    }

   
}
