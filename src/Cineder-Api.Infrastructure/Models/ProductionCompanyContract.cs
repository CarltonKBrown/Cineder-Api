using System.Text.Json;
using System.Text.Json.Serialization;
using Cineder_Api.Core.Entities;
using Cineder_Api.Core.Util;
using Cineder_Api.Infrastructure.Models;
using PreventR;

namespace Cineder_Api.Infrastructure;

public class ProductionCompanyContract : BaseContract
{
    public ProductionCompanyContract(long id, string name, string logoPath, string originCountry) : base(id)
    {
        Name = name.Prevent(nameof(name)).Null();
        LogoPath = logoPath.Prevent(nameof(logoPath)).Null();
        OriginCountry = originCountry.Prevent(nameof(originCountry)).Null();
    }

    public ProductionCompanyContract() : this(0, string.Empty, string.Empty, string.Empty)
    {

    }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("logo_path")]
    public string LogoPath { get; set; }

    [JsonPropertyName("origin_country")]
    public string OriginCountry { get; set; }

    public ProductionCompany ToProductionCompany() => new(Id, Name, LogoPath ?? string.Empty, OriginCountry ?? string.Empty);

    public override string ToString()
    {
        return JsonSerializer.Serialize(this, JsonUtil.Indent);
    }
}
