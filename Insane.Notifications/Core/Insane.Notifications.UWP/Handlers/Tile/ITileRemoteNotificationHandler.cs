using Windows.UI.Notifications;

namespace InsaneNotifications.UWP.Handlers
{
    public interface ITileRemoteNotificationHandler
    {
        void HandleTileNotification(TileNotification tileNotification);
    }
}
