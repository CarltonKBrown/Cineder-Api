using Cineder_Api.Core.Util;
using PreventR;
using System.Text.Json;

namespace Cineder_Api.Core.Entities
{
    public class ProductionCompany:Entity
    {
        public ProductionCompany(long id, string name, string logoPath, string originCountry):base(id, name)
        {
            LogoPath = logoPath.Prevent(nameof(logoPath)).Null();
            OriginCountry = originCountry.Prevent(nameof(originCountry)).Null();
        }

        public ProductionCompany():this(0, string.Empty, string.Empty, string.Empty)
        {
            
        }

        public string LogoPath { get; protected set; }
        public string OriginCountry { get; protected set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this, JsonUtil.Indent);
        }
    }
}
