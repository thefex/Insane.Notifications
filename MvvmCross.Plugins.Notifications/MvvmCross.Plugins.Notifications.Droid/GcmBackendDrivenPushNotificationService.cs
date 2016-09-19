using System;
using Android.Content;
using Gcm.Client;
using MvvmCross.Platform;
using MvvmCross.Plugins.Notifications.CachedStorage;
using MvvmCross.Plugins.Notifications.Data;
using MvvmCross.Plugins.Notifications.PushNotifications;

namespace MvvmCross.Plugins.Notifications.Droid
{
	public class GcmBackendDrivenPushNotificationService : BackendDrivenPushNotificationService
	{
		private readonly string _pushSenderId;

		public GcmBackendDrivenPushNotificationService(IPersistedStorage persistedStorage,
			IBackendPushRegistrationService backendPushRegistrationService, IPushTagsProvider pushTagsProvider, string pushSenderId)
			: base(persistedStorage, backendPushRegistrationService, pushTagsProvider)
		{
			_pushSenderId = pushSenderId;
		}

		protected override PushPlatformType PlatformType => PushPlatformType.Android;
		protected override bool IsUserRegisteredToPushService => GcmClient.IsRegistered(Mvx.Resolve<Context>());

		protected override void LaunchRegistrationProcess()
		{
			var androidContext = Mvx.Resolve<Context>();
			GcmClient.CheckDevice(androidContext);
			GcmClient.CheckManifest(androidContext);
			GcmClient.Register(androidContext, _pushSenderId);
		}

		protected override void LaunchUnregistrationProcess()
		{
			var androidContext = Mvx.Resolve<Context>();
			GcmClient.UnRegister(androidContext);
		}

		protected override string ParseRegistrationId(string registrationId)
			=> registrationId.Trim('\"');
	}
}