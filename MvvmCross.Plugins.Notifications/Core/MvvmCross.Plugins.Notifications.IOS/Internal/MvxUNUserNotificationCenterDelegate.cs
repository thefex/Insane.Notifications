using System;
using Foundation;
using UserNotifications;

namespace MvvmCross.Plugins.Notifications.IOS.Internal
{
	public class MvxUNUserNotificationCenterDelegate : UNUserNotificationCenterDelegate
	{
	    public MvxUNUserNotificationCenterDelegate()
	    {
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
	        base.DidReceiveNotificationResponse(center, response, completionHandler);
	    }

	    public override void WillPresentNotification(UNUserNotificationCenter center, UNNotification notification, Action<UNNotificationPresentationOptions> completionHandler)
	    {
	        base.WillPresentNotification(center, notification, completionHandler);


	    }
	}
}
