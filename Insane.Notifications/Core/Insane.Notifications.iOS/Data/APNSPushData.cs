using Newtonsoft.Json;

namespace Insane.Notifications.iOS.Data
{
    public class APNSPushData<T>
    {
        public APNSPushData()
        {
        }

        [JsonProperty("aps")]
        public T Data { get; set; }
    }

    
}
