using System;
using Android.Content;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Platform;
using MvvmCross.Platform;
using MvvmCross.Plugins.Notifications.Droid.LocalNotifications;
using MvvmCross.Plugins.Notifications.Droid.NotificationsBuilder;
using MvvmCross.Plugins.Notifications.Sample.Portable;
using MvvmCross.Plugins.Notifications.Sample.Portable.Data;
using MvvmCross.Plugins.Notifications.Samples.Droid.Services.LocalNotifications;

namespace MvvmCross.Plugins.Notifications.Samples.Droid
{
    public class Setup : MvxAndroidSetup
    {
        public Setup(Context applicationContext) : base(applicationContext)
        {
        }

        //protected override void InitializeLastChance()
        //{
        //    base.InitializeLastChance();

	       // Mvx
		      //  .RegisterType<MvxDroidNotificationCompatBuilder<LocalNotificationData>, LocalNotificationDataNotificationBuilder>();

        //    Mvx
        //        .RegisterType<INotificationsService>(
        //            () =>
        //            new MvxPeriodicLocalNotificationsService<AppLocalNotificationsPeriodicUpdateAlarmReceiver, AppLocalNotificationsService, LocalNotificationData>(ApplicationContext, TimeSpan.FromSeconds(60)));
        //}

        protected override IMvxApplication CreateApp()
			=> new MvxApp();
    }
}