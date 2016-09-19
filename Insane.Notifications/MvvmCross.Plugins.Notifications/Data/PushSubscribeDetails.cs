using System.Collections.Generic;

namespace MvvmCross.Plugins.Notifications.Data
{
    public class PushSubscribeDetails
    {
        internal PushSubscribeDetails()
        {
        }

        public PushPlatformType PushPlatformType { get; set; }

        public IEnumerable<string> TagsToRegisterIn { get; set; }

        public string PushHandle { get; set; }

        public string DeviceId { get; set; }
    }
}