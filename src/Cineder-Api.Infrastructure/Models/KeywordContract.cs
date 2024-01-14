using System.Text.Json;
using System.Text.Json.Serialization;
using Cineder_Api.Core.Entities;
using Cineder_Api.Infrastructure.Models;

namespace Cineder_Api.Infrastructure;

internal class KeywordContract : BaseContract
{
    public KeywordContract(long id, string name) : base(id)
    {
        Name = name;
    }

    public KeywordContract() : this(0, string.Empty) { }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    public Keyword ToKeyword() => new(Id, Name);

    public override string ToString()
    {
        return JsonSerializer.Serialize(this, new JsonSerializerOptions() { WriteIndented = true });
    }
}
