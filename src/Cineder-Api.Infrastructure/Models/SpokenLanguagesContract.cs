using Cineder_Api.Infrastructure.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cineder_Api.Infrastructure;

internal class SpokenLanguagesContract:BaseContract
{
    public SpokenLanguagesContract(long id, string isoRegion, string name) : base(id)
    {
        IsoRegion = isoRegion;
        Name = name;
    }

    public SpokenLanguagesContract() : this(0, string.Empty, string.Empty){}

    [JsonPropertyName("iso_639_1")]
    public string IsoRegion { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this, new JsonSerializerOptions() { WriteIndented = true });
    }
}
