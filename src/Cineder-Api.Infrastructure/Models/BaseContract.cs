using System.Text.Json.Serialization;

namespace Cineder_Api.Infrastructure.Models
{
    public abstract class BaseContract
    {
        protected BaseContract(long id)
        {
            Id = id;
        }

        protected BaseContract() : this(0) { }

        [JsonPropertyName("id")]
        public long Id { get; set; }
    }
}
