using System.Threading.Tasks;
using Foundation;
using MvvmCross.Platform;
using MvvmCross.Plugins.Notifications.Data;
using MvvmCross.Plugins.Notifications.IOS.Internal;
using MvvmCross.Plugins.Notifications.PushNotifications;
using UIKit;
using UserNotifications;

namespace MvvmCross.Plugins.Notifications.IOS
{
    public class MvxIoSNotificationsSetup
    {
        private MvxIoSNotificationsSetup()
        {

        }

        public static async Task<ServiceResponse> RequestPermissions()
        {
            var authorizationResponse = await UNUserNotificationCenter.Current.RequestAuthorizationAsync(UNAuthorizationOptions.Alert);

            var response = new ServiceResponse();
            if (!authorizationResponse.Item1) // not approved
                response.AddErrorMessage(authorizationResponse.Item2.LocalizedFailureReason);

            return response;
        }

        public static void Initialize(UIApplicationDelegate applicationDelegate)
        {
            UNUserNotificationCenter.Current.Delegate = new MvxUNUserNotificationCenterDelegate();
        }

		public static void OnRegisterToPushSuccess(NSData deviceTokenData)
		{
		    BackendDrivenPushNotificationService.NotifyThatRegistrationSucceed(deviceTokenData.Description);
		}

		public static void OnRegisterToPushFailure(NSError failureError)
		{
		    BackendDrivenPushNotificationService.NotifyThatRegistrationFailed(failureError.LocalizedFailureReason);
		}

        private static BackendDrivenPushNotificationService BackendDrivenPushNotificationService
            => Mvx.Resolve<BackendDrivenPushNotificationService>();
    }
}