using MimeKit;
using MusicManagementsMinimalAPI.Common.Options;
using MusicManagementsMinimalAPI.Models.DTO;
using System.Text;
using Yitter.IdGenerator;

namespace MusicManagementsMinimalAPI.Common
{
    public class SendEmailCode
    {
        
        public static async Task<EmailKeyValueDTO> SendEmailCodeAsync(EmailOptions emailOptions)
        {
            

            string title = "邮箱验证码";
            var code = GenerateRandomCode();
            var randomId = YitIdHelper.NextId();
            string content = "";
           
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(emailOptions.SendEmail, emailOptions.SendEmail));
            message.To.Add(new MailboxAddress(emailOptions.ReceiveEmail, emailOptions.ReceiveEmail));
            message.Subject = title;
            content = "您的邮箱验证码是：" + code;
            message.Body = new TextPart("html")
            {
                Text = content
            };

            using var client = new MailKit.Net.Smtp.SmtpClient();
            client.Connect(emailOptions.Host, emailOptions.Port, emailOptions.EnableSsl);
            client.Authenticate(emailOptions.UserName, emailOptions.Password);
            client.Send(message);
            client.Disconnect(true);
            var emailKeyValueDTO = new EmailKeyValueDTO
            {
                Key = $"CodeKey:{"xxx@qq.com"}:{randomId.ToString()}",
                Value = code,
                RandomId=randomId.ToString()
            };
            return await Task.FromResult(emailKeyValueDTO);


        }


        public static string GenerateRandomCode()
        {
            const string validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var result = new StringBuilder();
            for (var i = 0; i < 6; i++)
            {
                result.Append(validChars[random.Next(validChars.Length)]);
            }
            return result.ToString();
        }
    }
}
