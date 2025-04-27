using HMS.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace HMS.Infrustructure.Data
{//IdentityDbContext<User, Role, int, IdentityUserClaim<int>, IdentityUserRole<int>, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    public class ApplicationDBContext : DbContext
    {
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Specialty> Specialties { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Medication> Medications { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            : base(options)
        { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bill>()
                .HasOne(b => b.Prescription)
                .WithMany(p => p.Bills)
                .HasForeignKey(b => b.PrescriptionID)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Bill>()
                .Property(b => b.Amount)
                .HasPrecision(18, 4); // adjust precision and scale as needed

            modelBuilder.Entity<Medication>()
                .Property(m => m.Price)
                .HasPrecision(18, 4); // adjust as appropriate

            base.OnModelCreating(modelBuilder);
        }
    }
}
