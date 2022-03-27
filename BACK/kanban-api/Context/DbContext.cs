using kanban_api.Models;
using Microsoft.EntityFrameworkCore;

namespace kanban_api.Context
{
    public class KanbanApiContext : DbContext
    {
        public KanbanApiContext(DbContextOptions<KanbanApiContext> options)
            : base(options)
        { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseInMemoryDatabase(databaseName: "kanban");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        public DbSet<Cards> Cards { get; set; }
    }
}
