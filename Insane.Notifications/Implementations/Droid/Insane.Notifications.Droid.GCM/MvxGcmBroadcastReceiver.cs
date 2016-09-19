using Android.Content;
using MvvmCross.Droid.Platform;
using MvvmCross.Plugins.Notifications.Droid.GCM.GcmClient;
using MvvmCross.Plugins.Notifications.Presenter;

namespace MvvmCross.Plugins.Notifications.Droid.GCM
{
    public abstract class MvxGcmBroadcastReceiver<TMvxGcmService> : GcmBroadcastReceiverBase<TMvxGcmService>
        where TMvxGcmService : MvxGcmService
    {
        public override void OnReceive(Context context, Intent intent)
        {
            var setup = MvxAndroidSetupSingleton.EnsureSingletonAvailable(context);
            setup.EnsureInitialized();

            base.OnReceive(context, intent);


        }

    }
}