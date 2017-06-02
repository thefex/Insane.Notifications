using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.PushNotifications;
using Windows.UI.Notifications;
using InsaneNotifications.UWP.Handlers;
using MvvmCross.Plugins.Notifications.Data;
using MvvmCross.Plugins.Notifications.Presenter;
using MvvmCross.Plugins.Notifications.Presenter.Handlers;

namespace InsaneNotifications.UWP.Presenter
{
    public class UniversalWindowsRemoteNotificationsPresenter : RemoteNotificationsPresenter, IUniversalWindowsRemoteNotificationsPresenter
    {
        private readonly UniversalWindowsPresenterConfiguration _universalWindowsPresenterConfiguration;
        private readonly Assembly[] _assembliesToLookInForNotificationHandlers;
        private readonly Dictionary<string, ToastRemoteNotificationHandler> _toastRemoteNotificationHandlers = new Dictionary<string, ToastRemoteNotificationHandler>();
        private readonly Dictionary<string, ITileRemoteNotificationHandler> _tileRemoteNotificationHandlers = new Dictionary<string, ITileRemoteNotificationHandler>();
        private readonly Dictionary<string, IBadgeRemoteNotificationHandler> _badgeRemoteNotificationHandlers = new Dictionary<string, IBadgeRemoteNotificationHandler>();

        public UniversalWindowsRemoteNotificationsPresenter(IRemoteNotificationIdProvider remoteNotificationIdProvider, UniversalWindowsPresenterConfiguration universalWindowsPresenterConfiguration, params Assembly[] assembliesToLookInForNotificationHandlers) : base(remoteNotificationIdProvider, assembliesToLookInForNotificationHandlers)
        {
            _universalWindowsPresenterConfiguration = universalWindowsPresenterConfiguration;
            _assembliesToLookInForNotificationHandlers = assembliesToLookInForNotificationHandlers;
        }

        protected override void Initialize()
        {
            base.Initialize();

            InitializeToastRemoteNotificationsHandlers();
            InitializeTileRemoteNotificationsHandlers();
            InitializeBadgeRemoteNotificationsHandlers();
        }

        private void InitializeToastRemoteNotificationsHandlers()
        {
            var notificationHandlersData =
                _assembliesToLookInForNotificationHandlers
                    .SelectMany(x => x.DefinedTypes)
                    .Where(x => typeof(ToastRemoteNotificationHandler).GetTypeInfo().IsAssignableFrom(x))
                    .Where(x => x.CustomAttributes.Any(
                        y => y.AttributeType == typeof(ToastRemoteNotificationHandlerAttribute)))
                    .Where(x => !x.IsAbstract && x.IsClass)
                    .Select(x => new
                    {
                        HandlerId = x.GetCustomAttribute<ToastRemoteNotificationHandlerAttribute>().HandlerId,
                        Handler = (ToastRemoteNotificationHandler) Activator.CreateInstance(x.AsType())
                    });

            foreach (var item in notificationHandlersData)
            {
                if (_toastRemoteNotificationHandlers.ContainsKey(item.HandlerId))
                    throw new InvalidOperationException(
                        $"You have defined multiple {nameof(ToastRemoteNotificationHandler)} with same notification id of: ${item.HandlerId}");

                _toastRemoteNotificationHandlers.Add(item.HandlerId, item.Handler);
            }
        }

        private void InitializeTileRemoteNotificationsHandlers()
        {
            var notificationHandlersData =
                _assembliesToLookInForNotificationHandlers
                    .SelectMany(x => x.DefinedTypes)
                    .Where(x => typeof(ITileRemoteNotificationHandler).GetTypeInfo()
                        .IsAssignableFrom(x))
                    .Where(x => x.CustomAttributes.Any(
                        y => y.AttributeType == typeof(TileRemoteNotificationHandlerAttribute)))
                    .Where(x => !x.IsAbstract && x.IsClass)
                    .Select(x => new
                    {
                        HandlerId = x.GetCustomAttribute<TileRemoteNotificationHandlerAttribute>().HandlerId,
                        Handler = (ITileRemoteNotificationHandler)Activator.CreateInstance(x.AsType())
                    });

            foreach (var item in notificationHandlersData)
            {
                if (_tileRemoteNotificationHandlers.ContainsKey(item.HandlerId))
                    throw new InvalidOperationException(
                        $"You have defined multiple {nameof(ITileRemoteNotificationHandler)} with same notification id of: ${item.HandlerId}");

                _tileRemoteNotificationHandlers.Add(item.HandlerId, item.Handler);
            }
        }

