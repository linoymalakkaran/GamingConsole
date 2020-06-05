using System.Data.Entity;

namespace GamingConsole.Models
{
    public class GamingConsoleDbContext : DbContext
    {
        public GamingConsoleDbContext() : base("name=GamingConsoleDbContext")
        {
            Database.SetInitializer<GamingConsoleDbContext>(new DropCreateDatabaseIfModelChanges<GamingConsoleDbContext>());
        }

        public DbSet<BasketBallBoard> MainBoards { get; set; }
    }
}