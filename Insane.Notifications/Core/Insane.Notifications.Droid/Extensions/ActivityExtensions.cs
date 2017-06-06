using System;
using Android.OS;
using Insane.Notifications.Data;
using Insane.Notifications.Presenter.Handlers;
using Newtonsoft.Json;
using Insane.Notifications.Droid.NotificationsBuilder;

namespace Insane.Notifications.Droid.Extensions
{
    public static class ActivityExtensions
    {
        public static void HandlePushDataIfExists(this Bundle extrasBundle){
            if (extrasBundle == null)
                return;

            var remoteNotificationTapActionHandlerTypeResponse = GetRemoteNotificationTapActionHandlerType(extrasBundle);

            if (!remoteNotificationTapActionHandlerTypeResponse.IsSuccess)
                return;

            var pushDataExtractionResponse = ExtractPushNotificationData(extrasBundle);

            if (!pushDataExtractionResponse.IsSuccess)
                return;

            var pushJsonData = pushDataExtractionResponse.Result;

			var remoteNotificationTapActionHandler = Activator.CreateInstance(remoteNotificationTapActionHandlerTypeResponse.Result) as IRemoteNotificationTapAction;

			if (remoteNotificationTapActionHandler == null)
			{
				System.Diagnostics.Debug.WriteLine("WARNING ! Check Linker/Proguard settings, RemoteNotificationTapACtionHandler is null.");
				return;
			}

			remoteNotificationTapActionHandler.OnNotificationTapped(pushDataExtractionResponse.Result);
        }

        public static ServiceResponse<Type> GetRemoteNotificationTapActionHandlerType(this Bundle extrasBundle){
            if (extrasBundle == null)
                return new ServiceResponse<Type>().AddErrorMessage("Bundle is null");

            var remoteNotificationTapActionHandlerTypeBundleKey = DroidNotificationCompatBuilder<ServiceResponse>.TapActionIntentExtraName;

            if (!extrasBundle.ContainsKey(remoteNotificationTapActionHandlerTypeBundleKey))
                return new ServiceResponse<Type>().AddErrorMessage("Extras do not have Tap Action Handler Type. Perhaps this push do not have any action on tap.");

            var typeString = extrasBundle.GetString(remoteNotificationTapActionHandlerTypeBundleKey);
            return ServiceResponse<Type>.Build(Type.GetType(typeString));
        }

        public static ServiceResponse<string> ExtractPushNotificationData(this Bundle extrasBundle)
        {
            if (extrasBundle == null)
                return new ServiceResponse<string>().AddErrorMessage("Bundle is null");

            var pushDataExtrasName = DroidNotificationCompatBuilder<ServiceResponse>.PushDataExtraName;
            if (!extrasBundle.ContainsKey(pushDataExtrasName))
                return new ServiceResponse<string>().AddErrorMessage("Extras do not have PusH Data Extra Value inside bundle. Are you sure you have used " +
                                                                     "MvxDroidNotificationCompatBuilder<T> ?");
            
            return ServiceResponse<string>.Build(extrasBundle.GetString(pushDataExtrasName));
        }

        public static ServiceResponse<TNotificationData> ExtractPushNotificationData<TNotificationData>(this Bundle extrasBundle){
            try {
                var pushNotificationDataResponse = ExtractPushNotificationData(extrasBundle);

                if (!pushNotificationDataResponse.IsSuccess)
                    return pushNotificationDataResponse.CloneFailedResponse<TNotificationData>();

                var deserializedData = JsonConvert.DeserializeObject<TNotificationData>(pushNotificationDataResponse.Result);
                return ServiceResponse<TNotificationData>.Build(deserializedData);
            } catch (JsonException e) {
                return new ServiceResponse<TNotificationData>().AddErrorMessage("Can't deserialize object to requested notification type.");
            }

        }
    }
}
