using System.Text.Json;
using System.Text.Json.Serialization;
using Cineder_Api.Core.Entities;
using Cineder_Api.Infrastructure.Models;

namespace Cineder_Api.Infrastructure;

internal class CreatedByContract : BaseContract
{
    public CreatedByContract(long id, string creditId, string name, string gender, string profilePath) : base(id)
    {
        CreditId = creditId;
        Name = name;
        Gender = gender;
        ProfilePath = profilePath;
    }

    public CreatedByContract() : this(0, string.Empty, string.Empty, string.Empty, string.Empty) { }

    [JsonPropertyName("credit_id")]
    public string CreditId { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("gender")]
    public string Gender { get; set; }

    [JsonPropertyName("profile_path")]
    public string ProfilePath { get; set; }

    public CreatedBy ToCreatedBy()
    {
        return new(Id, Name, CreditId, Gender, ProfilePath);
    }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this, new JsonSerializerOptions() { WriteIndented = true });
    }
}
