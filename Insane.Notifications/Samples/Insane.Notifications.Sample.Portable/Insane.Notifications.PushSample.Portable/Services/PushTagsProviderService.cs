using System;
using System.Collections.Generic;
using Insane.Notifications.CachedStorage;
using Insane.Notifications.PushNotifications;

namespace Insane.Notifications.PushSample.Portable.Services
{
    public class PushTagsProviderService : IPushTagsProvider
    {
        public PushTagsProviderService()
        {
        }

        public IEnumerable<string> ActivePushTags => new List<string>();
    }
 
}
