using System.Linq;
using Windows.UI.Notifications;
using Insane.Notifications.UWP.Handlers.Toast;

namespace Insane.Notifications.PushSample.UWP.Services.Handlers
{
    class ToastNotificationsIdProvider : IToastRemoteNotificationHandlerIdProvider
    {
        public string GetToastNotificationHandlerId(ToastNotification toastNotification)
        {
            var bindingTag = toastNotification.Content.GetElementsByTagName("binding").First();
            var idValue = bindingTag?.Attributes?.FirstOrDefault(x => x.NodeName == "id")?.NodeValue as string ?? string.Empty;

            return idValue;
        }
    }
}