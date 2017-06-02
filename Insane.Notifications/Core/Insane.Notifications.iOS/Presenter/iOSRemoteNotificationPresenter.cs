using System;
using System.Collections.Generic;
using System.Reflection;
using MvvmCross.Plugins.Notifications.IOS.Presenter.Handlers;
using MvvmCross.Plugins.Notifications.IOS.Presenter.Handlers.Alert;
using MvvmCross.Plugins.Notifications.IOS.Presenter.Handlers.Badge;
using MvvmCross.Plugins.Notifications.Presenter;
using UserNotifications;
using Foundation;
using MvvmCross.Plugins.Notifications.IOS.Extensions;
using MvvmCross.Plugins.Notifications.IOS.Internal;
using System.Linq;
using MvvmCross.Plugins.Notifications.IOS.Presenter.Handlers.Sound;

namespace MvvmCross.Plugins.Notifications.IOS.Presenter
{
    public class iOSRemoteNotificationPresenter : RemoteNotificationsPresenter, IIOSRemoteNotificationsPresenter
    {
        readonly Dictionary<string, IIOSSoundNotificationHandler> _soundNotificationHandlersMap = new Dictionary<string, IIOSSoundNotificationHandler>();
        readonly Dictionary<string, IIOSBadgeNotificationHandler> _badgeNotificationHandlerMap = new Dictionary<string, IIOSBadgeNotificationHandler>();
        readonly Dictionary<string, IIOSAlertNotificationHandler> _alertNotificationHandlerMap = new Dictionary<string, IIOSAlertNotificationHandler>();
        readonly NotificationTappedHandler _notificationTappedHandler;
        readonly Assembly[] assembliesToLookInForNotificationHandlers;

        public iOSRemoteNotificationPresenter(IRemoteNotificationIdProvider remoteNotificationIdProvider, params Assembly[] assembliesToLookInForNotificationHandlers) : base(remoteNotificationIdProvider, assembliesToLookInForNotificationHandlers)
        {
            this.assembliesToLookInForNotificationHandlers = assembliesToLookInForNotificationHandlers;
            _notificationTappedHandler = new NotificationTappedHandler(remoteNotificationIdProvider, assembliesToLookInForNotificationHandlers);
        }

        protected override void Initialize()
        {
            base.Initialize();
            _notificationTappedHandler.InitializeIfNeeded();

            InitializeSoundNotificationHandlerMap();
            InitializeAlertNotificationHandlerMap();
            InitializeBadgeNotificationHandlerMap();
        }

        private void InitializeSoundNotificationHandlerMap()
        {
            var notificationHandlersData =
                assembliesToLookInForNotificationHandlers
                    .SelectMany(x => x.DefinedTypes)
                    .Where(x => typeof(IIOSSoundNotificationHandler).GetTypeInfo().IsAssignableFrom(x))
                    .Where(x => x.CustomAttributes.Any(
                        y => y.AttributeType == typeof(RemoteNotificationHandlerAttribute)))
                    .Where(x => !x.IsAbstract && x.IsClass)
                    .Select(x => new
                    {
                        NotificationId = x.GetCustomAttribute<RemoteNotificationHandlerAttribute>().NotificationId,
                        Handler = (IIOSSoundNotificationHandler)Activator.CreateInstance(x.AsType())
                    });

            foreach (var item in notificationHandlersData)
            {
                if (_soundNotificationHandlersMap.ContainsKey(item.NotificationId))
                    throw new InvalidOperationException($"You have defined multiple {nameof(iOSRemoteNotificationTapAction)} with same notification id of: ${item.NotificationId}");

                _soundNotificationHandlersMap.Add(item.NotificationId, item.Handler);
            }
        }

