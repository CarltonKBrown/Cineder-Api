using Cineder_Api.Core.Entities;
using Cineder_Api.Core.Util;
using PreventR;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cineder_Api.Infrastructure.Models
{
    public class CastContract : BaseContract
    {
        public CastContract(long id, string character, string creditId, int gender, string name, int order, string profilePath, bool adult, string knownForDepartment, double popularity) : base(id)
        {
            Character = character.Prevent(nameof(character)).Null();
            CreditId = creditId.Prevent(nameof(creditId)).Null();
            Gender = gender;
            Name = name.Prevent(nameof(name)).Null();
            Order = order;
            ProfilePath = profilePath.Prevent(nameof(profilePath)).Null();
            Adult = adult;
            KnownForDepartment = knownForDepartment;
            Popularity = popularity;
        }

        public CastContract() : this(0, string.Empty, string.Empty, 0, string.Empty, 0, string.Empty, false, string.Empty, 0.0) { }

        [JsonPropertyName("character")]
        public string Character { get; set; }

        [JsonPropertyName("credit_id")]
        public string CreditId { get; set; }

        [JsonPropertyName("gender")]
        public int Gender { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("order")]
        public int Order { get; set; }

        [JsonPropertyName("profile_path")]
        public string ProfilePath { get; set; }

        [JsonPropertyName("adult")]
        public bool Adult { get; set; }

        [JsonPropertyName("known_for_department")]
        public string KnownForDepartment { get; set; }

        [JsonPropertyName("popularity")]
        public double Popularity { get; set; }

        public Cast ToCast() => new(Id, Name, Character, CreditId, Gender, Order, ProfilePath, Adult, KnownForDepartment, Popularity);

        public override string ToString()
        {
            return JsonSerializer.Serialize(this, JsonUtil.Indent);
        }
    }
}
