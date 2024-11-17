using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using sh_rt.Areas.Admin.Interfaces;
using sh_rt.Areas.Admin.Services;
using sh_rt.Context;
using sh_rt.Interfaces;
using sh_rt.Mappings;
using sh_rt.Models;
using sh_rt.Repositories;
using sh_rt.Services;
using sh_rt.Services.Interfaces;

namespace sh_rt
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


            services.AddDbContext<AppDbContext>(opts =>
            opts.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
            );

            services.Configure<ImagesConfiguration>(Configuration.GetSection("ImagesFolderConfiguration"));

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped(s => Cart.GetCart(s));

            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IShirtRepository, ShirtRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();
            services.AddScoped<ISalesReportService, SalesReportService>();
            services.AddScoped<ISalesChartService, SalesChartService>();

            services.AddAuthorization(opts =>
            {
                opts.AddPolicy("Admin", policy =>
                {
                    policy.RequireRole("Admin");
                });
            });

            services.AddAutoMapper(typeof(Startup));

            services.AddControllersWithViews();

            services.AddPaging(opts =>
            {
                opts.ViewName = "Bootstrap4";
                opts.PageParameterName = "pageindex";
            });

            services.AddMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ISeedUserRoleInitial seedUserRoleInitial)
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
            app.UseSession();

            app.UseRouting();

            seedUserRoleInitial.SeedRoles();
            seedUserRoleInitial.SeedUsers();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "filter",
                    pattern: "shirt/{action}/{category?}",
                    defaults: new { Controller = "Shirt", action = "GetShirts" }
                    );

                endpoints.MapControllerRoute(
                  name: "areas",
                  pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}"
                );


                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
