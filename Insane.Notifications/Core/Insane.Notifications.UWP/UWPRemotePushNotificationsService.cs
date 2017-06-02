using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InsaneNotifications.UWP.Internal;
using InsaneNotifications.UWP.Presenter;
using MvvmCross.Plugins.Notifications;
using MvvmCross.Plugins.Notifications.CachedStorage;
using MvvmCross.Plugins.Notifications.Data;
using MvvmCross.Plugins.Notifications.PushNotifications;

namespace InsaneNotifications.UWP
{
    public class UWPRemotePushNotificationsService : INotificationsService
    {
        readonly INotificationsService uwpNotificationsService;
        public UWPRemotePushNotificationsService(IPersistedStorage persistedStorage, IRemotePushRegistrationService remotePushRegistrationService, IPushTagsProvider pushTagsProvider, IUniversalWindowsRemoteNotificationsPresenter remoteNotificationsPresenter)
        {
            uwpNotificationsService =
                new PushInvalidateRegistrationAppUpdateBackgroundTaskNotificationsServiceDecorator(
                    new PushBackgroundTaskNotificationServiceDecorator(
                        new UniversalWindowsRemotePushNotificationService(persistedStorage,
                            remotePushRegistrationService, pushTagsProvider, remoteNotificationsPresenter)
                    ));
        }

        public Task<ServiceResponse> SubscribeToNotifications(bool forceSubscribe = false)
            => uwpNotificationsService.SubscribeToNotifications(forceSubscribe);

        public Task<ServiceResponse> UnsubscribeFromNotifications()
            => uwpNotificationsService.UnsubscribeFromNotifications();
    }
}
