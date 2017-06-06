using System;
using Acr.UserDialogs;
using Android.Content;
using Android.Support.V4.App;
using Insane.Notifications.Data;
using Insane.Notifications.Presenter;
using Insane.Notifications.Presenter.Handlers;
using Insane.Notifications.PushSample.Portable.Data.Push;
using MvvmCross.Platform;
using Android.Media;
using Insane.Notifications.Droid.Presenter;
using Insane.Notifications.Droid.NotificationsBuilder;

namespace Insane.Notifications.PushSample.Droid.Services.PushHandlers
{
    [RemoteNotificationHandler("information")]
    public class InformationMessagePushHandler : DroidRemoteNotificationHandler<PushData>
    {
        public InformationMessagePushHandler() : base(Mvx.Resolve<Context>())
        {
        }

        protected override IDroidNotificationBuilder<PushData> GetNotificationBuilder()
        {
            return new InformationMesageNotificationBuilder();
        }

		class InformationMesageNotificationBuilder : DroidNotificationCompatBuilder<PushData>
		{
            protected override NotificationCompat.Builder ConfigureNotificationBuilder(Context context, PushData notificationData, NotificationCompat.Builder notificationBuilder)
            {
				return
					notificationBuilder
						.SetAutoCancel(true)
						.SetContentTitle(notificationData.Title)
						.SetContentText(notificationData.Content)
                        .SetContentIntent(GetPendingIntent(context, notificationData))
                        .SetSmallIcon(Resource.Mipmap.Icon)
                        .SetVisibility(NotificationCompat.VisibilityPublic)
						.SetPriority(2)
						.SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Notification));
            }

            protected override Type GetPendingIntentActivityType()
			{
				return typeof(MainActivity);
			}

			protected override ServiceResponse<IRemoteNotificationTapAction> GetRemoteNotificationTapAction()
			{
				return ServiceResponse<IRemoteNotificationTapAction>.Build(new InformationMessageTapActionHandler());
			}
		}

        class InformationMessageTapActionHandler : RemoteNotificationTapAction<PushData>
		{
			public override void OnNotificationTapped(PushData notificationData)
            {
                var userDialogs = Mvx.Resolve<IUserDialogs>();
                userDialogs.Alert("You have clicked at PUSH!", notificationData.Title);
            }
		}
    }
 

}
