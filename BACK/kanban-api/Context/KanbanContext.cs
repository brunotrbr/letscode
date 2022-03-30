using kanban_api.Models;
using Microsoft.EntityFrameworkCore;

namespace kanban_api.Context
{
    public partial class KanbanContext : DbContext
    {
        public KanbanContext(DbContextOptions<KanbanContext> options) : base(options)
        {
        }

        public DbSet<Cards> Cards { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }
    }
}
