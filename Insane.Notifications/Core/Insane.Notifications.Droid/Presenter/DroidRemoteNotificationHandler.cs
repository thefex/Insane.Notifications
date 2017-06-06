using System;
using Android.Content;
using Insane.Notifications.Droid.Extensions;
using Insane.Notifications.Droid.NotificationsBuilder;
using Insane.Notifications.Presenter.Handlers;

namespace Insane.Notifications.Droid.Presenter
{
    public abstract class DroidRemoteNotificationHandler<TNotification> : RemoteNotificationHandler<TNotification>
        where TNotification : class
    {
        readonly Context context;

        public DroidRemoteNotificationHandler(Context context)
        {
            this.context = context;
        }

        public override bool Handle(TNotification notification, string notificationId)
        {
            var notificationBuilder = GetNotificationBuilder();
            var droidNotification = notificationBuilder.BuildNotification(context, notification);

            droidNotification.Show(context);
            return true;
        }

        protected abstract IDroidNotificationBuilder<TNotification> GetNotificationBuilder();
    }
}
