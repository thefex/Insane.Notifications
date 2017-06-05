using System.Threading.Tasks;
using Android.Content;
using Insane.Notifications.CachedStorage;
using Insane.Notifications.Data;
using Insane.Notifications.PushNotifications;

namespace Insane.Notifications.Droid.GCM
{
    public class GcmRemotePushNotificationService : RemotePushNotificationService
    {
        private readonly string _pushSenderId;
        readonly Context androidContext;

        public GcmRemotePushNotificationService(IPersistedStorage persistedStorage, IRemotePushRegistrationService remotePushRegistrationService, IPushTagsProvider pushTagsProvider, Context androidContext, string pushSenderId)
            : base(persistedStorage, remotePushRegistrationService, pushTagsProvider)
        {
            this.androidContext = androidContext;
            _pushSenderId = pushSenderId;
        }

        protected override PushPlatformType PlatformType => PushPlatformType.Android;
        protected override bool IsUserRegisteredToPushService => GcmClient.GcmClient.IsRegistered(Mvx.Resolve<Context>());

        protected override Task<ServiceResponse> LaunchRegistrationProcess(bool forceSubscribe = false)
        {
            GcmClient.GcmClient.CheckDevice(androidContext);
            GcmClient.GcmClient.CheckManifest(androidContext);
            GcmClient.GcmClient.Register(androidContext, _pushSenderId);
			return Task.FromResult(new ServiceResponse());
        }

        protected override Task<ServiceResponse> LaunchUnregistrationProcess()
        {
            GcmClient.GcmClient.UnRegister(androidContext);
			return Task.FromResult(new ServiceResponse());
        }

        protected override string ParseRegistrationId(string registrationId)
            => registrationId.Trim('\"');
    }
}