using Android.App;
using Android.Content;
using Android.Support.V4.App;
using Insane.Notifications.Data;
using Insane.Notifications.Presenter.Handlers;
using Newtonsoft.Json;

namespace MvvmCross.Plugins.Notifications.Droid.NotificationsBuilder
{
    public abstract class MvxDroidNotificationCompatBuilder<TNotificationData> : IMvxDroidNotificationBuilder<TNotificationData> where TNotificationData : class
    {
        public const string TapActionIntentExtraName = "INSANELAB_NOTIFICATIONS_tap_action_handler";
        public const string PushDataExtraName = "INSANELAB_NOTIFICATION_push_data";


        public Notification BuildNotification(Context context, TNotificationData notificationData)
        {
            return 
                ConfigureNotificationBuilder(context, notificationData, new NotificationCompat.Builder(context))
                .Build();
        }

        protected PendingIntent GetPendingIntent(Context context, TNotificationData notificationData){
            Intent activityIntent = new Intent(context, GetPendingIntentActivityType());

            var remoteNotificationTapActionProviderResponse = GetRemoteNotificationTapAction();
            if (remoteNotificationTapActionProviderResponse.IsSuccess){
                var remoteNotificationTapActionType = remoteNotificationTapActionProviderResponse.Result.GetType();
                activityIntent.PutExtra(TapActionIntentExtraName, remoteNotificationTapActionType.AssemblyQualifiedName);
            }

            activityIntent.PutExtra(PushDataExtraName, JsonConvert.SerializeObject(notificationData));

            PendingIntent pendingIntent = PendingIntent.GetActivity(context, 0, activityIntent, PendingIntentFlags.UpdateCurrent);
            return pendingIntent;
        }

        protected virtual ServiceResponse<IRemoteNotificationTapAction> GetRemoteNotificationTapAction(){
            return new ServiceResponse<IRemoteNotificationTapAction>().AddErrorMessage("Tap action is not configured");
        }

        protected abstract System.Type GetPendingIntentActivityType();

        protected abstract NotificationCompat.Builder ConfigureNotificationBuilder(Context context, TNotificationData notificationData, NotificationCompat.Builder notificationBuilder);
    }
}