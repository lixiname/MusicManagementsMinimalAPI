using Microsoft.EntityFrameworkCore;
using MusicManagementsMinimalAPI.Models;

namespace MusicManagementsMinimalAPI.Data
{
    public class UserContext : DbContext
    {

        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {

        }
        public DbSet<UserProfile> User { get; set; } = default!;
    }
}
