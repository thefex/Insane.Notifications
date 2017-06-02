using System;
using Android.Content;
using MvvmCross.Droid.Platform;
using MvvmCross.Platform;
using MvvmCross.Plugins.Notifications.Droid.GCM.GcmClient;
using MvvmCross.Plugins.Notifications.Presenter;
using MvvmCross.Plugins.Notifications.PushNotifications;

namespace MvvmCross.Plugins.Notifications.Droid.GCM
{
    public abstract class MvxGcmService : GcmServiceBase
    {
        protected MvxGcmService()
        {
        }

        protected MvxGcmService(params string[] senderIds) : base(senderIds)
        {
        }

        protected override void OnRegistered(Context context, string registrationId)
        {
			var setup = MvxAndroidSetupSingleton.EnsureSingletonAvailable(ApplicationContext);
			setup.EnsureInitialized();

            GetNotificationsService().NotifyThatRegistrationSucceed(registrationId);
        }

        protected override void OnUnRegistered(Context context, string registrationId)
        {
			var setup = MvxAndroidSetupSingleton.EnsureSingletonAvailable(ApplicationContext);
			setup.EnsureInitialized();

            GetNotificationsService().NotifyThatUnregistrationSucceed();
        }

        protected override void OnMessage(Context context, Intent intent)
        {
			var setup = MvxAndroidSetupSingleton.EnsureSingletonAvailable(ApplicationContext);
			setup.EnsureInitialized();

			var pushJson = GetPushJsonData(intent);
			var remoteNotificationsPresenter = GetRemoteNotificationsPresenter();

			remoteNotificationsPresenter.HandleNotification(pushJson);
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

            var notificationsService = GetNotificationsService();

            // todo: create push specific exception
            notificationsService.NotifyThatRegistrationFailed(new InvalidOperationException(errorId));
            notificationsService.NotifyThatUnregistrationFailed(new InvalidOperationException(errorId));
        }

        protected GcmRemotePushNotificationService GetNotificationsService()
        {
            var notificationsService = Mvx.Resolve<RemotePushNotificationService>() as GcmRemotePushNotificationService;
            if (notificationsService == null)
                throw new InvalidOperationException(
                    $"{nameof(GcmRemotePushNotificationService)} is not registered for type: {nameof(INotificationsService)}");
            return notificationsService;
        }

        protected abstract IRemoteNotificationsPresenter GetRemoteNotificationsPresenter();

        protected abstract string GetPushJsonData(Intent fromIntent);
    }
}