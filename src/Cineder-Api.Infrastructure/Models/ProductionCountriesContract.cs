using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cineder_Api.Infrastructure;

internal class ProductionCountriesContract
{
    public ProductionCountriesContract(string isoLang, string name)
    {
        IsoLang = isoLang;
        Name = name;
    }

    public ProductionCountriesContract() : this(string.Empty, string.Empty)
    {

    }

    [JsonPropertyName("iso_3166_1")]
    public string IsoLang { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this, new JsonSerializerOptions() { WriteIndented = true });
    }
}
