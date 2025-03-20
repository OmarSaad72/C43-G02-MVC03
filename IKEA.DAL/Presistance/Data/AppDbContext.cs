using IKEA.DAL.Models.Departments;
using IKEA.DAL.Models.Employees;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Presistance.Data
{
    public class AppDbContext: IdentityDbContext 
    {
        public AppDbContext(DbContextOptions<AppDbContext> options ):base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=.;DataBase=IKEA;Trusted_Connection=True;TrustServerCertificate=True;");
        //}
        public DbSet<Department> Department { get; set; }
        public DbSet<Employee> Employees { get; set; }

        //public DbSet<IdentityUser> Users { get; set; }
        //public DbSet<IdentityRole> Roles { get; set; }
    }
}
