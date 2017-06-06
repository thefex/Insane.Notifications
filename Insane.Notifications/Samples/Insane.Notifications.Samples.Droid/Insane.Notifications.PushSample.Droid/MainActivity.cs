using Android.App;
using Android.Widget;
using Android.OS;
using MvvmCross.Droid.Support.V7.AppCompat;
using Insane.Notifications.PushSample.Portable.ViewModels;
using System;
using Android.Runtime;
using Insane.Notifications.Droid.Extensions;

namespace Insane.Notifications.PushSample.Droid
{
    [Activity(Label = "Insane.Notifications.PushSample.Droid", MainLauncher = true, Icon = "@mipmap/icon")]
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
            SetContentView(Resource.Layout.Main);

			Intent?.Extras?.HandlePushDataIfExists();
		}

		protected override void OnNewIntent(Android.Content.Intent intent)
		{
			base.OnNewIntent(intent);

			intent?.Extras?.HandlePushDataIfExists();
		}
    }
}

