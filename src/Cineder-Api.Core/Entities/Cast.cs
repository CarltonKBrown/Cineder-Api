using Cineder_Api.Core.Util;
using PreventR;
using System.Text.Json;

namespace Cineder_Api.Core.Entities
{
    public class Cast : Entity
    {
        public Cast(long id, string name, string character, string creditId, int gender, int order, string profilePath, bool adult, string knownForDepartment, double popularity) : base(id, name)
        {
            Character = character.Prevent(nameof(character)).Null();
            CreditId = creditId.Prevent(nameof(creditId)).Null();
            Gender = gender;
            Order = order;
            ProfilePath = profilePath;
        }

        public Cast() : this(0, string.Empty, string.Empty, string.Empty, 0, 0, string.Empty, false, string.Empty, 0.0)
        {

        }

        public string Character { get; protected set; }
        public string CreditId { get; protected set; }
        public int Gender { get; protected set; }
        public int Order { get; protected set; }
        public string ProfilePath { get; protected set; }

        public bool Adult { get; protected set; }
        public string KnownForDepartment { get; protected set; }
        public double Popularity { get; protected set; }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this, JsonUtil.Indent);
        }
    }
}
