using System;
using System.Threading.Tasks;
using Windows.Networking.PushNotifications;
using Insane.Notifications.CachedStorage;
using Insane.Notifications.Data;
using Insane.Notifications.PushNotifications;
using Insane.Notifications.UWP.Internal;
using Insane.Notifications.UWP.Presenter;
using Insane.Notifications.UWP.Storage;

namespace Insane.Notifications.UWP
{
    internal class UniversalWindowsRemotePushNotificationService : RemotePushNotificationService
    {
        private const string IsRegisteredToPushKey = "IsRegisteredToPushUwpKey";
        private readonly IPersistedStorage _persistedStorage;

        public UniversalWindowsRemotePushNotificationService(
            IRemotePushRegistrationService remotePushRegistrationService, IPushTagsProvider pushTagsProvider)
            : this(new UWPDefaultPersistedStorage(), remotePushRegistrationService, pushTagsProvider)
        {
        }

        public UniversalWindowsRemotePushNotificationService(IPersistedStorage persistedStorage,
            IRemotePushRegistrationService remotePushRegistrationService, IPushTagsProvider pushTagsProvider) : base(persistedStorage,
            remotePushRegistrationService, pushTagsProvider)
        {
            _persistedStorage = persistedStorage;
        }


        protected override PushPlatformType PlatformType => PushPlatformType.Windows;

        protected override bool IsUserRegisteredToPushService => _persistedStorage.Has(IsRegisteredToPushKey) &&
                                                                 _persistedStorage
                                                                     .Get<ValueTypeWrapper<bool>>(IsRegisteredToPushKey)
                                                                     .Data;

        protected override async Task<ServiceResponse> LaunchRegistrationProcess(bool forceSubscribe = false)
        {
            if (IsUserRegisteredToPushService && !forceSubscribe)
                return new ServiceResponse().AddErrorMessage("User is already registerd to push.");

            var pushChannel = PushServicesExtensions.PushNotificationChannel;

            NotifyThatRegistrationSucceed(pushChannel.Uri);
            _persistedStorage.SaveOrUpdate(IsRegisteredToPushKey, new ValueTypeWrapper<bool> {Data = true});

            return new ServiceResponse();
        }

        protected override Task<ServiceResponse> LaunchUnregistrationProcess()
        {
            if (!IsUserRegisteredToPushService)
                return Task.FromResult(new ServiceResponse().AddErrorMessage("User is not registerd to push"));

            _persistedStorage.SaveOrUpdate(IsRegisteredToPushKey, new ValueTypeWrapper<bool>
            {
                Data = false
            });
            NotifyThatUnregistrationSucceed();

            return Task.FromResult(new ServiceResponse());
        }

        protected override string ParseRegistrationId(string registrationId)
        {
            return registrationId;
        }
    }
}