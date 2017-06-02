using System.Threading.Tasks;
using Android.Content;
using Insane.Notifications.CachedStorage;
using Insane.Notifications.Data;
using Insane.Notifications.PushNotifications;
using MvvmCross.Platform;

namespace Insane.Notifications.Droid.GCM
{
    public class GcmRemotePushNotificationService : RemotePushNotificationService
    {
        private readonly string _pushSenderId;

        public GcmRemotePushNotificationService(IPersistedStorage persistedStorage,
            IRemotePushRegistrationService remotePushRegistrationService, IPushTagsProvider pushTagsProvider,
            string pushSenderId)
            : base(persistedStorage, remotePushRegistrationService, pushTagsProvider)
        {
            _pushSenderId = pushSenderId;
        }

        protected override PushPlatformType PlatformType => PushPlatformType.Android;
        protected override bool IsUserRegisteredToPushService => GcmClient.GcmClient.IsRegistered(Mvx.Resolve<Context>());

        protected override Task<ServiceResponse> LaunchRegistrationProcess(bool forceSubscribe = false)
        {
            var androidContext = Mvx.Resolve<Context>();
            GcmClient.GcmClient.CheckDevice(androidContext);
            GcmClient.GcmClient.CheckManifest(androidContext);
            GcmClient.GcmClient.Register(androidContext, _pushSenderId);
			return Task.FromResult(new ServiceResponse());
        }

        protected override Task<ServiceResponse> LaunchUnregistrationProcess()
        {
            var androidContext = Mvx.Resolve<Context>();
            GcmClient.GcmClient.UnRegister(androidContext);
			return Task.FromResult(new ServiceResponse());
        }

        protected override string ParseRegistrationId(string registrationId)
            => registrationId.Trim('\"');
    }
}