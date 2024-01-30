using System.Text.Json;
using System.Text.Json.Serialization;
using Cineder_Api.Core.Entities;
using Cineder_Api.Core.Util;
using Cineder_Api.Infrastructure.Models;
using PreventR;

namespace Cineder_Api.Infrastructure;

internal class NetworksContract : BaseContract
{
    public NetworksContract(long id, string name, string logoPath, string originCountry) : base(id)
    {
        Name = name.Prevent(nameof(name)).Null();
        LogoPath = logoPath.Prevent(nameof(logoPath)).Null();
        OriginCountry = originCountry.Prevent(nameof(originCountry)).Null();
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
        return JsonSerializer.Serialize(this, JsonUtil.Indent);
    }
}
