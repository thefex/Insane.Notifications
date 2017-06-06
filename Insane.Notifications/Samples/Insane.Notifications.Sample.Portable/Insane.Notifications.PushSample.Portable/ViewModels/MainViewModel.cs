using System;
using Acr.UserDialogs;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace Insane.Notifications.PushSample.Portable.ViewModels
{
    public class MainViewModel : MvxViewModel
    {
        bool _isRegisteredToPush;
        bool _isRegisteringInProgress;
        readonly INotificationsService remotePushNotificationService;

        public MainViewModel(INotificationsService remotePushNotificationService)
        {
            this.remotePushNotificationService = remotePushNotificationService;
        }

        public bool IsRegisteredToPush => remotePushNotificationService.IsSubscribedToNotifications;

        public bool IsRegisteringInProgress
        {
            get { return _isRegisteringInProgress; }
            set
            {
                if (SetProperty(ref _isRegisteringInProgress, value))
                {
                    RaiseAllPropertiesChanged();
                }
            }
        }

        public bool CanSubscribeToPush => !IsRegisteredToPush && !IsRegisteringInProgress;

        public bool CanUnsubscribeFromPush => IsRegisteredToPush && !IsRegisteringInProgress;

        public MvxCommand SubscribeToPush =>
            new MvxCommand(async () =>
            {
                var userDialog = Mvx.Resolve<IUserDialogs>();
                try
                {
                    IsRegisteringInProgress = true;
                    var subscribeToNotificationResponse = await remotePushNotificationService.SubscribeToNotifications();
                    RaiseAllPropertiesChanged();

                    if (!subscribeToNotificationResponse.IsSuccess)
                        userDialog.Alert(subscribeToNotificationResponse.FormattedErrorMessage, "Error");

                }
                catch (Exception e)
                {
                    userDialog.Alert(e.Message, "Error");
                }
                finally
                {
                    IsRegisteringInProgress = false;
                }
            });

        public MvxCommand UnsubscribeFromPush =>
        new MvxCommand(async () =>
        {
            var userDialog = Mvx.Resolve<IUserDialogs>();

            try
            {
                IsRegisteringInProgress = true;
                var unsubscribeFromNotificationsResponse = await remotePushNotificationService.UnsubscribeFromNotifications();
                RaiseAllPropertiesChanged();

                if (!unsubscribeFromNotificationsResponse.IsSuccess)
                    userDialog.Alert(unsubscribeFromNotificationsResponse.FormattedErrorMessage, "Error");
            }
            catch (Exception e)
            {
                userDialog.Alert(e.Message, "Error");
            }
            finally
            {
                IsRegisteringInProgress = false;
            }
        });
    }
}
