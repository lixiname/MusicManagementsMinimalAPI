using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicManagementsMinimalAPI.Data;
using MusicManagementsMinimalAPI.Models;
using MusicManagementsMinimalAPI.Models.DTO;

namespace MusicManagementsMinimalAPI.Route
{
    public static class TalkRoute
    {
        public static void AddTalkTransactionEndPoint(this WebApplication app)
        {

            app.MapGroup("Talk")
                .MapTalkManagApi()
                .WithOpenApi(opration => new(opration)
                {
                    Description = "crud func of talk",
                })
                .WithTags("TalkManagment");


            app.MapGet("/SingleMusicTalkSearch", async Task<Results<Ok<List<TalkDTO>>, NotFound<string>>> ([FromQuery(Name = "MusicId")] int MusicId, [FromServices] MusicContext musicContext) =>
            {
                var talkList = from userProfile in musicContext.UserProfile
                                  join talk in musicContext.Talk
                                  on userProfile.Id equals talk.TalkId
                                  where talk.MusicId== MusicId
                                  select new TalkDTO
                                  {
                                      TalkId = userProfile.Id,
                                      UserName = userProfile.Name,
                                      UploadUserId = talk.UploadUserId,
                                      MusicId = talk.MusicId,
                                  };

                if (talkList.Any())
                {
                    return TypedResults.Ok(talkList.ToList());
                }
                else
                {
                    return TypedResults.NotFound("nobody talk anything");
                }

            })
               .WithName("GetSingleMusicTalkList")
                .WithOpenApi(option => new(option)
                {
                    
                    Description = "get talk of single music ",
                    Summary = "get talk of single music"
                })
                .WithTags("TalkManagment");


        }

        static RouteGroupBuilder MapTalkManagApi(this RouteGroupBuilder group)
        {
            group.MapPost("",  ([FromBody] TalkRelate talk, MusicContext musicContext) =>
            {
                musicContext.Talk.Add(talk);
                var music = musicContext.Music.Find(talk.MusicId);
                music!.TalkNum += 1;
                musicContext.SaveChanges();
                return TypedResults.Ok("talk success");


            })
                .WithName("TalkMusic")
                .WithOpenApi(option => new(option)
                {

                    Description = "collect music ",
                    Summary = "collect music"
                });


            group.MapDelete("", ([FromQuery(Name = "TalkId")] int talkId, [FromQuery(Name = "MusicId")] int musicId,
                MusicContext musicContext) =>
            {
                
                var findTalk=musicContext.Talk.SingleOrDefault(x=>x.MusicId == musicId && x.TalkId == talkId);
                musicContext.Talk.Remove(findTalk!);
                musicContext.SaveChanges();
                var music = musicContext.Music.Find(musicId);
                music!.TalkNum -= 1;
                musicContext.SaveChanges();
                return TypedResults.Ok("delete talk  success");

            })
                .WithName("DeleteTalk");

            return group;

        }
    }
}
