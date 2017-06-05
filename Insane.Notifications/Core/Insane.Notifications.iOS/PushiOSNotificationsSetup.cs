using System.Threading.Tasks;
using Foundation;
using Insane.Notifications.Data;
using Insane.Notifications.iOS.Internal;
using Insane.Notifications.iOS.Presenter;
using Insane.Notifications.PushNotifications;
using UserNotifications;

namespace Insane.Notifications.iOS
{
    public class PushiOSNotificationsSetup
    {
        private PushiOSNotificationsSetup()
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

        public static void Initialize(IIOSRemoteNotificationsPresenter remoteNotificationsPresenter)
        {
            UNUserNotificationCenter.Current.Delegate = new InsaneNotificationsNUserNotificationCenterDelegate(remoteNotificationsPresenter);
        }

        public static void OnRegisterToPushSuccess(RemotePushNotificationService remotePushNotificationsService, NSData deviceTokenData)
		{
		    remotePushNotificationsService.NotifyThatRegistrationSucceed(deviceTokenData.Description);
		}

		public static void OnRegisterToPushFailure(RemotePushNotificationService remotePushNotificationsService, NSError failureError)
		{
			remotePushNotificationsService.NotifyThatRegistrationFailed(failureError.LocalizedDescription ?? string.Empty);
		}
    }
}