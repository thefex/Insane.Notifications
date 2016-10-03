using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Platform;

namespace MvvmCross.Plugins.Notifications.Droid.ToRemove
{
    /// <summary>
    /// remove when mvvmcross gets update
    /// </summary>

    public abstract class MvxWakefulBroadcastReceiver : WakefulBroadcastReceiver
    {
        protected MvxWakefulBroadcastReceiver(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        protected MvxWakefulBroadcastReceiver()
        {
        }

        public override void OnReceive(Context context, Intent intent)
        {
            var setup = MvxAndroidSetupSingleton.EnsureSingletonAvailable(context);
            setup.EnsureInitialized();
        }
    }

    public abstract class MvxIntentService : IntentService
    {
        protected MvxIntentService(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        protected MvxIntentService(string name) : base(name)
        {
        }

        protected MvxIntentService()
        {

        }

        protected override void OnHandleIntent(Intent intent)
        {
            var setup = MvxAndroidSetupSingleton.EnsureSingletonAvailable(ApplicationContext);
            setup.EnsureInitialized();
        }
    }

    public abstract class MvxBroadcastReceiver : BroadcastReceiver
    {
        protected MvxBroadcastReceiver(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        protected MvxBroadcastReceiver()
        {
        }

        public override void OnReceive(Context context, Intent intent)
        {
            var setup = MvxAndroidSetupSingleton.EnsureSingletonAvailable(context);
            setup.EnsureInitialized();
        }
    }
}