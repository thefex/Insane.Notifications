using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Insane.Notifications.Data;
using Insane.Notifications.Presenter.Handlers;

namespace Insane.Notifications.Presenter
{
    public class RemoteNotificationsPresenter : IRemoteNotificationsPresenter
    {
        protected readonly IRemoteNotificationIdProvider RemoteNotificationIdProvider;
        protected readonly Dictionary<string, IRemoteNotificationHandler> RemoteNotificationHandlers = new Dictionary<string, IRemoteNotificationHandler>();

        private readonly Assembly[] _assembliesToLookInForNotificationHandlers;
        private bool isInitialized;


        public RemoteNotificationsPresenter(IRemoteNotificationIdProvider remoteNotificationIdProvider, params Assembly[] assembliesToLookInForNotificationHandlers)
        {
            RemoteNotificationIdProvider = remoteNotificationIdProvider;
            _assembliesToLookInForNotificationHandlers = assembliesToLookInForNotificationHandlers;
        }

        protected void InitializeIfNeeded()
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
                    .Where(x => typeof(IRemoteNotificationHandler).GetTypeInfo().IsAssignableFrom(x))
                    .Where(x => x.CustomAttributes.Any(
                        y => y.AttributeType == typeof(RemoteNotificationHandlerAttribute)))
                    .Where(x => !x.IsAbstract && x.IsClass)
                    .Select(x => new
                    {
                        NotificationId = x.GetCustomAttribute<RemoteNotificationHandlerAttribute>().NotificationId,
                        Handler = (IRemoteNotificationHandler)Activator.CreateInstance(x.AsType())
                    });

            foreach (var item in notificationHandlersData)
            {
                if (RemoteNotificationHandlers.ContainsKey(item.NotificationId))
                    throw new InvalidOperationException($"You have defined multiple {nameof(IRemoteNotificationHandler)} with same notification id of: ${item.NotificationId}");

                RemoteNotificationHandlers.Add(item.NotificationId, item.Handler);
            }

            Initialize();
        }

        protected virtual void Initialize()
        {

        }

        public void HandleNotification(string notificationJson)
        {
            InitializeIfNeeded();

            string notificationId = RemoteNotificationIdProvider.GetNotificationId(notificationJson);
            bool isNotificationHandled = false;

            if (RemoteNotificationHandlers.ContainsKey(notificationId))
                isNotificationHandled = RemoteNotificationHandlers[notificationId].Handle(notificationJson, notificationId);

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
