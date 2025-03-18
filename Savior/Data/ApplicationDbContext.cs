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

    }
}
