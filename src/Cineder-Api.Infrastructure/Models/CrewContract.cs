using Cineder_Api.Core.Util;
using PreventR;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cineder_Api.Infrastructure.Models
{
    public class CrewMContract : BaseContract
    {
        public CrewMContract(long id, string creditId, string department, string name, int gender, string job, string profilePath) : base(id)
        {
            CreditId = creditId.Prevent(nameof(creditId)).Null();
            Department = department.Prevent(nameof(department)).Null();
            Name = name.Prevent(nameof(name)).Null();
            Gender = gender;
            Job = job.Prevent(nameof(job)).Null();
            ProfilePath = profilePath.Prevent(nameof(profilePath)).Null();
        }

        public CrewMContract() : this(0, string.Empty, string.Empty, string.Empty, 0, string.Empty, string.Empty) { }

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
            return JsonSerializer.Serialize(this, JsonUtil.Indent);
        }
    }
}
