using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Presistance.Data.Configurations.Departments
{
    internal class DepartmentConfigurations : IEntityTypeConfiguration<Models.Departments.Department>
    {
        public void Configure(EntityTypeBuilder<Models.Departments.Department> builder)
        {
            builder.Property(x => x.Id).UseIdentityColumn(10, 10);
            builder.Property(x => x.Name).IsRequired().HasColumnType("varchar(50)");
            builder.Property(x => x.Code).IsRequired().HasColumnType("varchar(50)");
            builder.Property(x => x.CreatedOn).HasDefaultValueSql("GETDATE()");
            builder.Property(x => x.LastModifiedOn).HasComputedColumnSql("GETDATE()");
            builder.HasMany(d => d.Employees)
                   .WithOne(e => e.Department)
                   .HasForeignKey(e => e.DepartmentId)
                   .OnDelete(DeleteBehavior.SetNull);

        }
    }
}
