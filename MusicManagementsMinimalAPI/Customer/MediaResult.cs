using System.Net.Mime;
using System.Net;

namespace MusicManagementsMinimalAPI.Customer
{
    public class MediaResult : IResult
    {
        private readonly string _mediaPath;
        public MediaResult(string path)
        {
            _mediaPath = path;
        }

        public Task ExecuteAsync(HttpContext httpContext)
        {
            var imageBytes = File.ReadAllBytes(_mediaPath);
            httpContext.Response.ContentType = "audio/mp3";
            httpContext.Response.StatusCode = (int)HttpStatusCode.OK;
            //var conetent=new ByteArrayContent(imageBytes);
            return httpContext.Response.Body.WriteAsync(imageBytes, 0, imageBytes.Length);
        }
    }
}
