using System.Threading.Tasks;
using Foundation;
using MvvmCross.Plugins.Notifications.CachedStorage;
using MvvmCross.Plugins.Notifications.Data;
using MvvmCross.Plugins.Notifications.PushNotifications;
using UIKit;
using UserNotifications;

namespace MvvmCross.Plugins.Notifications.IOS
{
    public class MvxRemotePushNotificationServiceIos : RemotePushNotificationService
    {
        public MvxRemotePushNotificationServiceIos(IPersistedStorage persistedStorage,
            IRemotePushRegistrationService remotePushRegistrationService, IPushTagsProvider pushTagsProvider)
            : base(persistedStorage, remotePushRegistrationService, pushTagsProvider)
        {
        }

        protected override PushPlatformType PlatformType => PushPlatformType.iOS;

        protected override bool IsUserRegisteredToPushService
            => UIApplication.SharedApplication.IsRegisteredForRemoteNotifications;

        protected override async Task<ServiceResponse> LaunchRegistrationProcess(bool forceSubscribe = false)
        {
            var permissionRequestResponse = await MvxIoSNotificationsSetup.RequestPermissions();

            if (!permissionRequestResponse.IsSuccess)
                return permissionRequestResponse;


            if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
            {
                var pushSettings = UIUserNotificationSettings.GetSettingsForTypes(
                    UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound,
                    new NSSet());

                UIApplication.SharedApplication.RegisterUserNotificationSettings(pushSettings);
                UIApplication.SharedApplication.RegisterForRemoteNotifications();
            }
            else
            {
                var notificationTypes = UIRemoteNotificationType.Alert |
                                        UIRemoteNotificationType.Badge |
                                        UIRemoteNotificationType.Sound;
                UIApplication.SharedApplication.RegisterForRemoteNotificationTypes(notificationTypes);
            }

            return new ServiceResponse();
        }

        protected override Task<ServiceResponse> LaunchUnregistrationProcess()
        {
            UIApplication.SharedApplication.UnregisterForRemoteNotifications();
            NotifyThatUnregistrationSucceed();
            return Task.FromResult(new ServiceResponse());
        }

        protected override string ParseRegistrationId(string registrationId)
        {
            return registrationId.Replace(" ", string.Empty).Replace("<", string.Empty).Replace(">", string.Empty);
        }
    }
}