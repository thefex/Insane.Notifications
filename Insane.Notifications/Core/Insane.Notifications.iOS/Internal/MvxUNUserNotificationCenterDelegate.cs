using System;
using System.Collections.Generic;
using Foundation;
using MvvmCross.Plugins.Notifications.IOS.Presenter;
using UserNotifications;

namespace MvvmCross.Plugins.Notifications.IOS.Internal
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
