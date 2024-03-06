using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MusicManagementsMinimalAPI.Data;
using MusicManagementsMinimalAPI.Models;

namespace MusicManagementsMinimalAPI.Route
{
    public static class UserRoute
    {
        public static void AddLoginTransactionEndPoint(this WebApplication app)
        {
            app.MapPost("UserLogin", Results<Ok<UserProfile>,NotFound<string>> ([FromBody] UserProfile userProfile, [FromServices] UserContext userContext) =>
            {
                var user=userContext.User.Find(userProfile.Id)??null;
                if (user != null)
                {
                    return TypedResults.Ok(user);
                }
                else
                {
                    return TypedResults.NotFound("not exist this user");
                }

            })
                .WithName("UsesrLogin")
                .WithOpenApi(option => new(option)
                {

                    Description = "UsesrLogin",
                    Summary = "UsesrLogin"
                })
                .WithTags("UsesrLogin");
        }

    }
}
