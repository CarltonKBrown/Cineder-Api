using System.Text.Json.Serialization;

namespace Cineder_Api.Infrastructure.Models
{
    internal abstract class BaseContract
    {
        protected BaseContract(long id)
        {
            Id = id;
        }

        protected BaseContract() : this(0) { }

        [JsonPropertyName("id")]
        internal long Id { get; set; }
    }
}