        private void InitializeBadgeNotificationHandlerMap()
        {
            var notificationHandlersData =
                assembliesToLookInForNotificationHandlers
                    .SelectMany(x => x.DefinedTypes)
                    .Where(x => typeof(IIOSBadgeNotificationHandler).GetTypeInfo().IsAssignableFrom(x))
                    .Where(x => x.CustomAttributes.Any(
                        y => y.AttributeType == typeof(RemoteNotificationHandlerAttribute)))
                    .Where(x => !x.IsAbstract && x.IsClass)
                    .Select(x => new
                    {
                        NotificationId = x.GetCustomAttribute<RemoteNotificationHandlerAttribute>().NotificationId,
                        Handler = (IIOSBadgeNotificationHandler)Activator.CreateInstance(x.AsType())
                    }).ToList();

            foreach (var item in notificationHandlersData)
            {
                if (_badgeNotificationHandlerMap.ContainsKey(item.NotificationId))
                    throw new InvalidOperationException($"You have defined multiple {nameof(iOSRemoteNotificationTapAction)} with same notification id of: ${item.NotificationId}");

                _badgeNotificationHandlerMap.Add(item.NotificationId, item.Handler);
            }
        }

        private void InitializeAlertNotificationHandlerMap()
        {
            var notificationHandlersData =
                assembliesToLookInForNotificationHandlers
                    .SelectMany(x => x.DefinedTypes)
                        .Where(x => typeof(IIOSAlertNotificationHandler).GetTypeInfo().IsAssignableFrom(x))
                        .Where(x => x.CustomAttributes.Any(
                            y => y.AttributeType == typeof(RemoteNotificationHandlerAttribute)))
                        .Where(x => !x.IsAbstract && x.IsClass)
                        .Select(x => new
                        {
                            NotificationId = x.GetCustomAttribute<RemoteNotificationHandlerAttribute>().NotificationId,
                            Handler = (IIOSAlertNotificationHandler)Activator.CreateInstance(x.AsType())
                        });

            foreach (var item in notificationHandlersData)
            {
                if (_alertNotificationHandlerMap.ContainsKey(item.NotificationId))
                    throw new InvalidOperationException($"You have defined multiple {nameof(iOSRemoteNotificationTapAction)} with same notification id of: ${item.NotificationId}");

                _alertNotificationHandlerMap.Add(item.NotificationId, item.Handler);
            }
        }

        public UNNotificationPresentationOptions HandleNotification(UNUserNotificationCenter center, UNNotification notification)
        {
            InitializeIfNeeded();

            var request = notification.Request;
            var notificationContent = request.Content;
            var notificationDataDictionary = notificationContent.UserInfo;

            var notificationJson = notificationDataDictionary.GetJsonFromDictionary();

            var notificationId = RemoteNotificationIdProvider.GetNotificationId(notificationJson);

            bool hasNotificationBeenHandled = false;

            if (_soundNotificationHandlersMap.ContainsKey(notificationId))
            {
                var soundNotificationHandler = _soundNotificationHandlersMap[notificationId];
                hasNotificationBeenHandled = soundNotificationHandler.HandleSoundNotification(notificationContent.Sound, notification);

                if (hasNotificationBeenHandled)
                    return UNNotificationPresentationOptions.Sound;
            }
            else if (_badgeNotificationHandlerMap.ContainsKey(notificationId))
            {
                var badgeNotificationHandler = _badgeNotificationHandlerMap[notificationId];
                hasNotificationBeenHandled = badgeNotificationHandler.HandleBadgeNotification((int)notificationContent.Badge, notification);

                if (hasNotificationBeenHandled)
                    return UNNotificationPresentationOptions.Badge;
            }
            else if (_alertNotificationHandlerMap.ContainsKey(notificationId))
            {
                var alertNotificationHandler = _alertNotificationHandlerMap[notificationId];
                hasNotificationBeenHandled = alertNotificationHandler.HandleAlertNotification(notificationContent, notification);

                if (hasNotificationBeenHandled)
                    return UNNotificationPresentationOptions.Alert;
            }
            else
                HandleNotification(notificationJson);

            return UNNotificationPresentationOptions.None;
        }

        public void HandleNotificationTapped(UNNotificationResponse notificationResponse)
        {
            InitializeIfNeeded();


            _notificationTappedHandler.HandleNotificationTapped(notificationResponse);
        }
    }
}
