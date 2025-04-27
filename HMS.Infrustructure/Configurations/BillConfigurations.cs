using HMS.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HMS.Infrustructure.Configurations
{
    public class BillConfigurations : IEntityTypeConfiguration<Bill>
    {
        public void Configure(EntityTypeBuilder<Bill> builder)
        {
            builder.HasKey(b => b.BillID);

            builder.Property(b => b.Amount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(b => b.BillDate)
                .HasDefaultValueSql("GETDATE()")
                .IsRequired();
        }
    }

}
