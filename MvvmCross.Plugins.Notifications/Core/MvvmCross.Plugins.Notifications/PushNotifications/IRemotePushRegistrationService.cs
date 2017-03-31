using System.Threading.Tasks;
using MvvmCross.Plugins.Notifications.Data;

namespace MvvmCross.Plugins.Notifications.PushNotifications
{
    public interface IRemotePushRegistrationService
    {
        Task<ServiceResponse> UnsubscribeFromPush(string registrationId);

        Task<ServiceResponse> UpdateDeviceRegistration(PushSubscribeDetails subscribeDetails);

        Task<ServiceResponse<string>> RegisterDevice(string deviceHandle);
    }
}