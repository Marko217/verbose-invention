using AgreementManagement.Data.Entities;
using AgreementManagement.Repo.Repo;
using AgreementManagement.Repo.Repo.Interfaces;
using AgreementManagement.Service.Services;
using AgreementManagement.Service.Services.Interfaces;
using AgreementManagement.Web.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serilog;

namespace AgreementManagement.Web
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
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddMvc();
            services.AddSession();
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<AuhtActionFilterAttribute>();

            services.AddDbContext<AgreementManagementDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("AgreementManagementConnection")).EnableSensitiveDataLogging());

            services.AddScoped(typeof(IAgreementRepository), typeof(AgreementRepository));
            services.AddTransient<IAgreementService, AgreementService>();

            services.AddScoped(typeof(IProductRepository), typeof(ProductRepository));
            services.AddTransient<IProductService, ProductService>();

            services.AddScoped(typeof(IProductGroupRepository), typeof(ProductGroupRepository));
            services.AddTransient<IProductGroupService, ProductGroupService>();
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSerilogRequestLogging();
            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
