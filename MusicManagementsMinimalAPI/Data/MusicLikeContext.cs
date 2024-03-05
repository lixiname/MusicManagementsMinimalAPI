using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MusicManagementsMinimalAPI.Models;

namespace MusicManagementsMinimalAPI.Data
{
    public class MusicLikeContext: DbContext
    {
        public MusicLikeContext(DbContextOptions<MusicLikeContext>options) 
            : base(options)
        {

        }
        public DbSet<UserMusicRelate> MusicLike { get; set; } = default!;
    }
}
