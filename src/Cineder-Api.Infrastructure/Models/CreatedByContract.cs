using System.Text.Json;
using System.Text.Json.Serialization;
using Cineder_Api.Core.Entities;
using Cineder_Api.Core.Util;
using Cineder_Api.Infrastructure.Models;
using PreventR;

namespace Cineder_Api.Infrastructure;

internal class CreatedByContract : BaseContract
{
    public CreatedByContract(long id, string creditId, string name, int gender, string profilePath) : base(id)
    {
        CreditId = creditId.Prevent(nameof(creditId)).Null();
        Name = name.Prevent(nameof(name)).Null();
        Gender = gender;
        ProfilePath = profilePath.Prevent(nameof(profilePath)).Null();
    }

    public CreatedByContract() : this(0, string.Empty, string.Empty, 0, string.Empty) { }

    [JsonPropertyName("credit_id")]
    public string CreditId { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("gender")]
    public int Gender { get; set; }

    [JsonPropertyName("profile_path")]
    public string ProfilePath { get; set; }

    public CreatedBy ToCreatedBy()
    {
        return new(Id, Name, CreditId, Gender, ProfilePath);
    }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this, JsonUtil.Indent);
    }
}
