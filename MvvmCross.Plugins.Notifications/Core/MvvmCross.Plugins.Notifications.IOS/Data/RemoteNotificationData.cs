using Newtonsoft.Json;

namespace MvvmCross.Plugins.Notifications.IOS.Data
{
    public class RemoteNotificationData
    {
        public RemoteNotificationData(string notificationId, string notificationJson)
        {
            NotificationId = notificationId;
            NotificationJson = notificationJson;
        }

        public string NotificationId { get; }

        public string NotificationJson { get; }

        public TNotificationData GetNotificationData<TNotificationData>()
            => JsonConvert.DeserializeObject<TNotificationData>(NotificationJson);
    }
}