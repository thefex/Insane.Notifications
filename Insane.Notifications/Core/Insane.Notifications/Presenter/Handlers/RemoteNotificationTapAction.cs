using Newtonsoft.Json;

namespace InsaneNotifications.UWP.Handlers
{
    public abstract class RemoteNotificationTapAction<TNotificationData> : IRemoteNotificationTapAction
        where TNotificationData : class
    {
        public void OnNotificationTapped(string notificationDataJson)
        {
            var deserializedObject = JsonConvert.DeserializeObject<TNotificationData>(notificationDataJson);
            OnNotificationTapped(deserializedObject);
        }

        /// <summary>
        /// Invoked when the notification is tapped.
        /// </summary>
        /// <param name="notificationData">Notification data.</param>
        public abstract void OnNotificationTapped(TNotificationData notificationData);
    }
}
