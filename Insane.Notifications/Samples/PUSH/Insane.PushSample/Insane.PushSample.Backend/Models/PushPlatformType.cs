using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Insane.PushSample.Backend.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PushPlatformType
    {
        MPNS,
        WNS,
        APNS,
        GCM
    }
}