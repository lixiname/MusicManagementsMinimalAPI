
namespace MusicManagementsMinimalAPI.Customer
{
    public static class ResultsExtensions
    {
        public static IResult ImgResult(this IResultExtensions resultExtensions, string path)
        {
            ArgumentNullException.ThrowIfNull(resultExtensions, nameof(resultExtensions));
            return new ImageResult(path);
            
        }

        
    }
}
