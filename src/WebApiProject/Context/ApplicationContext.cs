using ConsoleProject.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApiProject.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public DbSet<ApplicationProgram> Programs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationProgram>()
                .ToContainer("Programs");
        }
    }
}
