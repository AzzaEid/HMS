using HMS.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HMS.Infrustructure.Configurations
{
    public class SpecialtyConfigurations : IEntityTypeConfiguration<Specialty>
    {
        public void Configure(EntityTypeBuilder<Specialty> builder)
        {
            builder.HasKey(s => s.SpecialtyId);

            builder.Property(s => s.SpecialtyName)
                .IsRequired()
                .HasMaxLength(100);
        }
    }

}
