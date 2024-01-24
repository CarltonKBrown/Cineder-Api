using Cineder_Api.Core.Util;
using PreventR;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cineder_Api.Core.Entities
{
    public class Video
    {
        public Video(string id, string name, string isoLang, string isoRegion, string key, string site, int size, string type, bool official, DateTime publishedAt)
        {
            Id = id.Prevent(nameof(id)).Null();
            Name = name.Prevent(nameof(name)).Null();
            IsoLang = isoLang.Prevent(nameof(isoLang)).Null();
            IsoRegion = isoRegion.Prevent(nameof(isoRegion)).Null();
            Key = key.Prevent(nameof(key)).Null();
            Site = site.Prevent(nameof(site)).Null();
            Size = size;
            Type = type.Prevent(nameof(type)).Null();
            Official = official;
            PublishedAt = publishedAt;
        }

        public Video() : this(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 0, string.Empty, false, DateTime.Today)
        {

        }

        public string Id { get; protected set; }
        public string Name { get; protected set; }
        public string IsoLang { get; protected set; }
        public string IsoRegion { get; protected set; }
        public string Key { get; protected set; }
        public string Site { get; protected set; }
        public int Size { get; protected set; }
        public string Type { get; protected set; }
        public bool Official { get; set; }
        public DateTime PublishedAt { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this, JsonUtil.Indent);
        }
    }
}
