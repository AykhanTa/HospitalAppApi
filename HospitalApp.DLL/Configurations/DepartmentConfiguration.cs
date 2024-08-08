using HospitalApp.DLL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalApp.DLL.Configurations
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(d => d.Name).IsRequired().HasMaxLength(20);
            builder.Property(d => d.Limit).IsRequired();
            builder.Property(d=>d.CreateDate).IsRequired().HasDefaultValueSql("getdate()");
            builder.Property(d => d.Image).IsRequired();
        }
    }
}
