using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.Networking.PushNotifications;
using Windows.Storage;
using Insane.Notifications.Presenter;
using Insane.Notifications.UWP.Internal;
using Insane.Notifications.UWP.Presenter;
using Polly;

namespace Insane.Notifications.UWP
{
    public static class PushServicesExtensions
    {
        private const string ShouldForceSubcribeToPushSettingsKey =
            "INSANELAB_NOTIFICATIONS_SHOULD_FORCE_SUBSCRIBE_KEY";

        public static void HandlePushActivation(IActivatedEventArgs e, IUniversalWindowsRemoteNotificationsPresenter presenter)
        {
            var toastNotificationActivatedEventArgs = e as ToastNotificationActivatedEventArgs;

            if (toastNotificationActivatedEventArgs == null)
                return;
             
            presenter.HandleRemoteNotificationActivation(toastNotificationActivatedEventArgs.Argument ?? string.Empty);
        }

        public static void HandlePushRelatedBackgroundActivation(BackgroundActivatedEventArgs args, IRemoteNotificationsPresenter notificationsPresenter, INotificationsService notificationsService)
        {
            if (args.TaskInstance.Task.Name == PushBackgroundTaskNotificationServiceDecorator.BackgroundTaskName)
                notificationsPresenter.HandleNotification((args.TaskInstance.TriggerDetails as RawNotification).Content);

            if (args.TaskInstance.Task.Name == PushInvalidateRegistrationAppUpdateBackgroundTaskNotificationsServiceDecorator.BackgroundTaskName)
                HandleAppUpdateBackgroundActivation(args, notificationsService);
        }

        private static async void HandleAppUpdateBackgroundActivation(BackgroundActivatedEventArgs args, INotificationsService notificationsService)
        {
            var deferral = args.TaskInstance.GetDeferral();

            try
            {
                await notificationsService.SubscribeToNotifications(true);
                ApplicationData.Current.LocalSettings.Values[ShouldForceSubcribeToPushSettingsKey] = false;
            }
            catch (Exception e)
            {
                ApplicationData.Current.LocalSettings.Values[ShouldForceSubcribeToPushSettingsKey] = true;
            }

            deferral.Complete();
        }

        private static bool isNotificationInSetup = false;
        private static object synchronizationObject = new object();
        public static Task SetupNotificationsIfNeeded(INotificationsService notificationsService)
        {
            return Task.Run(async () =>
            {
                bool? booleanValue = false;
                lock (synchronizationObject)
                {
                    booleanValue = ApplicationData.Current.LocalSettings.Values[ShouldForceSubcribeToPushSettingsKey] as bool?;
                }
                    
                if (booleanValue.HasValue && booleanValue.Value)
                {
                    await Polly.Policy
                        .Handle<Exception>()
                        .WaitAndRetryForever(x => TimeSpan.FromMinutes(5))
                        .ExecuteAsync(async () =>
                        {
                            await notificationsService.SubscribeToNotifications(true);
                            lock (synchronizationObject)
                            {
                                ApplicationData.Current.LocalSettings.Values[ShouldForceSubcribeToPushSettingsKey] = false;
                            }
                        });
                }
            });

        }

    }
}
