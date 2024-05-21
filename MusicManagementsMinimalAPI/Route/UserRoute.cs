using Lazy.Captcha.Core;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicManagementsMinimalAPI.Common;
using MusicManagementsMinimalAPI.Common.Options;
using MusicManagementsMinimalAPI.Data;
using MusicManagementsMinimalAPI.Models;
using MusicManagementsMinimalAPI.Models.DTO;
using StackExchange.Redis;
using Yitter.IdGenerator;

namespace MusicManagementsMinimalAPI.Route
{
    public static class UserRoute
    {
        public static void AddLoginTransactionEndPoint(this WebApplication app)
        {

            app.MapGet("Captcha", ( ICaptcha captcha) =>
            {
                var codeId = YitIdHelper.NextId().ToString();
                var captchaInfo=captcha.Generate(codeId);
                var stream = new MemoryStream(captchaInfo.Bytes);
                
                return new { Id = captchaInfo.Id, Img = captchaInfo.Bytes };
                


            })
                .WithName("Captcha")
                .WithOpenApi(option => new(option)
                {

                    Description = "Captcha",
                    Summary = "Captcha"
                })
                .WithTags("Captcha");



            app.MapPost("UserLogin", Results<Ok<UserProfile>,NotFound<string>> ([FromBody] UserLoginInfoDTO userProfile, [FromServices] UserContext userContext, ICaptcha captcha) =>
            {
                if (!captcha.Validate(userProfile.CaptchaId.ToString(), userProfile.CaptchaCode))
                    return TypedResults.NotFound("CaptchaCode error");

                var Pwd=DataEncryption.ToMD5(userProfile.Password);
                userProfile.Password=Pwd;
                var user = userContext.User.FirstOrDefault(e => e.UserId == userProfile.UserId) ?? null;
                if (user != null)
                {
                    return TypedResults.Ok(user);
                }
                else
                {
                    return TypedResults.NotFound("not exist this user");
                }

            })
                .WithName("UserLogin")
                .WithOpenApi(option => new(option)
                {

                    Description = "UserLogin",
                    Summary = "UserLogin"
                })
                .WithTags("UserInfo");



            app.MapGet("sendEmailCode", async ([FromQuery(Name = "email")] string email,[FromServices] EmailOptions emailOptions, [FromServices] IDatabase redis) =>
            {
                emailOptions.ReceiveEmail = email;
                var emailKeyValueDTO=await SendEmailCode.SendEmailCodeAsync(emailOptions);
                redis.StringSet(emailKeyValueDTO.Key,emailKeyValueDTO.Value, TimeSpan.FromMinutes(3));
                return emailKeyValueDTO.RandomId;
            }).WithName("sendEmailCode")
                .WithOpenApi(option => new(option)
                {

                    Description = "findUserPassword",
                    Summary = "sendEmailCode to findUserPassword"
                })
                .WithTags("sendEmailCode");

            app.MapPost("findPwd", Results<Ok<UserProfile>, NotFound<string>> (
                [FromBody] UserResetPwdDTO userRestPwdDTO, [FromServices] IDatabase redis, [FromServices] UserContext userContext) =>
            {

                var user = userContext.User.FirstOrDefault(e => e.UserId == userRestPwdDTO.UserId) ?? null;
                if (user != null)
                {
                    
                    var cacheEmailCode = redis.StringGet($"CodeKey:{"2729801553@qq.com"}:{userRestPwdDTO.EmailRandomId}");
                    if (userRestPwdDTO.EmailCode.Equals(cacheEmailCode, StringComparison.OrdinalIgnoreCase))
                    {
                        var Pwd = DataEncryption.ToMD5(userRestPwdDTO.ChangePassword);
                        user.Password = Pwd;
                        userContext.User.Update(user);
                        userContext.SaveChanges();
                        return TypedResults.Ok(user);
                    }
                    else
                    {
                        return TypedResults.NotFound("code error");
                    }
                }
                else
                {
                    return TypedResults.NotFound("not find this user");
                }
                
                
            }).WithName("findUserPassword")
                .WithOpenApi(option => new(option)
                {

                    Description = "findUserPassword",
                    Summary = "findUserPassword"
                })
                .WithTags("findPwd");


            app.MapPost("ManagmentUserLogin", Results<Ok<ManagementUserProfile>, NotFound<string>> ([FromBody] ManagementUserProfile userProfile, [FromServices] UserContext userContext) =>
            {
                var user = userContext.ManagementUser.FirstOrDefault(e=>e.UserId==userProfile.UserId) ?? null;
                
                if (user != null)
                {
                    return TypedResults.Ok(user);
                }
                else
                {
                    return TypedResults.NotFound("not exist this management user");
                }

            })
                .WithName("ManagementUserLogin")
                .WithOpenApi(option => new(option)
                {

                    Description = "ManagementUserLogin",
                    Summary = "ManagementUserLogin"
                })
                .WithTags("ManagementUserLogin");

            app.MapPost("UserRegister", Results<Ok<UserProfile>, NotFound<string>> ([FromBody] UserProfile userProfile, [FromServices] UserContext userContext) =>
            {
                var Pwd = DataEncryption.ToMD5(userProfile.Password);
                userProfile.Password = Pwd;
                var user = userContext.User.Where(e => e.UserId == userProfile.UserId) ?? null;
                if (user.Any())
                {
                    return TypedResults.NotFound("exist this user");

                }
                else
                {
                    userContext.User.Add(userProfile);
                    userContext.SaveChanges();
                    return TypedResults.Ok(userProfile);
                }

            })
                .WithName("UserRegister")
                .WithOpenApi(option => new(option)
                {

                    Description = "UserRegister",
                    Summary = "UserRegister"
                })
                .WithTags("UserInfo");


            app.MapPost("UserInfoUpdate", Results<Ok<UserProfile>, NotFound<string>> ([FromBody] UserProfileUpdateDTO userProfile, [FromServices] UserContext userContext) =>
            {
                var user = userContext.User.FirstOrDefault(e => e.UserId == userProfile.UserId) ?? null;
                if (user != null)
                {
                    user.Name = userProfile.Name;
                    user.Phone = userProfile.Phone;
                    user.Email = userProfile.Email;
                    userContext.User.Update(user);
                    userContext.SaveChanges();
                    return TypedResults.Ok(user);

                }
                else
                {
                    return TypedResults.NotFound("not exist this user");
                }

            })
                .WithName("UserInfoUpdate")
                .WithOpenApi(option => new(option)
                {

                    Description = "UserInfoUpdate",
                    Summary = "UserInfoUpdate"
                })
                .WithTags("UserInfo");

            app.MapGet("/UserList", async Task<Results<Ok<List<UserProfile>>, NotFound<string>>> ([FromServices] UserContext userContext) =>
            {

                var query = await userContext.User.ToListAsync();
                if (query.Count > 0)
                {
                    //return Results.Json(query);
                    return TypedResults.Ok(query);
                }
                else
                {
                    return TypedResults.NotFound("not find anything");
                }

            })
              .WithName("GetUserList")
              .WithTags("UserManagment");
            


        }

    }
}
