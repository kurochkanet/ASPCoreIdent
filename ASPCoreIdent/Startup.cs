using ASPCoreIdent.Models;
using BLL;
using BLL.Utils;
using Contracts.Business;
using Contracts.Repositories;

using DAL.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

using AutoMapper;

using Microsoft.AspNetCore.HttpsPolicy;
using DAL;

namespace ASPCoreIdent
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
            //ApplicationContext
            services.AddDbContext<ApplicationContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, IdentityRole>(opts => {
                opts.User.RequireUniqueEmail = true;    // ?????????? email
            })
            .AddEntityFrameworkStores<ApplicationContext>();

            services.AddControllersWithViews();


            ////DBContext
            services.AddDbContext<DataContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
             );
            services.AddScoped<DBInitalizer, DBInitalizer>();
            //models mapping
            services.AddAutoMapper(new Type[] { typeof(DalMapper), typeof(ViewMapper) });

            //data layers
            services.AddScoped<IGoodsRepository, GoodsRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            

            //services
            services.AddScoped<IGoodsService, GoodService>();
            services.AddScoped<ICategoryService, CategoryService>();
            

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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
        private void InitFileEnviroments(IWebHostEnvironment env)
        {
            ContentPathBuilder.ContentRootPath = env.ContentRootPath;
            ContentPathBuilder.ContentGoodsFolder = Configuration.GetValue<string>("Content:GoodsFolder");
            ContentPathBuilder.ContentThumbnailsFolder = Configuration.GetValue<string>("Content:ThumbnailsFolder");
            ContentPathBuilder.ContentWebFolder = Configuration.GetValue<string>("Content:WebRootFolder");
        }
    }
}
