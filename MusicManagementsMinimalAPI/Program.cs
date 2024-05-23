using Microsoft.AspNetCore.Mvc;
using MusicManagementsMinimalAPI.Data;

using MusicManagementsMinimalAPI.Common.Config;
using Microsoft.AspNetCore.Http.HttpResults;
using MusicManagementsMinimalAPI.Models;
using Microsoft.EntityFrameworkCore;
using MusicManagementsMinimalAPI.Route;
namespace MusicManagementsMinimalAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.AddServicesConfig();
            builder.AddYitIdHelperConfig();
            builder.AddRedisConfig();
            builder.AddEmailConfig();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            app.UseSwagger();
            app.UseSwaggerUI();
            //1 (b,c,d)=(a.b,a.c,a.d)Ԫ�� 2 model���Զ��ƶ�Key���� 3 ��ν��EF�����Ƿ�Ĭ�����Զ�����
            app.AddLoginTransactionEndPoint();
            app.AddMusicTransactionEndPoint();
            app.AddCollectTransactionEndPoint();
            app.AddDownloadTransactionEndPoint();
            app.AddAgreedTransactionEndPoint();
            app.AddTalkTransactionEndPoint();
            
            app.Run();
        }
    }
}
