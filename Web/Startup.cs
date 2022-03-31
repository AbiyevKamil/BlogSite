using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Constants;
using Core.Entities;
using Core.Helpers;
using DataAccess.Persistance.Contexts;
using DataAccess.Persistance.Seeding;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Abstract.Base;
using DataAccess.Repositories.Implementation;
using DataAccess.Repositories.Implementation.Base;
using DataAccess.UnitOfWork.Abstract;
using DataAccess.UnitOfWork.Implementation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Service.Business.Abstract;
using Service.Business.Implementation;
using Web.Helpers;

namespace Web
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
            var conString = Configuration.GetConnectionString("ConStringProduction");
            services.AddDbContext<BlogContext>(options => { options.UseSqlServer(conString); });
            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = "/home/index";
                options.LoginPath = "/auth/login";
                options.LogoutPath = "/auth/logout";
                options.ExpireTimeSpan = TimeSpan.FromHours(72);
            });
            services.AddIdentity<User, Role>(
                    options =>
                    {
                        options.Password.RequireDigit = false;
                        options.Password.RequiredLength = 6;
                        options.Password.RequireLowercase = false;
                        options.Password.RequireUppercase = false;
                        options.Password.RequireNonAlphanumeric = false;

                        options.SignIn.RequireConfirmedEmail = true;
                    }
                ).AddEntityFrameworkStores<BlogContext>()
                .AddDefaultTokenProviders();
            services.AddControllersWithViews();
            services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

            // DI
            services.AddSingleton<EmailService>();
            services.AddSingleton<FileService>();

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IBlogRepository, BlogRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IBlogService, BlogService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICommentService, CommentService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<User> userManager,
            RoleManager<Role> roleManager, BlogContext context)
        {
            if (env.IsDevelopment())
            {
                SeedData.Seed(context, userManager, roleManager).Wait();
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
                    name: "Blogs",
                    pattern: "blog/{uniqueUrl}",
                    new {controller = "Blogs", action = "Details"}
                );
                endpoints.MapControllerRoute(
                    name: "Categories",
                    pattern: "category/{categoryName}",
                    new {controller = "Categories", action = "Index"}
                );
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}