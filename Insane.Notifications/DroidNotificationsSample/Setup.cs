using System;
using Android.Content;
using DroidNotificationsSample.Services.LocalNotifications;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Platform;
using MvvmCross.Platform;
using MvvmCross.Plugins.Notifications;
using MvvmCross.Plugins.Notifications.Droid.LocalNotifications;
using MvvmCross.Plugins.Notifications.Droid.NotificationsBuilder;
using NotificationsSample.Portable;
using NotificationsSample.Portable.Data;

namespace DroidNotificationsSample
{
    public class Setup : MvxAndroidSetup
    {
        public Setup(Context applicationContext) : base(applicationContext)
        {
        }

        protected override void InitializeLastChance()
        {
            base.InitializeLastChance();

	        Mvx
		        .RegisterType<MvxDroidNotificationCompatBuilder<LocalNotificationData>, LocalNotificationDataNotificationBuilder>();

            Mvx
                .RegisterType<INotificationsService>(
                    () =>
                    new MvxPeriodicLocalNotificationsService<AppLocalNotificationsPeriodicUpdateAlarmReceiver, AppLocalNotificationsService, LocalNotificationData>(ApplicationContext, TimeSpan.FromSeconds(60)));
        }

        protected override IMvxApplication CreateApp()
            => new MvxApp();
    }
}