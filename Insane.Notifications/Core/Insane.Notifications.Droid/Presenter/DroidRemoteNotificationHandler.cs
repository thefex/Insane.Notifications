using System;
using Android.Content;
using Insane.Notifications.Presenter.Handlers;
using MvvmCross.Plugins.Notifications.Droid.NotificationsBuilder;
using MvvmCross.Plugins.Notifications.Droid.Extensions;

namespace MvvmCross.Plugins.Notifications.Droid.Presenter
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

        protected abstract IMvxDroidNotificationBuilder<TNotification> GetNotificationBuilder();
    }
}