        private void InitializeBadgeRemoteNotificationsHandlers()
        {
            var notificationHandlersData =
                _assembliesToLookInForNotificationHandlers
                    .SelectMany(x => x.DefinedTypes)
                    .Where(x => typeof(IBadgeRemoteNotificationHandler).GetTypeInfo()
                        .IsAssignableFrom(x))
                    .Where(x => x.CustomAttributes.Any(
                        y => y.AttributeType == typeof(BadgeRemoteNotificationHandlerAttribute)))
                    .Where(x => !x.IsAbstract && x.IsClass)
                    .Select(x => new
                    {
                        HandlerId = x.GetCustomAttribute<BadgeRemoteNotificationHandlerAttribute>().HandlerId,
                        Handler = (IBadgeRemoteNotificationHandler)Activator.CreateInstance(x.AsType())
                    });

            foreach (var item in notificationHandlersData)
            {
                if (_badgeRemoteNotificationHandlers.ContainsKey(item.HandlerId))
                    throw new InvalidOperationException(
                        $"You have defined multiple {nameof(IBadgeRemoteNotificationHandler)} with same notification id of: ${item.HandlerId}");

                _badgeRemoteNotificationHandlers.Add(item.HandlerId, item.Handler);
            }
        }

        public void HandlePlatformNotification(PushNotificationReceivedEventArgs notificationArgs)
        {
            InitializeIfNeeded();

            bool isNotificationHandled = false;
            switch (notificationArgs.NotificationType)
            {
                    case PushNotificationType.Badge:
                        isNotificationHandled = HandleBadgeNotification(notificationArgs.BadgeNotification);
                        if (!isNotificationHandled)
                            OnBadgeNotificationPublished(notificationArgs.BadgeNotification);
                        break;
                    case PushNotificationType.Tile:
                        isNotificationHandled = HandleTileNotification(notificationArgs.TileNotification);
                        if (!isNotificationHandled)
                            OnTileNotificationPublished(notificationArgs.TileNotification);
                        break;
                    case PushNotificationType.Toast:
                        isNotificationHandled = HandleToastNotification(notificationArgs.ToastNotification);
                        if(!isNotificationHandled)
                            OnToastNotificationPublished(notificationArgs.ToastNotification);
                        break;
            }

            if (isNotificationHandled)
                notificationArgs.Cancel = true;
        }

        public void HandleRemoteNotificationActivation(string launchArgument)
        {
            InitializeIfNeeded();

            if (!LaunchArgumentSerializator.IsPushServicesLaunchArgumentFormat(launchArgument))
                return;

            var launchPushId = LaunchArgumentSerializator.GetLaunchId(launchArgument);

            if (RemoteNotificationHandlers.ContainsKey(launchPushId))
            {
                var remoteNotificationHandler = RemoteNotificationHandlers[launchPushId];
                var remoteNotifcationTapAction = remoteNotificationHandler as IRemoteNotificationTapAction;

                if (remoteNotifcationTapAction != null)
                {
                    var notificationData = LaunchArgumentSerializator.GetLaunchArgumentJsonString(launchArgument);
                    remoteNotifcationTapAction?.OnNotificationTapped(notificationData);
                }
            }

        }

        public event Action<ToastNotification> ToastNotificationPublished;
        public event Action<BadgeNotification> BadgeNotificationPublished;
        public event Action<TileNotification> TileNotificationPublished;

        private bool HandleBadgeNotification(BadgeNotification badgeNotification)
        {
            string badgeNotificationHandlerId = _universalWindowsPresenterConfiguration.BadgeRemoteNotificationHandlerIdProvider.GetBadgeNotificationHandlerId(badgeNotification);

            if (_badgeRemoteNotificationHandlers.ContainsKey(badgeNotificationHandlerId))
            {
                _badgeRemoteNotificationHandlers[badgeNotificationHandlerId].HandleBadgeNotification(badgeNotification);
                return true;
            }

            return false;
        }

        private bool HandleTileNotification(TileNotification tileNotification)
        {
            string tileNotificationHandlerId = _universalWindowsPresenterConfiguration
                .TileRemoteNotificationHandlerIdProvider.GetTileNotificationHandlerId(tileNotification);

            if (_tileRemoteNotificationHandlers.ContainsKey(tileNotificationHandlerId))
            {
                _tileRemoteNotificationHandlers[tileNotificationHandlerId].HandleTileNotification(tileNotification);
                return true;
            }

            return false;
        }

        private bool HandleToastNotification(ToastNotification toastNotification)
        {
            string toastNotificationHandlerId = _universalWindowsPresenterConfiguration
                .ToastRemoteNotificationHandlerIdProvider.GetToastNotificationHandlerId(toastNotification);

            if (_toastRemoteNotificationHandlers.ContainsKey(toastNotificationHandlerId))
            {
                var toastRemoteNotificationHandler = _toastRemoteNotificationHandlers[toastNotificationHandlerId];
                return toastRemoteNotificationHandler.HandleToastNotification(toastNotification);
            }

            return false;
        }

        protected virtual void OnToastNotificationPublished(ToastNotification obj)
        {
            ToastNotificationPublished?.Invoke(obj);
        }

        protected virtual void OnBadgeNotificationPublished(BadgeNotification obj)
        {
            BadgeNotificationPublished?.Invoke(obj);
        }

        protected virtual void OnTileNotificationPublished(TileNotification obj)
        {
            TileNotificationPublished?.Invoke(obj);
        }
    }
}
