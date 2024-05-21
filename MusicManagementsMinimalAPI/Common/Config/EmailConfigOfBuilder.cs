using MusicManagementsMinimalAPI.Common.Options;

namespace MusicManagementsMinimalAPI.Common.Config
{
    public static class EmailConfigOfBuilder
    {
        public static void AddEmailConfig(this WebApplicationBuilder builder)
        {
            var emailConfig = new EmailOptions();
            builder.Configuration.GetSection("Email").Bind(emailConfig);
            builder.Services.AddSingleton(emailConfig);
        }
    }
}
