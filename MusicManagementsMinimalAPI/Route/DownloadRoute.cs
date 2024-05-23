using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MusicManagementsMinimalAPI.Customer;
using MusicManagementsMinimalAPI.Data;
using MusicManagementsMinimalAPI.Models;

namespace MusicManagementsMinimalAPI.Route
{
    public static class DownloadRoute
    {
        public static void AddDownloadTransactionEndPoint(this WebApplication app)
        {

            app.MapGroup("DownloadMusic")
                .MapDownloadManagApi()
                .WithOpenApi(opration => new(opration)
                {
                    Description = "crud func of Download music",
                })
                .WithTags("DownloadManagment");
        }

        static RouteGroupBuilder MapDownloadManagApi(this RouteGroupBuilder group)
        {
            group.MapGet("", async  ([FromQuery(Name = "MusicId")] long musicId,
                [FromQuery(Name = "UserId")] long userId,
                [FromQuery(Name = "Time")] DateTime time,
                MusicContext musicContext) =>
            {
                var musicDownload = new DownloadRelate
                {
                    MusicId = musicId,
                    UserId = userId,
                    time = time,
                };
                musicContext.DownloadDB.Add(musicDownload);
                var music = musicContext.Music.Find(musicDownload.MusicId);
                music!.DownLoadNum += 1;
                var path = music.MusicContentUrl;
                await musicContext.SaveChangesAsync();
                
                return TypedResults.Extensions.MediaResult(path);

            })
                .WithName("musicDownload")
                .WithOpenApi(option => new(option)
                {

                    Description = "musicDownload music ",
                    Summary = "musicDownload music"
                });

            return group;


        }
    }
}
