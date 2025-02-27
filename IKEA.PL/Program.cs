using IKEA.BLL.Services.Department;
using IKEA.DAL.Presistance.Data;
using IKEA.DAL.Presistance.Repositories.Department;
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
            builder.Services.AddDbContext<AppDbContext>((OptionsBuilder =>
            {
                //OptionsBuilder.UseSqlServer("Server=.;DataBase=IKEA;Trusted_Connection=True;TrustServerCertificate=True;");
                OptionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            }));

            builder.Services.AddScoped<IDepartmentRepo, DepartmentRepo>();  //allow dependancy injection 
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();  //allow dependancy injection 
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
