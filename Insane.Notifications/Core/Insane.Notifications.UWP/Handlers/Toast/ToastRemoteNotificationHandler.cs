using Windows.UI.Notifications;

namespace Insane.Notifications.UWP.Handlers.Toast
{
    public abstract class ToastRemoteNotificationHandler
    {
        public virtual bool HandleToastNotification(ToastNotification toastNotification)
        {
            toastNotification.Activated += ToastNotification_Activated;
            return !ShouldShowDefaultToast;
        }

        protected virtual void OnToastTapped(ToastNotification toastNotification)
        {
            
        }

        public virtual bool ShouldShowDefaultToast => true;

        private void ToastNotification_Activated(ToastNotification sender, object args)
        {
            OnToastTapped(sender);
        }
    }
}
