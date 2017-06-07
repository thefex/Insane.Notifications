using System;
using Insane.Notifications.iOS.Data;
using Insane.Notifications.Presenter;
using Insane.Notifications.PushSample.Portable.Data.Push;

namespace Insane.Notifications.PushSample.iOS.Services
{
    public class AppNotificationsIdProvider : RemoteNotificationIdProvider<APNSPushData<PushData>>
    {
        public AppNotificationsIdProvider()
        {
        }

        public override string GetNotificationId(APNSPushData<PushData> data)
        {
            return data?.Data?.Type ?? string.Empty;
        }
    }
}
