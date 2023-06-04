using System.Text.Json;

namespace Cineder_Api.Core.Entities
{
    public class Video : Entity
    {
        public Video(long id, string name, string isoLang, string isoRegion, string key, string site, int size, string type) : base(id, name)
        {
            IsoLang = isoLang;
            IsoRegion = isoRegion;
            Key = key;
            Site = site;
            Size = size;
            Type = type;
        }

        public Video() : this(0, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 0, string.Empty)
        {

        }

        public string IsoLang { get; protected set; }
        public string IsoRegion { get; protected set; }
        public string Key { get; protected set; }
        public string Site { get; protected set; }
        public int Size { get; protected set; }
        public string Type { get; protected set; }

        public override string ToString()
        {
            var opt = new JsonSerializerOptions() { WriteIndented = true };
            return JsonSerializer.Serialize(this, opt);
        }
    }
}
