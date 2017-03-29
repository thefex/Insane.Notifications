using System.Threading.Tasks;
using Android.Content;
using Gcm.Client;
using MvvmCross.Platform;
using MvvmCross.Plugins.Notifications.CachedStorage;
using MvvmCross.Plugins.Notifications.Data;
using MvvmCross.Plugins.Notifications.PushNotifications;

namespace MvvmCross.Plugins.Notifications.Droid.GCM
{
    public class GcmBackendDrivenPushNotificationService : BackendDrivenPushNotificationService
    {
        private readonly string _pushSenderId;

        public GcmBackendDrivenPushNotificationService(IPersistedStorage persistedStorage,
            IBackendPushRegistrationService backendPushRegistrationService, IPushTagsProvider pushTagsProvider,
            string pushSenderId)
            : base(persistedStorage, backendPushRegistrationService, pushTagsProvider)
        {
            _pushSenderId = pushSenderId;
        }

        protected override PushPlatformType PlatformType => PushPlatformType.Android;
        protected override bool IsUserRegisteredToPushService => GcmClient.IsRegistered(Mvx.Resolve<Context>());

		protected override Task<ServiceResponse> LaunchRegistrationProcess()
        {
            var androidContext = Mvx.Resolve<Context>();
            GcmClient.CheckDevice(androidContext);
            GcmClient.CheckManifest(androidContext);
            GcmClient.Register(androidContext, _pushSenderId);
			return Task.FromResult(new ServiceResponse());
        }

        protected override Task<ServiceResponse> LaunchUnregistrationProcess()
        {
            var androidContext = Mvx.Resolve<Context>();
            GcmClient.UnRegister(androidContext);
			return Task.FromResult(new ServiceResponse());
        }

        protected override string ParseRegistrationId(string registrationId)
            => registrationId.Trim('\"');
    }
}