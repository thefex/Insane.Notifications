using Windows.UI.Notifications;

namespace InsaneNotifications.UWP.Handlers
{
    public interface ITileRemoteNotificationHandlerIdProvider
    {
        string GetTileNotificationHandlerId(TileNotification tileNotification);
    }
}
