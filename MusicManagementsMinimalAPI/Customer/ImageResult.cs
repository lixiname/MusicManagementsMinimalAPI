
using System.IO;
using System.Net;
using System.Net.Mime;
using System.Text;

namespace MusicManagementsMinimalAPI.Customer
{
    public class ImageResult:IResult
    {
        private readonly string _imagePath;
        public ImageResult(string path)
        {
            _imagePath = path;
        }

        public Task ExecuteAsync(HttpContext httpContext)
        {
            var imageBytes = File.ReadAllBytes(_imagePath);
            httpContext.Response.ContentType = MediaTypeNames.Image.Png;
            httpContext.Response.StatusCode = (int)HttpStatusCode.OK;
            //var conetent=new ByteArrayContent(imageBytes);
            return httpContext.Response.Body.WriteAsync(imageBytes,0,imageBytes.Length);
        }
    }
}
