using HMS.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HMS.Infrustructure.Configurations
{
    public class PrescriptionConfigurations : IEntityTypeConfiguration<Prescription>
    {
        public void Configure(EntityTypeBuilder<Prescription> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Date)
                .IsRequired();

            builder.HasMany(p => p.Medications)
                .WithMany(m => m.Prescriptions);

            builder.HasMany(p => p.Bills)
                .WithOne(b => b.Prescription)
                .HasForeignKey(b => b.PrescriptionID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
