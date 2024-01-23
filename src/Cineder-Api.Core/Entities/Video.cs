using Cineder_Api.Core.Util;
using PreventR;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cineder_Api.Core.Entities
{
    public class Video : Entity
    {
        public Video(long id, string name, string isoLang, string isoRegion, string key, string site, int size, string type, bool official, DateTime publishedAt) : base(id, name)
        {
            IsoLang = isoLang.Prevent(nameof(isoLang)).NullOrWhiteSpace();
            IsoRegion = isoRegion.Prevent(nameof(isoRegion)).NullOrWhiteSpace();
            Key = key.Prevent(nameof(key)).NullOrWhiteSpace();
            Site = site.Prevent(nameof(site)).NullOrWhiteSpace();
            Size = size;
            Type = type.Prevent(nameof(type)).NullOrWhiteSpace();
            Official = official;
            PublishedAt = publishedAt;
        }

        public Video() : this(0, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 0, string.Empty, false, DateTime.Today)
        {

        }

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
