using Newtonsoft.Json;
using UserNotifications;

namespace Insane.Notifications.iOS.Presenter.Handlers
{
    public abstract class iOSRemoteNotificationTapAction<TNotificationData> : iOSRemoteNotificationTapAction
        where TNotificationData : class
    {
        public sealed override void OnNotificationTapped(string notificationDataJson)
        {
            ExecuteOnNotificationTapped(JsonConvert.DeserializeObject<TNotificationData>(notificationDataJson));
        }

        public abstract void ExecuteOnNotificationTapped(TNotificationData notificationData);

        public sealed override void OnNotificationDismissed(string notificationDataJson, UNNotificationResponse notificationResponse)
        {
            base.OnNotificationDismissed(notificationDataJson, notificationResponse);
            var deserializedObject = JsonConvert.DeserializeObject<TNotificationData>(notificationDataJson);
            ExecuteOnNotificationDismissed(deserializedObject, notificationResponse);
        }

        public virtual void ExecuteOnNotificationDismissed(TNotificationData notificationData, UNNotificationResponse notificationResponse)
        {
            
        }

        public sealed override void OnNotificationTappedWithAction(string actionId, string notificationDataJson, UNNotificationResponse notificationResponse)
        {
            base.OnNotificationTappedWithAction(actionId, notificationDataJson, notificationResponse);
            var deserializedObject = JsonConvert.DeserializeObject<TNotificationData>(notificationDataJson);
            ExecuteOnNotificationTappedWithAction(actionId, deserializedObject, notificationResponse);
        }

        public virtual void ExecuteOnNotificationTappedWithAction(string actionId, TNotificationData notificationData, UNNotificationResponse notificationResponse)
        {

        }
    }
}
