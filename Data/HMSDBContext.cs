using Hospital_Management_System.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Management_System.Data
{
    public class HMSDBContext: DbContext
    {
        public DbSet<Doctor> Doctors {  get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Specialty> Specialties { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Medication> Medications { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public HMSDBContext(DbContextOptions<HMSDBContext> options) 
            : base(options)
        { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bill>()
                .HasOne(b => b.Prescription)
                .WithMany(p => p.Bills)
                .HasForeignKey(b => b.PrescriptionID)
                .OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(modelBuilder);
        }
    }
}
