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
            Name = name.Prevent().Null();
            IsoLang = isoLang.Prevent().Null();
            IsoRegion = isoRegion.Prevent().Null();
            Key = key.Prevent().Null();
            Site = site.Prevent().Null();
            Size = size;
            Type = type.Prevent().Null();
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
