using System.Text.Json;

namespace Cineder_Api.Core.Entities
{
    public class Genre : Entity
    {
        public Genre(long id, string name) : base(id, name)
        {

        }

        public Genre() : this(0, string.Empty)
        {

        }

        public override string ToString()
        {
            var opt = new JsonSerializerOptions() { WriteIndented = true };
            return JsonSerializer.Serialize(this, opt);
        }
    }
}
