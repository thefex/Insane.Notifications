using Android.App;
using Insane.Notifications.Presenter;
using Newtonsoft.Json;
using Insane.Notifications.PushSample.Portable.Data.Push;


namespace Insane.Notifications.PushSample.Droid.Services
{
    public class GcmPushNotificationIdProvider : IRemoteNotificationIdProvider
    {
        public GcmPushNotificationIdProvider()
        {
        }

        public string GetNotificationId(string notificationJson)
        {
            var pushData = JsonConvert.DeserializeObject<PushData>(notificationJson);
            return pushData?.Type ?? string.Empty;
        }
    }
}
