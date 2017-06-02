using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InsaneNotifications.UWP.Handlers;

namespace InsaneNotifications.UWP.Presenter
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
