using Microsoft.EntityFrameworkCore;
using MusicManagementsMinimalAPI.Data;
using MusicManagementsMinimalAPI.Models;
using Yitter.IdGenerator;

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
            builder.Services.AddCaptcha(builder.Configuration);

        }

        public static void AddYitIdHelperConfig(this WebApplicationBuilder builder)
        {


            var idGeneratorOptions = new IdGeneratorOptions(1) { WorkerIdBitLength = 6 };
            // 保存参数（务必调用，否则参数设置不生效）：
            YitIdHelper.SetIdGenerator(idGeneratorOptions);



        }
    }
}
