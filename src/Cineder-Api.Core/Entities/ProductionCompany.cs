using System.Text.Json;

namespace Cineder_Api.Core.Entities
{
    public class ProductionCompany:Entity
    {
        public ProductionCompany(long id, string name, string logoPath, string originCountry):base(id, name)
        {
            LogoPath = logoPath;
            OriginCountry = originCountry;
        }

        public ProductionCompany():this(0, string.Empty, string.Empty, string.Empty)
        {
            
        }

        public string LogoPath { get; protected set; }
        public string OriginCountry { get; protected set; }

        public override string ToString()
        {
            var opt = new JsonSerializerOptions() { WriteIndented = true };
            return JsonSerializer.Serialize(this, opt);
        }
    }
}
