using Insane.PushSample.Backend.Services.Abstract;
using Insane.PushSample.Backend.Services.Concrete;
using Microsoft.Azure.NotificationHubs;

namespace Insane.PushSample.Backend.Services
{
    public class NotificationHubRegistrationServices
    {
        public NotificationHubRegistrationServices(DefaultTagSubscriptionService tagSubscriptionService)
        {
            NotificationHub = NotificationHubClient.CreateClientFromConnectionString("Endpoint=sb://ENTER_YOUR_SHARED_ACCESS_KEY_ENDPOINT", "ENTER_HUB_NAME_HERE");
            TagSubscriptionService = tagSubscriptionService;
        }

        public NotificationHubClient NotificationHub { get; }
        public TagSubscriptionService TagSubscriptionService { get; }
    }
}