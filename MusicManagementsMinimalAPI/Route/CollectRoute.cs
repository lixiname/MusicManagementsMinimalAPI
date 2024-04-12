using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicManagementsMinimalAPI.Data;
using MusicManagementsMinimalAPI.Models;
using MusicManagementsMinimalAPI.Models.DTO;

namespace MusicManagementsMinimalAPI.Route
{
    public static class CollectRoute
    {
        public static void AddCollectTransactionEndPoint(this WebApplication app)
        {
            
            app.MapGroup("Collect")
                .MapCollectManagApi()
                .WithOpenApi(opration => new(opration)
                {
                    Description = "crud func of collect music",
                })
                .WithTags("CollectManagment");
            

            app.MapGet("/CollectSearch",async Task<Results<Ok<List<MusicLikeDTO>>,NotFound<string>>> ([FromQuery(Name ="UserId")] long userId, [FromServices] MusicContext musicContext) =>
            {
                Console.WriteLine();
                var collectList=from music in musicContext.Music
                join collect in musicContext.UserMusicRelate
                on music.Id equals collect.MusicId
                where collect.UserId == userId
                select new MusicLikeDTO
                {
                    Id = music.Id,
                    UploadUserId = music.UploadUserId,
                    Name = music.Name,
                    MusicImageUrl = music.MusicImageUrl,
                    Author = music.Author,
                    MusicContentUrl = music.MusicContentUrl,
                    MusicType = music.MusicType,
                    AgreedNum=music.AgreedNum,
                    DownLoadNum=music.DownLoadNum,
                    TalkNum=music.TalkNum,
                    UsingNum=music.UsingNum,
                    CollectNum=music.CollectNum,
                    UserId = collect.UserId,
                };
                
                if (collectList.Any())
                {
                    return TypedResults.Ok(collectList.ToList());
                }
                else
                {
                    return TypedResults.NotFound("not collect anything");
                }
                
            })
               .WithName("GetCollectMusicList")
                .WithOpenApi(option => new(option)
                {
                    OperationId = "getCollectList",
                    Description = "get music of user Like",
                    Summary = "get music list of user collect"
                })
                .WithTags("CollectManagment");


        }
        
        static RouteGroupBuilder MapCollectManagApi(this RouteGroupBuilder group)
        {
            group.MapPost("", async Task<Results<Ok<string>,NotFound<string>>> ([FromBody] UserMusicRelate musicCollect, MusicContext musicContext) =>
            {
                var existCollect=musicContext.UserMusicRelate
                    .SingleOrDefault(x => x.MusicId == musicCollect.MusicId && x.UserId == musicCollect.UserId);
                if (existCollect != null)
                {
                    return TypedResults.NotFound("collected it don't collect again");
                }
                else
                {
                    
                    musicContext.UserMusicRelate.Add(musicCollect);
                    var music = musicContext.Music.Find(musicCollect.MusicId);
                    //music!.AgreedNum += 1;
                    music!.CollectNum += 1;
                    await musicContext.SaveChangesAsync();
                    
                    return TypedResults.Ok("collect success");
                }
               
            })
                .WithName("CollectMusic")
                .WithOpenApi(option => new(option)
                {
                    
                    Description = "collect music ",
                    Summary = "collect music"
                });


            group.MapDelete("", ([FromQuery(Name = "UserId")] long userId, [FromQuery(Name = "MusicId")] long musicId,
                MusicContext musicContext) =>
            {
                var collect = new UserMusicRelate
                {
                    MusicId = musicId,
                    UserId = userId,
                };
                musicContext.UserMusicRelate.Remove(collect);
                musicContext.SaveChanges();
                var music=musicContext.Music.Find(musicId);
               
                music!.CollectNum -= 1;
                musicContext.SaveChanges();
                return TypedResults.Ok("quit collect music success");

            })
                .WithName("DeleteCollectMusic");
               
            return group;
            
        }
        
    }

}
