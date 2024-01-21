using System.Text.Json;

namespace Cineder_Api.Core.Util
{
    public static class JsonUtil
    {
        public static readonly JsonSerializerOptions Indent = new() { WriteIndented = true };
    }
}
