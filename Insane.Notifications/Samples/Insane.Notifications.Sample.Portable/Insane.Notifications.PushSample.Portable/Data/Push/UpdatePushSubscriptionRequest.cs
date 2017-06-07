using System.Collections.Generic;
using System.Linq;

namespace Insane.Notifications.PushSample.Portable.Data.Push
{
    public class UpdatePushSubscriptionRequest
    {
        public string Id { get; set; }
        public PlatformType? Platform { get; set; }

        public string Handle { get; set; }

        public IEnumerable<string> Tags { get; set; } = Enumerable.Empty<string>();
    }
}
