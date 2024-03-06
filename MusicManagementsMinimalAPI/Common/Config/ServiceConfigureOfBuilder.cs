using Microsoft.EntityFrameworkCore;
using MusicManagementsMinimalAPI.Data;
using MusicManagementsMinimalAPI.Models;

namespace MusicManagementsMinimalAPI.Common.Config
{
    public static class ServiceConfigureOfBuilder
    {
        public static void AddServicesConfig(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<MusicLikeContext>(opt =>
                opt.UseSqlServer(builder.Configuration.GetConnectionString("dbContext") ??
                throw new InvalidOperationException("Connection string 'dbContext' not found.")))
                .AddDbContext<MusicContext>(opt =>
                opt.UseSqlServer(builder.Configuration.GetConnectionString("dbContext") ??
                throw new InvalidOperationException("Connection string 'dbContext' not found.")))
                .AddDbContext<UserContext>(opt =>
                opt.UseSqlServer(builder.Configuration.GetConnectionString("dbContext") ??
                throw new InvalidOperationException("Connection string 'dbContext' not found.")));
            

        }
    }
}
