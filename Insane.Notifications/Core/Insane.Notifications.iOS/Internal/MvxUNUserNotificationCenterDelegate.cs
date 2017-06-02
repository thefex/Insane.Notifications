using System;
using Foundation;
using Insane.Notifications.iOS.Presenter;
using UserNotifications;

namespace Insane.Notifications.iOS.Internal
{
	public class MvxUNUserNotificationCenterDelegate : UNUserNotificationCenterDelegate
	{
        readonly IIOSRemoteNotificationsPresenter remoteNotificationsPresenter;

        public MvxUNUserNotificationCenterDelegate(IIOSRemoteNotificationsPresenter remoteNotificationsPresenter)
	    {
            this.remoteNotificationsPresenter = remoteNotificationsPresenter;
        }

	    protected MvxUNUserNotificationCenterDelegate(NSObjectFlag t) : base(t)
	    {
	    }

	    protected internal MvxUNUserNotificationCenterDelegate(IntPtr handle) : base(handle)
	    {
	    }



	    public override void DidReceiveNotificationResponse(UNUserNotificationCenter center, UNNotificationResponse response,
	        Action completionHandler)
	    {
            remoteNotificationsPresenter.HandleNotificationTapped(response);
			completionHandler();
	    }


	    public override void WillPresentNotification(UNUserNotificationCenter center, UNNotification notification, Action<UNNotificationPresentationOptions> completionHandler)
	    {
            UNUserNotificationCenter.Current.Delegate = this;

            var notificationResponse = remoteNotificationsPresenter.HandleNotification(center, notification);
            completionHandler(notificationResponse);
	    }
	}
}
