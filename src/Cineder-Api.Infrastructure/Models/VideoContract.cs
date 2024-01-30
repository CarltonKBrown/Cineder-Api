using Cineder_Api.Core.Entities;
using Cineder_Api.Core.Util;
using PreventR;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cineder_Api.Infrastructure.Models
{
    public class VideoContract
    {
        public VideoContract(string id, string name, string isoLang, string isoRegion, string key, string site, int size, string type, bool official, DateTime publishedAt)
        {
            Id = id.Prevent(nameof(id)).Null();
            Name = name.Prevent(nameof(name)).Null();
            IsoLang = isoLang.Prevent(nameof(isoLang)).Null();
            IsoRegion = isoRegion.Prevent().Null();
            Key = key.Prevent().Null();
            Site = site.Prevent().Null();
            Size = size;
            Type = type.Prevent().Null();
            PublishedAt = publishedAt;
            Official = official;
        }

        public VideoContract() : this(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 0, string.Empty, false, DateTime.Now) { }

        [JsonPropertyName("id")]
        public string Id { get; set; }

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

        [JsonPropertyName("official")]
        public bool Official { get; set; }

        [JsonPropertyName("published_at")]
        public DateTime PublishedAt { get; set; }


        public Video ToVideo() => new (Id, Name, IsoLang, IsoRegion, Key, Site, Size, Type, Official, PublishedAt);

        public override string ToString()
        {
            return JsonSerializer.Serialize(this, JsonUtil.Indent);
        }
    }
}
