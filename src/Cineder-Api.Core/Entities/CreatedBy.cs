using System.Text.Json;

namespace Cineder_Api.Core.Entities
{
    public class CreatedBy : Entity
    {
        public CreatedBy(long id, string name, string creditId, string gender, string profilePath) : base(id, name)
        {
            CreditId = creditId;
            Gender = gender;
            ProfilePath = profilePath;
        }

        public CreatedBy() : this(0, string.Empty, string.Empty, string.Empty, string.Empty)
        {

        }
        public string CreditId { get; protected set; }
        public string Gender { get; protected set; }
        public string ProfilePath { get; protected set; }

        public override string ToString()
        {
            var opt = new JsonSerializerOptions() { WriteIndented = true };
            return JsonSerializer.Serialize(this, opt);
        }
    }
}
