using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Foundation;
using MvvmCross.Platform;
using MvvmCross.Plugins.Notifications.IOS.Data;
using MvvmCross.Plugins.Notifications.IOS.Extensions;
using MvvmCross.Plugins.Notifications.IOS.Presenter.Handlers;
using UserNotifications;

namespace MvvmCross.Plugins.Notifications.IOS.NotificationsPresenter
{
	public class RemoteNotificationsPresenter
	{
	    private readonly IRemoteNotificationIdProvider _remoteNotificationIdProvider;
	    private readonly Assembly[] _assembliesToLookInForNotificationHandlers;
	    private readonly Dictionary<string, IRemoteNotificationHandler> _remoteNotificationHandlers;
	    private bool isInitialized;


	    public RemoteNotificationsPresenter(IRemoteNotificationIdProvider remoteNotificationIdProvider, params Assembly[] assembliesToLookInForNotificationHandlers)
	    {
	        _remoteNotificationIdProvider = remoteNotificationIdProvider;
	        _assembliesToLookInForNotificationHandlers = assembliesToLookInForNotificationHandlers;
	    }

	    private void InitializeIfNeeded()
	    {
	        lock (this)
	        {
	            if (isInitialized)
	                return;
	            isInitialized = true;
	        }

	        var notificationHandlersData =
	                _assembliesToLookInForNotificationHandlers
	                    .SelectMany(x => x.DefinedTypes)
	                    .Where(x => typeof(IRemoteNotificationHandler).IsAssignableFrom(x.GetTypeInfo()))
	                    .Where(x => x.CustomAttributes.Any(
	                        y => y.AttributeType == typeof(RemoteNotificationHandlerAttribute)))
	                    .Where(x => !x.IsAbstract && x.IsClass)
	                    .Select(x => new
	                    {
	                        NotificationId = x.GetCustomAttribute<RemoteNotificationHandlerAttribute>().NotificationId,
	                        Handler = (IRemoteNotificationHandler) Activator.CreateInstance(x)
	                    });

            foreach (var item in notificationHandlersData)
            {
                if (_remoteNotificationHandlers.ContainsKey(item.NotificationId))
                    throw new InvalidOperationException($"You have defined multiple {nameof(IRemoteNotificationHandler)} with same notification id of: ${item.NotificationId}");

                _remoteNotificationHandlers.Add(item.NotificationId, item.Handler);
            }
	    }

	    public void HandleNotification(NSDictionary notificationInfo)
	    {
	        InitializeIfNeeded();

	        var notificationJson = notificationInfo.GetJsonFromDictionary();
	        string notificationId = _remoteNotificationIdProvider.GetNotificationId(notificationJson);
	        bool isNotificationHandled = false;

	        if (_remoteNotificationHandlers.ContainsKey(notificationId))
	            isNotificationHandled = _remoteNotificationHandlers[notificationId].Handle(notificationJson, notificationId);

	        if (!isNotificationHandled)
                OnNotificationPublished(new RemoteNotificationData(notificationId, notificationJson));
	    }

	    /// <summary>
	    /// When remote notificaiton is incoming then event will be raised with notification json.
	    /// </summary>
	    public event Action<RemoteNotificationData> NotificationPublished;

	    protected virtual void OnNotificationPublished(RemoteNotificationData obj)
	    {
	        NotificationPublished?.Invoke(obj);
	    }
	}
}
