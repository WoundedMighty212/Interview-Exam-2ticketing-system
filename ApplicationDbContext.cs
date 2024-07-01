using Interview_Exam_2ticketing_system.Models;
// Data/ApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
namespace Interview_Exam_2ticketing_system
{
    

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Bug> Bugs { get; set; }
    }

}
