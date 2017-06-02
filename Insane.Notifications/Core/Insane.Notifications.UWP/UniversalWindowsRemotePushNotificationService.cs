using System;
using System.Threading.Tasks;
using Windows.Networking.PushNotifications;
using InsaneNotifications.UWP.Presenter;
using MvvmCross.Plugins.Notifications.CachedStorage;
using MvvmCross.Plugins.Notifications.Data;
using MvvmCross.Plugins.Notifications.Presenter;
using MvvmCross.Plugins.Notifications.PushNotifications;

namespace InsaneNotifications.UWP
{
    internal class UniversalWindowsRemotePushNotificationService : RemotePushNotificationService
    {
        private readonly IUniversalWindowsRemoteNotificationsPresenter _remoteNotificationsPresenter;
        private bool isRegisteredToPush;
        private PushNotificationChannel pushChannel;

        public UniversalWindowsRemotePushNotificationService(IPersistedStorage persistedStorage, IRemotePushRegistrationService remotePushRegistrationService, IPushTagsProvider pushTagsProvider, IUniversalWindowsRemoteNotificationsPresenter remoteNotificationsPresenter) : base(persistedStorage, remotePushRegistrationService, pushTagsProvider)
        {
            _remoteNotificationsPresenter = remoteNotificationsPresenter;
        }


        protected override PushPlatformType PlatformType => PushPlatformType.Windows;
        protected override bool IsUserRegisteredToPushService => isRegisteredToPush;

        protected override async Task<ServiceResponse> LaunchRegistrationProcess(bool forceSubscribe = false)
        {
            if (IsUserRegisteredToPushService && !forceSubscribe)
                return new ServiceResponse().AddErrorMessage("User is already registerd to push.");

            pushChannel = await PushNotificationChannelManager.CreatePushNotificationChannelForApplicationAsync();

            NotifyThatRegistrationSucceed(pushChannel.Uri);
            pushChannel.PushNotificationReceived += PushChannel_PushNotificationReceived;

            return new ServiceResponse();
        }

        private void PushChannel_PushNotificationReceived(PushNotificationChannel sender, PushNotificationReceivedEventArgs args)
        {
            if (args.NotificationType == PushNotificationType.Raw)
            {
                args.Cancel = true;
                _remoteNotificationsPresenter.HandleNotification(args.RawNotification.Content);
            }
            else
            {
                _remoteNotificationsPresenter.HandlePlatformNotification(args);
            }
        }

        protected override Task<ServiceResponse> LaunchUnregistrationProcess()
        {
            if (!IsUserRegisteredToPushService)
                return Task.FromResult(new ServiceResponse().AddErrorMessage("User is not registerd to push"));

            pushChannel.PushNotificationReceived -= PushChannel_PushNotificationReceived;
            pushChannel = null;
            NotifyThatUnregistrationSucceed();
        
            return Task.FromResult(new ServiceResponse());
        }

        protected override string ParseRegistrationId(string registrationId) => registrationId;
    }
}
