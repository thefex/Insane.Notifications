using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Insane.Notifications.Data;

namespace Insane.Notifications.UWP.Internal
{
    internal class PushInvalidateRegistrationAppUpdateBackgroundTaskNotificationsServiceDecorator : INotificationsService
    {
        public const string BackgroundTaskName = "InsaneNotifications.UWP.AppUpdatePushRegistrationInvalidateHandler";

        private readonly INotificationsService _decoratedNotificationsService;

        public PushInvalidateRegistrationAppUpdateBackgroundTaskNotificationsServiceDecorator(INotificationsService decoratedNotificationsService)
        {
            _decoratedNotificationsService = decoratedNotificationsService;
        }

        public async Task<ServiceResponse> SubscribeToNotifications(bool forceSubscribe = false)
        {
            RegisterPushBackgroundTask();

            var response = await _decoratedNotificationsService.SubscribeToNotifications(forceSubscribe);

            if (!response.IsSuccess)
                UnregisterFromPushBackgroundTask();

            return response;
        }

        private void RegisterPushBackgroundTask()
        {
            if (BackgroundTaskRegistration.AllTasks.Any(x => x.Value.Name == BackgroundTaskName))
                return;

            var builder = new BackgroundTaskBuilder()
            {
                Name = BackgroundTaskName
            };
            builder.SetTrigger(new SystemTrigger(SystemTriggerType.ServicingComplete, false));
            builder.Register();
        }

        public async Task<ServiceResponse> UnsubscribeFromNotifications()
        {
            UnregisterFromPushBackgroundTask();

            var response = await _decoratedNotificationsService.UnsubscribeFromNotifications();

            if (!response.IsSuccess)
                RegisterPushBackgroundTask();

            return response;
        }

        private void UnregisterFromPushBackgroundTask()
        {
            if (BackgroundTaskRegistration.AllTasks.All(x => x.Value.Name != BackgroundTaskName))
                return;

            BackgroundTaskRegistration.AllTasks.FirstOrDefault(x => x.Value.Name == BackgroundTaskName).Value.Unregister(true);
        }
    }
}