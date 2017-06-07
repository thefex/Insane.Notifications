using System.Threading.Tasks;
using Insane.Notifications.CachedStorage;
using Insane.Notifications.Data;
using Insane.Notifications.PushNotifications;
using Insane.Notifications.UWP.Internal;
using Insane.Notifications.UWP.Presenter;
using Insane.Notifications.UWP.Storage;

namespace Insane.Notifications.UWP
{
    public class UWPRemotePushNotificationsService : INotificationsService
    {
        readonly INotificationsService uwpNotificationsService;

        public UWPRemotePushNotificationsService(IRemotePushRegistrationService remotePushRegistrationService, IPushTagsProvider pushTagsProvider, IUniversalWindowsRemoteNotificationsPresenter remoteNotificationsPresenter)
            : this(new UWPDefaultPersistedStorage(), remotePushRegistrationService, pushTagsProvider, remoteNotificationsPresenter)
        {
           
        }
        public UWPRemotePushNotificationsService(IPersistedStorage persistedStorage, IRemotePushRegistrationService remotePushRegistrationService, IPushTagsProvider pushTagsProvider, IUniversalWindowsRemoteNotificationsPresenter remoteNotificationsPresenter)
        {
            uwpNotificationsService =
                new PushInvalidateRegistrationAppUpdateBackgroundTaskNotificationsServiceDecorator(
                    new PushBackgroundTaskNotificationServiceDecorator(
                        new UniversalWindowsRemotePushNotificationService(persistedStorage,
                            remotePushRegistrationService, pushTagsProvider)
                    ));
        }

        public Task<ServiceResponse> SubscribeToNotifications(bool forceSubscribe = false)
            => uwpNotificationsService.SubscribeToNotifications(forceSubscribe);

        public Task<ServiceResponse> UnsubscribeFromNotifications()
            => uwpNotificationsService.UnsubscribeFromNotifications();

        public bool IsSubscribedToNotifications => uwpNotificationsService.IsSubscribedToNotifications;
    }
}
