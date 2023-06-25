using Cineder_Api.Core.Entities;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cineder_Api.Infrastructure.Models
{
    internal class CastContract : BaseContract
    {
        internal CastContract(long id, long castId, string character, string creditId, int gender, string name, int order, string profilePath) : base(id)
        {
            CastId = castId;
            Character = character;
            CreditId = creditId;
            Gender = gender;
            Name = name;
            Order = order;
            ProfilePath = profilePath;
        }

        internal CastContract() : this(0, 0, string.Empty, string.Empty, 0, string.Empty, 0, string.Empty) { }

        [JsonPropertyName("cast_id")]
        public long CastId { get; set; }

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

        public Cast ToCast() => new(Id, Name, CastId, Character, CreditId, Gender, Order, ProfilePath);

        public override string ToString()
        {
            return JsonSerializer.Serialize(this, new JsonSerializerOptions() { WriteIndented = true });
        }
    }
}
