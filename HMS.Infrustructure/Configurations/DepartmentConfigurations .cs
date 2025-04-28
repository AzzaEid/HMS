using HMS.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HMS.Infrustructure.Configurations
{
    public class DepartmentConfigurations : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasKey(d => d.DepartmentId);

            builder.Property(d => d.DepartmentName)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasMany(d => d.Doctors)
                .WithOne(doc => doc.Department)
                .HasForeignKey(doc => doc.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            //builder.HasOne(d => d.ManagerDoctor)
            //    .WithOne(doc => doc.ManagedDepartment)
            //    .HasForeignKey<Department>(d => d.ManagerDoctorId)
            //    .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.ManagerDoctor)
                .WithOne(doc => doc.ManagedDepartment)
                .HasForeignKey<Department>(d => d.ManagerDoctorId)
                   .HasPrincipalKey<Doctor>(d => d.Id)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
