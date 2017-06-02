using Newtonsoft.Json;

namespace Insane.Notifications.Data
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