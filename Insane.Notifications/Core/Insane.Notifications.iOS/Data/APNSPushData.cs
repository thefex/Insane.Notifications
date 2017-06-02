using System;
using Newtonsoft.Json;

namespace MvvmCross.Plugins.Notifications.IOS.Data
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
