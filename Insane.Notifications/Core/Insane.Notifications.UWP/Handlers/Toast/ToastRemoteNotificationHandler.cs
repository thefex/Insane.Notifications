using Windows.UI.Notifications;

namespace InsaneNotifications.UWP.Handlers
{
    public abstract class ToastRemoteNotificationHandler
    {
        public bool HandleToastNotification(ToastNotification toastNotification)
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
