using Cineder_Api.Core.Util;
using PreventR;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cineder_Api.Infrastructure.Models
{
    public class AppendCastsContract
    {
        public AppendCastsContract(IEnumerable<CastContract> cast)
        {
            Cast = cast.Prevent(nameof(cast)).Null().Value;
        }

        public AppendCastsContract() : this(Enumerable.Empty<CastContract>())
        {

        }

        [JsonPropertyName("cast")]
        public IEnumerable<CastContract> Cast { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this, JsonUtil.Indent);
        }
    }
}
