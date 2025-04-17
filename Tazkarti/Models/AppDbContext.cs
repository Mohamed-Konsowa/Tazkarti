using Microsoft.EntityFrameworkCore;

namespace Tazkarti.Models
{
    public class AppDbContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<User_Ticket> User_Tickets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server = DESKTOP-AG9S4RI; Database = Tazkarti; Integrated Security = SSPI; TrustServerCertificate = True");
        }
    }
}
