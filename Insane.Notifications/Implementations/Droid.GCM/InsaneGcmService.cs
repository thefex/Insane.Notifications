using System;
using Android.Content;
using Insane.Notifications.Droid.GCM.GcmClient;
using Insane.Notifications.Presenter;

namespace Insane.Notifications.Droid.GCM
{
    public abstract class InsaneGcmService : GcmServiceBase
    {
        protected InsaneGcmService()
        {
        }

        public InsaneGcmService(IntPtr javaReference, Android.Runtime.JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        protected InsaneGcmService(params string[] senderIds) : base(senderIds)
		{
        }

        protected override void OnRegistered(Context context, string registrationId)
        {
            GetNotificationsService().NotifyThatRegistrationSucceed(registrationId);
        }

        protected override void OnUnRegistered(Context context, string registrationId)
        {
            GetNotificationsService().NotifyThatUnregistrationSucceed();
        }

        protected override void OnMessage(Context context, Intent intent)
        {
            var pushJson = GetPushJsonData(intent);
            var remoteNotificationsPresenter = GetRemoteNotificationsPresenter();

            remoteNotificationsPresenter.HandleNotification(pushJson);
        }

        protected override void OnError(Context context, string errorId)
        {
            var notificationsService = GetNotificationsService();

            // todo: create push specific exception
            notificationsService.NotifyThatRegistrationFailed(new InvalidOperationException(errorId));
            notificationsService.NotifyThatUnregistrationFailed(new InvalidOperationException(errorId));
        }

        protected abstract GcmRemotePushNotificationService GetNotificationsService();
        protected abstract IRemoteNotificationsPresenter GetRemoteNotificationsPresenter();

        protected abstract string GetPushJsonData(Intent fromIntent);
    }
}