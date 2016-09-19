using Foundation;
using MvvmCross.Plugins.Notifications.CachedStorage;
using MvvmCross.Plugins.Notifications.Data;
using MvvmCross.Plugins.Notifications.PushNotifications;
using UIKit;

namespace MvvmCross.Plugins.Notifications.iOS
{
    public class MvxBackendDrivenPushNotificationServiceIOS : BackendDrivenPushNotificationService
    {
        public MvxBackendDrivenPushNotificationServiceIOS(IPersistedStorage persistedStorage,
            IBackendPushRegistrationService backendPushRegistrationService, IPushTagsProvider pushTagsProvider)
            : base(persistedStorage, backendPushRegistrationService, pushTagsProvider)
        {
        }

        protected override PushPlatformType PlatformType => PushPlatformType.iOS;

        protected override bool IsUserRegisteredToPushService
            => UIApplication.SharedApplication.IsRegisteredForRemoteNotifications;

        protected override void LaunchRegistrationProcess()
        {
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
        }

        protected override void LaunchUnregistrationProcess()
        {
            UIApplication.SharedApplication.UnregisterForRemoteNotifications();
        }

        protected override string ParseRegistrationId(string registrationId)
        {
            return registrationId.Replace(" ", string.Empty).Replace("<", string.Empty).Replace(">", string.Empty);
        }
    }
}