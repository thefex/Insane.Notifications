using System.Threading.Tasks;
using Insane.Notifications.Data;

namespace Insane.Notifications.PushNotifications
{
    public interface IRemotePushRegistrationService
    {
        Task<ServiceResponse> UnsubscribeFromPush(string registrationId);

        Task<ServiceResponse> UpdateDeviceRegistration(PushSubscribeDetails subscribeDetails);

        Task<ServiceResponse<string>> RegisterDevice(string deviceHandle);
    }
}