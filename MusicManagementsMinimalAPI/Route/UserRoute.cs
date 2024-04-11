using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MusicManagementsMinimalAPI.Data;
using MusicManagementsMinimalAPI.Models;
using MusicManagementsMinimalAPI.Models.DTO;

namespace MusicManagementsMinimalAPI.Route
{
    public static class UserRoute
    {
        public static void AddLoginTransactionEndPoint(this WebApplication app)
        {
            app.MapPost("UserLogin", Results<Ok<UserProfile>,NotFound<string>> ([FromBody] UserProfile userProfile, [FromServices] UserContext userContext) =>
            {
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
                .WithTags("UserLogin");



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

            app.MapPost("UserInfoUpdate", Results<Ok<UserProfile>, NotFound<string>> ([FromBody] UserProfile userProfile, [FromServices] UserContext userContext) =>
            {
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
                .WithTags("UserRegister");


            app.MapPost("UserInfoUpdate", Results<Ok<UserProfile>, NotFound<string>> ([FromBody] UserProfileUpdateDTO userProfile, [FromServices] UserContext userContext) =>
            {
                var user = userContext.User.FirstOrDefault(e => e.UserId == userProfile.UserId) ?? null;
                if (user != null)
                {
                    user.Name = userProfile.Name;
                    user.Phone = userProfile.Phone;
                    user.Email = userProfile.Email;
                    userContext.User.Add(user);
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
                .WithTags("UserInfoUpdate");
            
        }

    }
}
