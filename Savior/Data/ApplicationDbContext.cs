using Microsoft.EntityFrameworkCore;
using Savior.Models;
using System.Collections.Generic;

namespace Savior.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<Emergency> Emergencies { get; set; }
        public DbSet<MedicalTeam> MedicalTeams { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<WeeklySchedule> WeeklySchedules { get; set; }
        public DbSet<DailySchedule> DailySchedules { get; set; }
        public DbSet<Clinic> Clinics { get; set; }
        public DbSet<WorksAt> WorksAts { get; set; }
        public DbSet<FirstAid> FirstAids { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<WorksAt>()
                .HasKey(w => new { w.DoctorID, w.ClinicID });

            modelBuilder.Entity<WorksAt>()
                .HasOne(w => w.Doctor)
                .WithMany(d => d.WorksAts)
                .HasForeignKey(w => w.DoctorID);

            modelBuilder.Entity<WorksAt>()
                .HasOne(w => w.Clinic)
                .WithMany(c => c.WorksAts)
                .HasForeignKey(w => w.ClinicID);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.User)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.UserID);

            modelBuilder.Entity<MedicalTeam>()
               .HasOne(m => m.User)
               .WithMany(u => u.MedicalTeams)
               .HasForeignKey(m => m.UserID);


            modelBuilder.Entity<Emergency>()
                .HasOne(e => e.User)
                .WithMany(u => u.EmergencyRequests)
                .HasForeignKey(e => e.UserID);

            modelBuilder.Entity<FirstAid>()
                .HasOne(f => f.User)
                .WithMany(u => u.FirstAids)
                .HasForeignKey(f => f.UserID);
            
            modelBuilder.Entity<DailySchedule>()
                .HasOne(d => d.Clinic)
                .WithMany(c => c.DailySchedules)
                .HasForeignKey(d => d.ClinicID);

            modelBuilder.Entity<WeeklySchedule>()
                .HasOne(w => w.Clinic)
                .WithMany(c => c.WeeklySchedules)
                .HasForeignKey(w => w.ClinicID);
        


        }
    }
}