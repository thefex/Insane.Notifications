using System;
using MvvmCross.Plugins.Notifications.Presenter;
using UserNotifications;
using MvvmCross.Plugins.Notifications.IOS.Extensions;
using System.Collections.Generic;
using MvvmCross.Plugins.Notifications.IOS.Presenter.Handlers;
using System.Reflection;
using System.Linq;

namespace MvvmCross.Plugins.Notifications.IOS.Internal
{
    internal class NotificationTappedHandler
    {
        readonly IRemoteNotificationIdProvider remoteNotificationsIdProvider;
        readonly Dictionary<string, iOSRemoteNotificationTapAction> remoteNotificationTapActionMap;
        internal const string DefaultActionIdentifier = "DefaultActionIdentifier";
        internal const string DismissActionIdentifier = "DismissActionIdentifier";
        readonly Assembly[] assembliesToLookInForHandlers;

        public NotificationTappedHandler(IRemoteNotificationIdProvider remoteNotificationsIdProvider, params Assembly[] assembliesToLookInForHandlers)
        {
            this.assembliesToLookInForHandlers = assembliesToLookInForHandlers;
            this.remoteNotificationsIdProvider = remoteNotificationsIdProvider;
            remoteNotificationTapActionMap = new Dictionary<string, iOSRemoteNotificationTapAction>();
        }

        bool isInitialized;
        public void InitializeIfNeeded(){
            lock (this)
            {
                if (isInitialized)
                    return;
                isInitialized = true;
            }

			var notificationHandlersData =
				assembliesToLookInForHandlers
					.SelectMany(x => x.DefinedTypes)
					.Where(x => typeof(iOSRemoteNotificationTapAction).GetTypeInfo().IsAssignableFrom(x))
					.Where(x => x.CustomAttributes.Any(
						y => y.AttributeType == typeof(RemoteNotificationHandlerAttribute)))
					.Where(x => !x.IsAbstract && x.IsClass)
					.Select(x => new
					{
						NotificationId = x.GetCustomAttribute<RemoteNotificationHandlerAttribute>().NotificationId,
						Handler = (iOSRemoteNotificationTapAction)Activator.CreateInstance(x.AsType())
					});

			foreach (var item in notificationHandlersData)
			{
				if (remoteNotificationTapActionMap.ContainsKey(item.NotificationId))
					throw new InvalidOperationException($"You have defined multiple {nameof(iOSRemoteNotificationTapAction)} with same notification id of: ${item.NotificationId}");

				remoteNotificationTapActionMap.Add(item.NotificationId, item.Handler);
			}
		}

        public void HandleNotificationTapped(UNNotificationResponse notificationResponse)
        {
            InitializeIfNeeded();

            var notification = notificationResponse.Notification;
            var notificationRequest = notification.Request;
            var notificationContent = notificationRequest.Content;

            var notificationDataDictionary = notificationContent.UserInfo;
            var notificationJson = notificationDataDictionary.GetJsonFromDictionary();

            var notificationId = remoteNotificationsIdProvider.GetNotificationId(notificationJson);
  
            if (remoteNotificationTapActionMap.ContainsKey(notificationId))
            {
                var remoteNotificationTapAction = remoteNotificationTapActionMap[notificationId];
                string actionId = notificationResponse.ActionIdentifier;

                if (notificationResponse.IsDefaultAction)
                    actionId = DefaultActionIdentifier;
                else if (notificationResponse.IsDismissAction)
                    actionId = DismissActionIdentifier;

                remoteNotificationTapAction.OnNotificationTapped(actionId, notificationJson, notificationResponse);
            }
        }
    }
}
