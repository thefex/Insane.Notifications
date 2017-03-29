using MvvmCross.Plugins.Notifications.IOS.NotificationsPresenter;
using Newtonsoft.Json;

namespace MvvmCross.Plugins.Notifications.IOS.Presenter.Handlers
{
    public abstract class RemoteNotificationHandler<TNotification> : IRemoteNotificationHandler
    {
        public abstract bool Handle(TNotification notification, string notificationId);

        public bool Handle(string notificationJson, string notificationId)
        {
            var deserializedNotification = JsonConvert.DeserializeObject<TNotification>(notificationJson);
            return Handle(deserializedNotification, notificationId);
        }
    }
}