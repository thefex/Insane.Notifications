using UserNotifications;
using Newtonsoft.Json;

namespace MvvmCross.Plugins.Notifications.IOS.Presenter.Handlers
{
    public abstract class iOSRemoteNotificationTapAction<TNotificationData> : iOSRemoteNotificationTapAction
        where TNotificationData : class
    {
        public sealed override void OnNotificationTapped(string notificationDataJson)
        {

        }

        public abstract void OnNotificationTapped(TNotificationData notificationData);

        public sealed override void OnNotificationDismissed(string notificationDataJson, UNNotificationResponse notificationResponse)
        {
            base.OnNotificationDismissed(notificationDataJson, notificationResponse);
            var deserializedObject = JsonConvert.DeserializeObject<TNotificationData>(notificationDataJson);
            OnNotificationDismissed(deserializedObject, notificationResponse);
        }

        public virtual void OnNotificationDismissed(TNotificationData notificationData, UNNotificationResponse notificationResponse)
        {

        }

        public sealed override void OnNotificationTappedWithAction(string actionId, string notificationDataJson, UNNotificationResponse notificationResponse)
        {
            base.OnNotificationTappedWithAction(actionId, notificationDataJson, notificationResponse);
            var deserializedObject = JsonConvert.DeserializeObject<TNotificationData>(notificationDataJson);
            OnNotificationTappedWithAction(actionId, deserializedObject, notificationResponse);
        }

        public virtual void OnNotificationTappedWithAction(string actionId, TNotificationData notificationData, UNNotificationResponse notificationResponse)
        {

        }
    }
}
