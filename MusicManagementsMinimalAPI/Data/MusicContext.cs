using Microsoft.EntityFrameworkCore;
using MusicManagementsMinimalAPI.Models;
namespace MusicManagementsMinimalAPI.Data
{
    public class MusicContext:DbContext
    {
        public MusicContext(DbContextOptions<MusicContext>options):base(options)
        { }
        public DbSet<Music> Music { get; set; } = default!;
        public DbSet<UserMusicRelate> UserMusicRelate { get; set;} = default!;
        public DbSet<TalkRelate> Talk { get; set; } = default!;
        public DbSet<DownloadRelate> DownloadDB { get; set; } = default!;
        public DbSet<AgreedRelate> AgreedDB { get; set; } = default!;
       

        public DbSet<UserProfile> UserProfile { get; set; } = default!;
    }
}
