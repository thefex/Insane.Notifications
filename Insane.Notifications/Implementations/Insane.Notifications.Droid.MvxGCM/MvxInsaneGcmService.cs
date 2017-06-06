using System;
using Android.Content;
using Insane.Notifications.Droid.GCM;
using Insane.Notifications.PushNotifications;
using MvvmCross.Droid.Platform;
using MvvmCross.Platform;
using Insane.Notifications.Presenter;

namespace Insane.Notifications.Droid.MvxGCM
{
    public abstract class MvxInsaneGcmService : InsaneGcmService
    {
        protected MvxInsaneGcmService()
        {
        }

        protected MvxInsaneGcmService(params string[] senderIds) : base(senderIds)
        {
        }

        public MvxInsaneGcmService(IntPtr javaReference, Android.Runtime.JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        protected override void OnRegistered(Context context, string registrationId)
        {
            var setup = MvxAndroidSetupSingleton.EnsureSingletonAvailable(ApplicationContext);
            setup.EnsureInitialized();

            base.OnRegistered(context, registrationId);
        }

        protected override void OnUnRegistered(Context context, string registrationId)
        {
            var setup = MvxAndroidSetupSingleton.EnsureSingletonAvailable(ApplicationContext);
            setup.EnsureInitialized();

            base.OnUnRegistered(context, registrationId);
        }

        protected override void OnMessage(Context context, Intent intent)
        {
            var setup = MvxAndroidSetupSingleton.EnsureSingletonAvailable(ApplicationContext);
            setup.EnsureInitialized();

            base.OnMessage(context, intent);
        }

        protected override void OnHandleIntent(Intent intent)
        {
            var setup = MvxAndroidSetupSingleton.EnsureSingletonAvailable(ApplicationContext);
            setup.EnsureInitialized();

            base.OnHandleIntent(intent);
        }

        protected override void OnError(Context context, string errorId)
        {
            var setup = MvxAndroidSetupSingleton.EnsureSingletonAvailable(ApplicationContext);
            setup.EnsureInitialized();

            base.OnError(context, errorId);
        }

        protected override GcmRemotePushNotificationService GetNotificationsService()
        {
            var notificationsService = Mvx.Resolve<RemotePushNotificationService>() as GcmRemotePushNotificationService;
            if (notificationsService == null)
                throw new InvalidOperationException(
                    $"{nameof(GcmRemotePushNotificationService)} is not registered for type: {nameof(INotificationsService)}");
            return notificationsService;
        }

        protected override IRemoteNotificationsPresenter GetRemoteNotificationsPresenter()
        {
            return Mvx.Resolve<IRemoteNotificationsPresenter>();
        }
    }
}
