using AutoMapper;
using IKEA.BLL.Common.Services.AttachmentService;
using IKEA.BLL.Services.Department;
using IKEA.BLL.Services.Employees;
using IKEA.DAL.Presistance.Data;
using IKEA.DAL.Presistance.Repositories.Department;
using IKEA.DAL.Presistance.Repositories.Employees;
using IKEA.DAL.Presistance.UnitOfWork;
using IKEA.PL.Mapping_Profile;
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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
