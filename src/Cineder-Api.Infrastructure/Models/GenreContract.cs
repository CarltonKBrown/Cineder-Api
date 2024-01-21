using System.Text.Json;
using System.Text.Json.Serialization;
using Cineder_Api.Core.Entities;
using Cineder_Api.Core.Util;
using Cineder_Api.Infrastructure.Models;
using PreventR;

namespace Cineder_Api.Infrastructure;

public class GenreContract : BaseContract
{
    public GenreContract(long id, string name) : base(id)
    {
        Name = name.Prevent((nameof(name))).NullOrWhiteSpace();
    }

    public GenreContract() : this(0, string.Empty) { }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    public Genre ToGenre() => new(Id, Name);

    public override string ToString()
    {
        return JsonSerializer.Serialize(this, JsonUtil.Indent);
    }
}
