
using MusicManagementsMinimalAPI.Common.Options;
using StackExchange.Redis;

namespace MusicManagementsMinimalAPI.Common.Config
{
    public static class RedisConfigOfBuilder
    {
        public static void AddRedisConfig(this WebApplicationBuilder builder)
        {
            var redisConfig = new RedisOptions();
            builder.Configuration.GetSection("Redis").Bind(redisConfig);
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(redisConfig.Connection??
            throw new InvalidOperationException("Connection redisSettings not found."));
            
            var database = redis.GetDatabase();
            builder.Services.AddSingleton(database);
        }
    }
}
