using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cineder_Api.Infrastructure.Models
{
    internal class CrewMContract : BaseContract
    {
        internal CrewMContract(long id, string creditId, string department, string name, int gender, string job, string profilePath) : base(id)
        {
            CreditId = creditId;
            Department = department;
            Name = name;
            Gender = gender;
            Job = job;
            ProfilePath = profilePath;
        }

        internal CrewMContract() : this(0, string.Empty, string.Empty, string.Empty, 0, string.Empty, string.Empty) { }

        [JsonPropertyName("credit_id")]
        public string CreditId { get; set; }

        [JsonPropertyName("department")]
        public string Department { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("gender")]
        public int Gender { get; set; }

        [JsonPropertyName("job")]
        public string Job { get; set; }

        [JsonPropertyName("profile_path")]
        public string ProfilePath { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this, new JsonSerializerOptions() { WriteIndented = true });
        }
    }
}
