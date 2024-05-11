using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicManagementsMinimalAPI.Customer;
using MusicManagementsMinimalAPI.Data;
using MusicManagementsMinimalAPI.Models;
using MusicManagementsMinimalAPI.Models.Enum;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MusicManagementsMinimalAPI.Route
{
    public static class MusicRoute
    {
        public static void AddMusicTransactionEndPoint(this WebApplication app)
        {
            app.MapGroup("Music")
                .MapMusicManageApi()
                .WithOpenApi(opration => new(opration)
                {
                    //Deprecated = true ,
                    Description = "crud func of music ",
                    
                })
                .WithTags("MusicManagment");

            
            app.MapGet("/Search", async Task<Results<Ok<List<Music>>,NotFound<string>>> ([FromServices] MusicContext musicContext) =>
            {

                var query = await musicContext.Music.ToListAsync();
                if(query.Count > 0)
                {
                    //return Results.Json(query);
                    return TypedResults.Ok(query);
                }
                else
                {
                    return TypedResults.NotFound("not find anything");
                }
                
                
                

            })
               .WithName("GetMusicList")
               .WithTags("MusicManagment");

            app.MapGet("/SearchReview", async Task<Results<Ok<List<Music>>, NotFound<string>>> (
                string searchKey,[FromServices] MusicContext musicContext) =>
            {
                if (!string.IsNullOrWhiteSpace(searchKey!))
                {
                    var query = await musicContext.Music
                    .Where(e => e.Review == MusicReviewEnum.Access
                    &&(e.Name.Contains(searchKey) || e.Author.Contains(searchKey)))
                    .ToListAsync();
                    if (query.Count > 0)
                    {
                        //return Results.Json(query);
                        return TypedResults.Ok(query);
                    }
                    else
                    {
                        return TypedResults.NotFound("not find anything");
                    }
                }
                else
                {
                    var query = await musicContext.Music.Where(e => e.Review == MusicReviewEnum.Access).ToListAsync();
                    if (query.Count > 0)
                    {
                        //return Results.Json(query);
                        return TypedResults.Ok(query);
                    }
                    else
                    {
                        return TypedResults.NotFound("not find anything");
                    }
                }

            })
               .WithName("GetReviewMusicList")
               .WithTags("MusicManagment");

            

            app.MapGet("/Download", async Task<Results<Ok<Music>, NotFound<string>>> 
                ([FromQuery(Name = "id")] long musicId, [FromServices] MusicContext musicContext) =>
            {

                var music = musicContext.Music.Find(musicId) ?? null;
                if (music != null)
                {
                    var path = music.MusicContentUrl;
                    return TypedResults.Ok(music);
                }
                else
                {
                    return TypedResults.NotFound("music content assert not exist");
                }

            })
               .WithName("DownloadMusic")
               .WithTags("MusicManagment");

            app.MapGet("/Image",  ([FromQuery(Name = "id")] long musicId, [FromServices] MusicContext musicContext) =>
            {

                var music = musicContext.Music.Find(musicId) ?? null;
                if (music != null)
                {
                    var path=music.MusicImageUrl;
                    return TypedResults.Extensions.ImgResult(path);
                    
                }
                else
                {
                    return TypedResults.NotFound("image assert not exist");
                }

            })
               .WithName("ImageCover")
               .WithTags("MusicManagment");

            app.MapGet("/Media", ([FromQuery(Name = "id")] long musicId, [FromServices] MusicContext musicContext) =>
            {

                var music = musicContext.Music.Find(musicId) ?? null;
                if (music != null)
                {
                    var path = music.MusicContentUrl;
                    return TypedResults.Extensions.MediaResult(path);

                }
                else
                {
                    return TypedResults.NotFound("image assert not exist");
                }

            })
              .WithName("Media")
              .WithTags("MusicManagment");

        }
        

        static RouteGroupBuilder MapMusicManageApi(this RouteGroupBuilder group)
        {
            group.MapGet("/", async Task<Results<Ok<Music>,NotFound<string>>> (long musicId, [FromServices] MusicContext musicContext) =>
            {

                var music=musicContext.Music.Find(musicId)??null;
                if (music != null)
                {
                    return TypedResults.Ok(music);
                }
                else
                {
                    return TypedResults.NotFound("assert not exist");
                }
                
            })
                .WithName("GetMusic");
                

            group.MapPost("", async Task<Results<Ok<string>,NotFound<string>>>([FromBody] Music music,[FromServices] MusicContext musicContext) =>
            {
                var existMusic=musicContext.Music.Find(music.Id) ?? null;
                if(existMusic == null)
                {
                    await musicContext.Music.AddAsync(music);
                    musicContext.SaveChanges();
                    return TypedResults.Ok("share music success");
                }
                else
                {
                    return TypedResults.NotFound("exist this music");
                    
                }
                
            })
                .WithName("uploadMusic")
                .WithOpenApi();

            group.MapPut("", async Task<Results<Ok<string>,NotFound<string>>>([FromBody] Music music, [FromServices] MusicContext musicContext) =>
            {
                
                var existMusic = musicContext.Music.Find(music.Id) ?? null;
                if (existMusic != null)
                {
                    existMusic.Review=music.Review;
                    await musicContext.SaveChangesAsync();
                    return TypedResults.Ok("review success");
                }
                else
                {
                    return TypedResults.NotFound("this music not exit");
                }
            })
                .WithName("reviewMusic")
                .WithOpenApi();

            group.MapDelete("",  ([FromBody] Music music, [FromServices] MusicContext musicContext) =>
            {
                //不需要跟踪吗？
                musicContext.Music.Remove(music);
                musicContext.SaveChanges();
                return TypedResults.Ok("delete music success");
                
            })
                .WithName("delete music");



            return group;
        }
    }
}
