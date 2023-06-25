using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cineder_Api.Infrastructure.Models
{
    internal class AppendCastsContract
    {
        internal AppendCastsContract(IEnumerable<CastContract> cast)
        {
            Cast = cast;
        }

        internal AppendCastsContract() : this(Enumerable.Empty<CastContract>())
        {

        }

        [JsonPropertyName("cast")]
        internal IEnumerable<CastContract> Cast { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this, new JsonSerializerOptions() { WriteIndented = true });
        }
    }
}
