using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Cineder_Api.Infrastructure.Models
{
    internal class AppendVideosContract
    {
        public AppendVideosContract(IEnumerable<VideoContract> results)
        {
            Results = results;
        }

        public AppendVideosContract() : this(Enumerable.Empty<VideoContract>())
        {
            
        }

        [JsonPropertyName("results")]
        public IEnumerable<VideoContract> Results { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this, new JsonSerializerOptions() { WriteIndented = true });
        }
    }
}
