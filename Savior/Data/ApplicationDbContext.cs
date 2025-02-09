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

    }
}
