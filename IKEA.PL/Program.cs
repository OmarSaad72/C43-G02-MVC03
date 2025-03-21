using AutoMapper;
using IKEA.BLL.Common.Services.AttachmentService;
using IKEA.BLL.Services.Department;
using IKEA.BLL.Services.Employees;
using IKEA.DAL.Models.Identity;
using IKEA.DAL.Presistance.Data;
using IKEA.DAL.Presistance.Repositories.Department;
using IKEA.DAL.Presistance.Repositories.Employees;
using IKEA.DAL.Presistance.UnitOfWork;
using IKEA.PL.Mapping_Profile;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IKEA.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Configure
            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<AppDbContext>((OptionsBuilder) =>
            {
                //OptionsBuilder.UseSqlServer("Server=.;DataBase=IKEA;Trusted_Connection=True;TrustServerCertificate=True;");
                OptionsBuilder.UseLazyLoadingProxies()
                .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            //builder.Services.AddScoped<IDepartmentRepo, DepartmentRepo>();  //allow dependancy injection 
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();  //allow dependancy injection 
            //builder.Services.AddScoped<IEmployeesRepo, EmployeesRepo>();  //allow dependancy injection 
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();  //allow dependancy injection 
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();  //allow dependancy injection 
            builder.Services.AddAutoMapper(m => m.AddProfile(new MappingProfiles()));  // LifeTime: Transient
            builder.Services.AddTransient<IAttachmentService, AttachmentService>();
            //builder.Services.AddScoped<UserManager<AppUser>>();
            //builder.Services.AddScoped<RoleManager<IdentityRole>>();
            //builder.Services.AddScoped<SignInManager<AppUser>>();
            builder.Services.AddIdentity<AppUser, IdentityRole>((options) =>
            {
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 5;
            })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();  //PasswordSignInAsync depend on ==> AddDefaultTokenProviders
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie
                (
                options =>
                {
                    options.LoginPath = "/Account/LogIn";
                    options.AccessDeniedPath = "/Home/Error"; // Errors
                    options.LogoutPath = "/Account/LogIn";
                }
                );
            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
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

            app.MapControllerRoute(
                name: "default",
                //pattern: "{controller=Account}/{action=Register}/{id?}");
                pattern: "{controller=Account}/{action=LogIn}/{id?}");

            app.Run();
        }
    }
}
