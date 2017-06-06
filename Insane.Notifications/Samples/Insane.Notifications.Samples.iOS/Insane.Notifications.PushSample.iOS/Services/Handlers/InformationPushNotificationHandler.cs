using System;
using Insane.Notifications.iOS.Presenter.Handlers;
using Insane.Notifications.iOS.Presenter.Handlers.Alert;
using Insane.Notifications.Presenter;
using UserNotifications;
using MvvmCross.Platform;
using Acr.UserDialogs;
using Insane.Notifications.PushSample.Portable.Data.Push;
using Insane.Notifications.iOS.Data;

namespace Insane.Notifications.PushSample.iOS.Services.Handlers
{
	[RemoteNotificationHandler("information")]
	public class InformationPushNotificationHandler : iOSRemoteNotificationTapAction<APNSPushData<PushData>>, IIOSAlertNotificationHandler
	{
		public bool HandleAlertNotification(UNNotificationContent notificationContent, UNNotification completeNotificiation)
		{
			return true;
		}

        public override void OnNotificationTapped(APNSPushData<PushData> notificationData)
        {
			var userDialogs = Mvx.Resolve<IUserDialogs>();
            userDialogs.Alert("You have tapped notification!", notificationData.Data.Title);
        }
	}
}
