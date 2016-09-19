using Newtonsoft.Json;

namespace Insane.Notifications.Presenter.Handlers
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