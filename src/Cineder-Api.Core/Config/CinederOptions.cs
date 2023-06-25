namespace Cineder_Api.Core.Config
{
    public class CinederOptions
    {
        public string ApiBaseUrl { get; set; } = string.Empty;
        public string ApiKey { get; set; } = string.Empty;
        public string Language { get; set; } = string.Empty;
        public string ClientName { get; set; } = string.Empty;
        public int MaxPages { get; set; } = 1;

        public static string SectionName => "";
    }
}
