using Insane.Notifications.UWP.Handlers.Badge;
using Insane.Notifications.UWP.Handlers.Tile;
using Insane.Notifications.UWP.Handlers.Toast;

namespace Insane.Notifications.UWP.Presenter
{
    public class UniversalWindowsPresenterConfiguration
    {
        public IBadgeRemoteNotificationHandlerIdProvider BadgeRemoteNotificationHandlerIdProvider { get; }
        public IToastRemoteNotificationHandlerIdProvider ToastRemoteNotificationHandlerIdProvider { get; }
        public ITileRemoteNotificationHandlerIdProvider TileRemoteNotificationHandlerIdProvider { get; }

        public UniversalWindowsPresenterConfiguration(IBadgeRemoteNotificationHandlerIdProvider badgeRemoteNotificationHandlerIdProvider, IToastRemoteNotificationHandlerIdProvider toastRemoteNotificationHandlerIdProvider,
                                                     ITileRemoteNotificationHandlerIdProvider tileRemoteNotificationHandlerIdProvider)
        {
            BadgeRemoteNotificationHandlerIdProvider = badgeRemoteNotificationHandlerIdProvider;
            ToastRemoteNotificationHandlerIdProvider = toastRemoteNotificationHandlerIdProvider;
            TileRemoteNotificationHandlerIdProvider = tileRemoteNotificationHandlerIdProvider;
        }
    }
}
