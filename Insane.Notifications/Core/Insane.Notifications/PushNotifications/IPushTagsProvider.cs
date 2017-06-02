using System.Collections.Generic;

namespace Insane.Notifications.PushNotifications
{
    public interface IPushTagsProvider
    {
        IEnumerable<string> ActivePushTags { get; }
    }
}