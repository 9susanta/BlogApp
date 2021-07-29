using BlogApp.Common;
using BlogApp.Concrete;
using BlogApp.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<blogdbContext>(o => o.UseMySql(Configuration.GetConnectionString("AppConnection"), ServerVersion.AutoDetect(Configuration.GetConnectionString("AppConnection"))));
            services.AddScoped<INewsOprations, NewsOprations>();
            services.AddScoped(typeof(IOperation<>),typeof(OpertionClass<>));
            services.AddScoped<IClsRole, ClsRole>();
            services.AddScoped<IUsermanament, Usermanament>();
            services.AddScoped<IclsNewsType, clsNewsType>();
            services.AddScoped<IapiPlugin, apiPlugin>();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.  
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            //services.AddWebOptimizer(pipeline =>
            //{
            //    pipeline.MinifyJsFiles("js/a.js", "js/b.js", "js/c.js");
            //    pipeline.MinifyCssFiles("css/**/*.css");
            //    pipeline.AddCssBundle("/css/bundle.css", "css/a.css", "css/b.css");
            //    pipeline.AddJavaScriptBundle("/js/scripts.js", "a.js", "b.js");
            //});
            services.AddControllersWithViews();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
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
                app.UseExceptionHandler("/Home/Error");
            }
            //app.UseWebOptimizer();

            app.UseStaticFiles();
            app.UseCookiePolicy();
            
            app.UseRouting();
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
