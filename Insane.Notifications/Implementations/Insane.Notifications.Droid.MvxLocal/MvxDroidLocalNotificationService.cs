using System;
using System.Threading.Tasks;
using Insane.Notifications.Data;
using Insane.Notifications.Droid.Local;
using MvvmCross.Droid.Platform;
using MvvmCross.Platform;
using MvvmCross.Plugins.Notifications.Droid.NotificationsBuilder;

namespace Insane.Notifications.Droid.MvxLocal
{
    public abstract class MvxDroidLocalNotificationService<TNotificationData> : DroidLocalNotificationService<TNotificationData> where TNotificationData : class
    {
        public MvxDroidLocalNotificationService() : base()
        {
            
		}

		protected MvxDroidLocalNotificationService(string name) : base(name)
        {
		}


        public MvxDroidLocalNotificationService(IntPtr javaReference, Android.Runtime.JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        protected override void OnHandleIntent(Android.Content.Intent intent)
        {
            var setup = MvxAndroidSetupSingleton.EnsureSingletonAvailable(this.ApplicationContext);
			setup.EnsureInitialized();

            base.OnHandleIntent(intent);
        }

        protected override DroidNotificationCompatBuilder<TNotificationData> GetNotificationCompatBuilder()
        {
            return Mvx.Resolve<DroidNotificationCompatBuilder<TNotificationData>>();
        }
    }
}
