using Android.Content;
using Gcm.Client;
using MvvmCross.Droid.Platform;

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