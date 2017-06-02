using Windows.UI.Notifications;

namespace InsaneNotifications.UWP.Handlers.Tile
{
    public interface ITileRemoteNotificationHandlerIdProvider
    {
        string GetTileNotificationHandlerId(TileNotification tileNotification);
    }
}
