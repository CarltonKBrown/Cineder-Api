using System.Text.Json;
using System.Text.Json.Serialization;
using Cineder_Api.Core.Entities;
using Cineder_Api.Infrastructure.Models;

namespace Cineder_Api.Infrastructure;

internal class NetworksContract : BaseContract
{
    public NetworksContract(long id, string name, string logoPath, string originCountry) : base(id)
    {
        Name = name;
        LogoPath = logoPath;
        OriginCountry = originCountry;
    }

    public NetworksContract() : this(0, string.Empty, string.Empty, string.Empty)
    {

    }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("logo_path")]
    public string LogoPath { get; set; }

    [JsonPropertyName("origin_country")]
    public string OriginCountry { get; set; }

    public Network ToNetwork()
    {
        return new(Id, Name, LogoPath, OriginCountry);
    }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this, new JsonSerializerOptions() { WriteIndented = true });
    }
}
