using ConsoleProject.Models;
using ConsoleProject.Models.Profile;
using ConsoleProject.Models.Questions;
using ConsoleProject.Models.Stages;
using Microsoft.EntityFrameworkCore;

namespace WebApiProject.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public DbSet<ApplicationProgram> Programs { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<Stage> Stages { get; set; }
        public DbSet<Question> Questions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ef configurations
            modelBuilder.Entity<ApplicationProgram>()
                .ToContainer("Programs");

            modelBuilder.Entity<Application>()
                .ToContainer("Applications");

            modelBuilder.Entity<Stage>()
                .ToContainer("Stages");

            modelBuilder.Entity<Question>()
                .ToContainer("Questions");
        }
    }
}
