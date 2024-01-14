using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cineder_Api.Infrastructure.Models
{
    internal class AppendCreditsContract
    {
        internal AppendCreditsContract(IEnumerable<CastContract> cast, IEnumerable<CrewMContract> crew)
        {
            Cast = cast;
            Crew = crew;
        }

        internal AppendCreditsContract() : this(Enumerable.Empty<CastContract>(), Enumerable.Empty<CrewMContract>()) { }

        [JsonPropertyName("cast")]
        internal IEnumerable<CastContract> Cast { get; set; }
        [JsonPropertyName("crew")]
        internal IEnumerable<CrewMContract> Crew { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this, new JsonSerializerOptions() { WriteIndented = true });
        }
    }
}
