using System;
using Android.App;
using Android.Widget;
using Android.OS;
using Android.Runtime;
using MvvmCross.Droid.Views;
using MvvmCross.Platform;
using MvvmCross.Plugins.Notifications;

namespace DroidNotificationsSample
{
    [Activity(Label = "DroidNotificationsSample", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : MvxActivity
    {
        public MainActivity(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public MainActivity()
        {
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView (Resource.Layout.Main);

	        Mvx.Resolve<INotificationsService>().SubscribeToNotifications();
        }
    }
}

