using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platform;
using MvvmCross.Plugins.Notifications.Sample.Portable.ViewModels;

namespace MvvmCross.Plugins.Notifications.Samples.Droid
{
    [Activity(Label = "DroidNotificationsSample", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : MvxCachingFragmentCompatActivity<MainViewModel>
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

