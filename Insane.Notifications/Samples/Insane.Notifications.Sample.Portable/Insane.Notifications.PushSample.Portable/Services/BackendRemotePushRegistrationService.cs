using System;
using System.Threading;
using System.Threading.Tasks;
using Insane.Notifications.Data;
using Insane.Notifications.PushNotifications;
using Insane.Notifications.PushSample.Portable.Data;
using Insane.Notifications.PushSample.Portable.Data.Push;
using Insane.Notifications.PushSample.Portable.Services.RestInterfaces;
using Insane.Notifications.PushSample.Portable.Extensions;
using System.Collections;
using System.Collections.Generic;

namespace Insane.Notifications.PushSample.Portable.Services
{
	public class BackendBasedRemotePushRegistrationService : IRemotePushRegistrationService
	{
        private readonly AppRestService _restService;

		public BackendBasedRemotePushRegistrationService(AppRestService restService)
		{
			_restService = restService;
		}

		public async Task<ServiceResponse> UnsubscribeFromPush(string registrationId)
		{
			var unsubscribeFromPushResponse = await _restService.Execute<IPushNotificationsApi>(api => api.Unsubscribe(new UnsubscribeFromPushRequest()
			{
				Id = registrationId
            }, CancellationToken.None));

			return unsubscribeFromPushResponse.ToServiceResponse();
		}

		public async Task<ServiceResponse> UpdateDeviceRegistration(PushSubscribeDetails subscribeDetails)
		{
			PlatformType platformType;

			switch (subscribeDetails.PushPlatformType)
			{
				case PushPlatformType.Android:
					platformType = PlatformType.GCM;
					break;
				case PushPlatformType.iOS:
					platformType = PlatformType.APNS;
					break;
				case PushPlatformType.Windows:
					platformType = PlatformType.WNS;
					break;
				default:
					throw new InvalidOperationException("Can't recognize platform type.");
			}

			var updateDeviceRegistrationResponse = await _restService.Execute<IPushNotificationsApi>(api =>
				api.UpdateSubscription(new UpdatePushSubscriptionRequest()
				{
					Handle = subscribeDetails.PushHandle,
					Id = subscribeDetails.DeviceId,
					Platform = platformType,
					Tags = new List<string>() { "new_tickets", "chat_message_added" }
				}, CancellationToken.None));

			return updateDeviceRegistrationResponse.ToServiceResponse();
		}

		public async Task<ServiceResponse<string>> RegisterDevice(string deviceHandle)
		{
			var subscribeRequest = new SubscribeToPushRequest() { Handle = deviceHandle };
			var registrationResponse = await _restService.Execute<IPushNotificationsApi, PushRegistrationResponse>(api => api.Subscribe(subscribeRequest, CancellationToken.None));

			return !registrationResponse.IsSuccess ?
				registrationResponse.ToServiceResponse().CloneFailedResponse<string>() :
				ServiceResponse<string>.Build(registrationResponse.Results.RegistrationId);
		}
	}
}
