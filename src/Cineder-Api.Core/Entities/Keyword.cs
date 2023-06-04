using System.Text.Json;

namespace Cineder_Api.Core.Entities
{
    public class Keyword: Entity
    {
        public Keyword(long id, string name):base(id, name) { }

        public Keyword():this(0, string.Empty) { }

        public override string ToString()
        {
            var opt = new JsonSerializerOptions() { WriteIndented = true };
            return JsonSerializer.Serialize(this, opt);
        }
    }
}
