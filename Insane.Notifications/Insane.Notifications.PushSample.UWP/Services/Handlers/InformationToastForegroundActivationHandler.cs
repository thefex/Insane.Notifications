using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Notifications;
using Acr.UserDialogs;
using Insane.Notifications.Presenter;
using Insane.Notifications.Presenter.Handlers;
using Insane.Notifications.UWP.Handlers.Toast;
using MvvmCross.Platform;

namespace Insane.Notifications.PushSample.UWP.Services.Handlers
{
    [ToastRemoteNotificationHandler("information")]
    class InformationToastForegroundActivationHandler : ToastRemoteNotificationHandler
    {
        protected override void OnToastTapped(ToastNotification toastNotification)
        {
            base.OnToastTapped(toastNotification);
            var textTag = toastNotification.Content.GetElementsByTagName("text").First();
            var toastContent = textTag?.FirstChild.NodeValue as string;

            var userDialogs = Mvx.Resolve<IUserDialogs>();
            userDialogs.Alert(toastContent, "You have tapped on PUSH!");
        }
    }
    
}
