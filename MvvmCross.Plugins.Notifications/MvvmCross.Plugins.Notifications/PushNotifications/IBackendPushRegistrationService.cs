using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Plugins.Notifications.Data;

namespace MvvmCross.Plugins.Notifications.PushNotifications
{
	public interface IBackendPushRegistrationService
	{
		Task<ServiceResponse> UnsubscribeFromPush(string registrationId);

		Task<ServiceResponse> UpdateDeviceRegistration(PushSubscribeDetails subscribeDetails);

		Task<ServiceResponse<string>> RegisterDevice(string deviceHandle);
	}
}
