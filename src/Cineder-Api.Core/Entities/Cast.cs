using Cineder_Api.Core.Util;
using PreventR;
using System.Linq;
using System.Text.Json;

namespace Cineder_Api.Core.Entities
{
    public class Cast : Entity
    {
        public Cast(long id, string name, long castId, string character, string creditId, int gender, int order, string profilePath) : base(id, name)
        {
            CastId = castId;
            Character = character.Prevent(nameof(character)).NullOrWhiteSpace();
            CreditId = creditId.Prevent(nameof(creditId)).NullOrWhiteSpace();
            Gender = gender;
            Order = order;
            ProfilePath = profilePath.Prevent(nameof(profilePath)).NullOrWhiteSpace();
        }

        public Cast() : this(0, string.Empty, 0, string.Empty, string.Empty, 0, 0, string.Empty)
        {

        }

        public long CastId { get; protected set; }
        public string Character { get; protected set; }
        public string CreditId { get; protected set; }
        public int Gender { get; protected set; }
        public int Order { get; protected set; }
        public string ProfilePath { get; protected set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this, JsonUtil.Indent);
        }
    }
}
