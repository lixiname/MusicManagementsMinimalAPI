using Microsoft.AspNetCore.Http.HttpResults;
using MusicManagementsMinimalAPI.Data;
using MusicManagementsMinimalAPI.Models.DTO;
using MusicManagementsMinimalAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace MusicManagementsMinimalAPI.Route
{
    public static class AgreedRoute
    {
        public static void AddAgreedTransactionEndPoint(this WebApplication app)
        {

            app.MapGroup("Agreed")
                .MapAgreedManagApi()
                .WithOpenApi(opration => new(opration)
                {
                    Description = "crud func of Agreed music",
                })
                .WithTags("AgreedManagment");
        }

        static RouteGroupBuilder MapAgreedManagApi(this RouteGroupBuilder group)
        {
            group.MapPost("", async Task<Results<Ok<string>, NotFound<string>>> ([FromBody] AgreedRelate musicAgreed, MusicContext musicContext) =>
            {
                var existAgreed = musicContext.AgreedDB
                    .SingleOrDefault(x => x.MusicId == musicAgreed.MusicId && x.UserId == musicAgreed.UserId);
                if (existAgreed != null)
                {
                    return TypedResults.NotFound("Agreed it don't Agreed again");
                }
                else
                {

                    musicContext.AgreedDB.Add(musicAgreed);
                    var music = musicContext.Music.Find(musicAgreed.MusicId);
                    music!.AgreedNum += 1;
                    await musicContext.SaveChangesAsync();

                    return TypedResults.Ok("Agreed success");
                }

            })
                .WithName("AgreedMusic")
                .WithOpenApi(option => new(option)
                {

                    Description = "Agreed music ",
                    Summary = "Agreed music"
                });


            group.MapDelete("", ([FromQuery(Name = "UserId")] long userId, [FromQuery(Name = "MusicId")] long musicId,
                MusicContext musicContext) =>
            {
                var agreed = new AgreedRelate
                {
                    MusicId = musicId,
                    UserId = userId,
                };
                musicContext.AgreedDB.Remove(agreed);
                musicContext.SaveChanges();
                var music = musicContext.Music.Find(musicId);

                music!.AgreedNum -= 1;
                musicContext.SaveChanges();
                return TypedResults.Ok("quit Agreed music success");

            })
                .WithName("DeleteAgreedMusic");

            return group;

        }
    }
}
