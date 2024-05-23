using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicManagementsMinimalAPI.Customer;
using MusicManagementsMinimalAPI.Data;
using MusicManagementsMinimalAPI.Models;
using MusicManagementsMinimalAPI.Models.DTO;
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
            
            app.MapGet("/SearchLineChart", async Task<Results<Ok<List<MusicAgreedTopSortDTO>>, NotFound<string>>> (
                [FromQuery(Name = "year")] int year,
                [FromQuery(Name = "month")] int month,
                [FromQuery(Name = "musicId")] long musicId,
                [FromServices] MusicContext musicContext) =>
            {

                var music=await musicContext.Music.FindAsync(musicId);
                
                var musicSort = await musicContext.AgreedDB
                .Where(item => item.time.Year == year && item.time.Month == month&&item.MusicId==musicId)
                .GroupBy(item => item.time.Day)
                .OrderBy(item => item.First().time.Day)
                .Select(group => new MusicAgreedTopSortDTO
                { 
                    MusicId=music!.Id,
                    MusicName=music.Name,
                    Count = group.Count(),
                    Day=group.First().time.Day,
                    
                }).ToListAsync();
               
                if (musicSort.Count>0)
                {
                    //return Results.Json(query);
                    return TypedResults.Ok(musicSort);
                }
                else
                {
                    return TypedResults.NotFound("not find anything");
                }




            })
               .WithName("GetBarChartMusicList")
               .WithTags("MusicChart");

            app.MapGet("/SearchBarChart", async Task<Results<Ok<List<MusicCollectedLineDTO>>, NotFound<string>>> (
                [FromQuery(Name = "year")] int year,
                [FromQuery(Name = "month")] int month,
                [FromServices] MusicContext musicContext) =>
            {

                var musicSort = await musicContext.UserMusicRelate
                .Where(item => item.time.Year == year && item.time.Month == month)
                .GroupBy(item => item.MusicId)
                .OrderByDescending(item => item.Count())
                .Select(group => new {
                    MusicId = group.First().MusicId,
                    Count = group.Count()
                }).ToListAsync();
                var query = from agreedMusic in musicSort
                            join music in musicContext.Music
                            on agreedMusic.MusicId equals music.Id
                            select new MusicCollectedLineDTO
                            {
                                MusicId = music.Id,
                                Count = agreedMusic.Count,
                                MusicName = music.Name,
                                Month = month,
                            };
                if (query.Any())
                {
                    //return Results.Json(query);
                    return TypedResults.Ok(query.ToList());
                }
                else
                {
                    return TypedResults.NotFound("not find anything");
                }




            })
               .WithName("GetLineChartMusicList")
               .WithTags("MusicChart");


            app.MapGet("/SearchPieChart", async Task<Results<Ok<List<MusicDownloadPieDTO>>, NotFound<string>>> (
               [FromQuery(Name = "year")] int year,
               [FromQuery(Name = "month")] int month,
               [FromServices] MusicContext musicContext) =>
            {

                var musicSort = await musicContext.DownloadDB
               .Where(item => item.time.Year == year && item.time.Month == month)
               .GroupBy(item => item.MusicId)
               .OrderByDescending(item => item.Count())
               .Select(group => new {
                   MusicId = group.First().MusicId,
                   Count = group.Count()
               }).ToListAsync();
                var query = from agreedMusic in musicSort
                            join music in musicContext.Music
                            on agreedMusic.MusicId equals music.Id
                            select new MusicDownloadPieDTO
                            {
                                MusicId = music.Id,
                                Count = agreedMusic.Count,
                                MusicName = music.Name,
                                Month = month,
                            };
                if (query.Any())
                {
                    //return Results.Json(query);
                    return TypedResults.Ok(query.ToList());
                }
                else
                {
                    return TypedResults.NotFound("not find anything");
                }


            })
              .WithName("GetPieChartMusicList")
              .WithTags("MusicChart");



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
