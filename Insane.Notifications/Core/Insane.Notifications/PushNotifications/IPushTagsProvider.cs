using System.Collections.Generic;

namespace MvvmCross.Plugins.Notifications.PushNotifications
{
    public interface IPushTagsProvider
    {
        IEnumerable<string> ActivePushTags { get; }
    }
}