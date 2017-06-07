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

        public static PushNotificationChannel PushNotificationChannel { get; private set; }
        private static IUniversalWindowsRemoteNotificationsPresenter _notificationsPresenter;

        public static void HandlePushActivation(IActivatedEventArgs e, IUniversalWindowsRemoteNotificationsPresenter presenter)
        {
            var toastNotificationActivatedEventArgs = e as ToastNotificationActivatedEventArgs;

            if (toastNotificationActivatedEventArgs == null)
                return;
             
            presenter.HandleRemoteNotificationActivation(toastNotificationActivatedEventArgs.Argument ?? string.Empty);
        }

        public static bool IsPushRelatedBackgroundTask(BackgroundActivatedEventArgs args)
        {
            return args.TaskInstance.Task.Name == PushBackgroundTaskNotificationServiceDecorator.BackgroundTaskName ||
                   args.TaskInstance.Task.Name == PushInvalidateRegistrationAppUpdateBackgroundTaskNotificationsServiceDecorator.BackgroundTaskName;

        }

        public static void HandlePushRelatedBackgroundActivation(BackgroundActivatedEventArgs args, IUniversalWindowsRemoteNotificationsPresenter notificationsPresenter, INotificationsService notificationsService)
        {
            PushServicesExtensions.SetupNotificationsIfNeeded(notificationsService, notificationsPresenter);
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

        public static async Task SetupNotificationsIfNeeded(INotificationsService notificationsService, IUniversalWindowsRemoteNotificationsPresenter notificationsPresenter)
        {
            _notificationsPresenter = notificationsPresenter;
            if (PushNotificationChannel == null)
            {
                PushNotificationChannel = await PushNotificationChannelManager
                    .CreatePushNotificationChannelForApplicationAsync();
                PushNotificationChannel.PushNotificationReceived += PushChannel_PushNotificationReceived;
            }

            await Task.Run(async () =>
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

        private static void PushChannel_PushNotificationReceived(PushNotificationChannel sender, PushNotificationReceivedEventArgs args)
        {
            if (args.NotificationType == PushNotificationType.Raw)
            {
                args.Cancel = true;
                _notificationsPresenter.HandleNotification(args.RawNotification.Content);
            }
            else
            {
                _notificationsPresenter.HandlePlatformNotification(args);
            }
        }
    }
}
