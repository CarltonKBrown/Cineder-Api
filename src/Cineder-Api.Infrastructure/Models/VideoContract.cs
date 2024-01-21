using Cineder_Api.Core.Entities;
using Cineder_Api.Core.Util;
using PreventR;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cineder_Api.Infrastructure.Models
{
    public class VideoContract : BaseContract
    {
        public VideoContract(long id, string name, string isoLang, string isoRegion, string key, string site, int size, string type) : base(id)
        {
            Name = name.Prevent().NullOrWhiteSpace();
            IsoLang = isoLang.Prevent().NullOrWhiteSpace();
            IsoRegion = isoRegion.Prevent().NullOrWhiteSpace();
            Key = key.Prevent().NullOrWhiteSpace();
            Site = site.Prevent().NullOrWhiteSpace();
            Size = size;
            Type = type.Prevent().NullOrWhiteSpace();
        }

        public VideoContract() : this(0, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 0, string.Empty) { }

        [JsonPropertyName("iso_3166_1")]
        public string IsoLang { get; set; }

        [JsonPropertyName("iso_639_1")]
        public string IsoRegion { get; set; }

        [JsonPropertyName("key")]
        public string Key { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("site")]
        public string Site { get; set; }

        [JsonPropertyName("size")]
        public int Size { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }


        public Video ToVideo() => new (Id, Name, IsoLang, IsoRegion, Key, Site, Size, Type);

        public override string ToString()
        {
            return JsonSerializer.Serialize(this, JsonUtil.Indent);
        }
    }
}
