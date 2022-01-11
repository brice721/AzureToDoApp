using Domain;
using Infrastructure;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class ToDoDbContext : DbContext
    {
        public ToDoDbContext() { }

        public ToDoDbContext(DbContextOptions<ToDoDbContext> option) : base(option) { }

        public AppSettings Settings { get; set; } = new AppSettings();

        public virtual DbSet<ToDo> ToDos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultContainer(Settings.Container);
            builder.Entity<ToDo>(todo =>
            {
                todo.HasPartitionKey(key => key.Id);
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseCosmos(
                Settings.Url.ToString(),
                Settings.PrimaryKey,
                Settings.Database, options =>
                {
                    options.ConnectionMode(ConnectionMode.Gateway);
                });
        }
    }
}