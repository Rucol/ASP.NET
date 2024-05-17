using ContosoJourney.Models;
using ContosoUniversity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ContosoJourney.Data
{
    
    public class JourneyContext : IdentityDbContext<IdentityUser>
    {
        public JourneyContext(DbContextOptions<JourneyContext> options) : base(options)
        {
        }

        public DbSet<Journey> Journeys { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Journey>().ToTable("Journey");
            modelBuilder.Entity<Enrollment>().ToTable("Enrollment");
            modelBuilder.Entity<Student>().ToTable("Student");
            
            
        }
    }
} 
