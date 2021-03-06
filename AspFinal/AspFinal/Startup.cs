using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Allaia.Libs;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Repository.Data;
using Repository.Repositories.AdminRepository;
using Repository.Repository.AboutRepository;
using Repository.Repository.BlogRepository;
using Repository.Repository.CaseRepository;
using Repository.Repository.CategoryRepository;
using Repository.Repository.HomeRepository;

namespace AspFinal
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
            services.AddAutoMapper(typeof(Startup));
            services.AddDbContext<JotexDbContext>(options => options.
                                                            UseSqlServer(Configuration.
                                                            GetConnectionString("Default"),
                                                            x => x.MigrationsAssembly("Repository")));

            services.AddTransient<IHomeRepository, HomeRepository>();
            services.AddTransient<ICategoryyRepository, CategoryyRepository>();
            services.AddTransient<IAdminRepository, AdminRepository>();
            services.AddTransient<IFileManager, FileManager>();
            services.AddTransient<IBlogRepository, BlogRepository>();
            services.AddTransient<ICaseRepository, CaseRepository>();
            services.AddTransient<IAboutRepository, AboutRepository>();



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

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute(
                  name: "MyArea",
                  pattern: "{area:exists}/{controller=Account}/{action=Login}/{id?}");


                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
