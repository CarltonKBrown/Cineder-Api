using Cineder_Api.Core.Util;
using PreventR;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cineder_Api.Infrastructure.Models
{
    public class AppendVideosContract
    {
        public AppendVideosContract(IEnumerable<VideoContract> results)
        {
            Results = results.Prevent(nameof(results)).Null().Value;
        }

        public AppendVideosContract() : this(Enumerable.Empty<VideoContract>())
        {

        }

        [JsonPropertyName("results")]
        public IEnumerable<VideoContract> Results { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this, JsonUtil.Indent);
        }
    }
}
