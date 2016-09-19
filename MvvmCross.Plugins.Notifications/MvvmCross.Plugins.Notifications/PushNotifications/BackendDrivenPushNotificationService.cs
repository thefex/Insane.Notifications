using System;
using System.Threading;
using System.Threading.Tasks;
using MvvmCross.Plugins.Notifications.CachedStorage;
using MvvmCross.Plugins.Notifications.Data;

namespace MvvmCross.Plugins.Notifications.PushNotifications
{
	public abstract class BackendDrivenPushNotificationService : INotificationsService
	{
		private const string StoragePushDeviceRegistrationIdKey = "PushNotificationsServiceDeviceIdKey";
		private readonly IBackendPushRegistrationService _backendPushRegistrationService;
		private readonly IPersistedStorage _persistedStorage;
		private readonly IPushTagsProvider _pushTagsProvider;

		private readonly Semaphore _synchronizationSemaphore = new Semaphore(1, 1);

		private TaskCompletionSource<string> _registerPushTcs;
		private TaskCompletionSource<bool> _unregisterPushTcs;

		protected BackendDrivenPushNotificationService(IPersistedStorage persistedStorage,
			IBackendPushRegistrationService backendPushRegistrationService, IPushTagsProvider pushTagsProvider)
		{
			_persistedStorage = persistedStorage;
			_backendPushRegistrationService = backendPushRegistrationService;
			_pushTagsProvider = pushTagsProvider;
		}

		protected abstract PushPlatformType PlatformType { get; }

		protected abstract bool IsUserRegisteredToPushService { get; }

		public async Task<ServiceResponse> SubscribeToNotifications()
		{
			try
			{
				_synchronizationSemaphore.WaitOne();

				if (IsUserRegisteredToPushService)
					return new ServiceResponse();

				_registerPushTcs = new TaskCompletionSource<string>();
				LaunchRegistrationProcess();
				var deviceRegistrationHandle = await _registerPushTcs.Task.ConfigureAwait(false);
				return await SubscribeToPushNotifications(deviceRegistrationHandle).ConfigureAwait(false);
			}
			finally
			{
				_registerPushTcs = null;
				_synchronizationSemaphore.Release();
			}
		}

		public async Task<ServiceResponse> UnsubscribeFromNotifications()
		{
			try
			{
				_synchronizationSemaphore.WaitOne();
				if (!IsUserRegisteredToPushService)
					return ServiceResponse.Build();

				_unregisterPushTcs = new TaskCompletionSource<bool>();
				LaunchUnregistrationProcess();

				await _unregisterPushTcs.Task.ConfigureAwait(false);
				return await UnsubscribeFromPushNotifications().ConfigureAwait(false);
			}
			finally
			{
				_unregisterPushTcs = null;
				_synchronizationSemaphore.Release();
			}
		}

		protected abstract void LaunchRegistrationProcess();

		protected abstract void LaunchUnregistrationProcess();

		protected async Task<ServiceResponse> SubscribeToPushNotifications(string deviceHandle)
		{
			var getDeviceRegistrationIdResponse = await GetDeviceRegistrationId(deviceHandle).ConfigureAwait(false);

			if (!getDeviceRegistrationIdResponse.IsSuccess)
				return getDeviceRegistrationIdResponse;

			var subscribeToPushResponse =
				await
					UpdateDeviceRegistration(getDeviceRegistrationIdResponse.Result, deviceHandle)
						.ConfigureAwait(false);

			if (subscribeToPushResponse.IsSuccess)
				return subscribeToPushResponse;

			var newDeviceRegistrationId =
				await GetDeviceRegistrationId(deviceHandle, true)
					.ConfigureAwait(false);
			subscribeToPushResponse =
				await UpdateDeviceRegistration(newDeviceRegistrationId.Result, deviceHandle)
					.ConfigureAwait(false);

			return subscribeToPushResponse;
		}

		protected Task<ServiceResponse> UnsubscribeFromPushNotifications()
		{
			var hasUserRegisteredToPush = _persistedStorage.Has(StoragePushDeviceRegistrationIdKey);

			if (!hasUserRegisteredToPush)
				return Task.FromResult(
					new ServiceResponse().AddErrorMessage(ErrorMessagesConstants.UserNotRegisteredToPush)
				);

			var deviceRegistrationId = ParseRegistrationId(_persistedStorage.Get<string>(StoragePushDeviceRegistrationIdKey));
			return _backendPushRegistrationService.UnsubscribeFromPush(deviceRegistrationId);
		}

		private async Task<ServiceResponse<string>> GetDeviceRegistrationId(string deviceHandle,
			bool forceReplaceCacheValue = false)
		{
			var hasDeviceRegistrationId = _persistedStorage.Has(StoragePushDeviceRegistrationIdKey);

			if (!hasDeviceRegistrationId || forceReplaceCacheValue)
			{
				var deviceRegistrationIdResponse =
					await _backendPushRegistrationService.RegisterDevice(deviceHandle).ConfigureAwait(false);

				if (!deviceRegistrationIdResponse.IsSuccess)
					return deviceRegistrationIdResponse;

				var parsedRegistrationId = ParseRegistrationId(deviceRegistrationIdResponse.Result);
				_persistedStorage.SaveOrUpdate(StoragePushDeviceRegistrationIdKey, parsedRegistrationId);
			}

			var registrationId = _persistedStorage.Get<string>(StoragePushDeviceRegistrationIdKey);
			return ServiceResponse<string>.Build(ParseRegistrationId(registrationId));
		}

		private Task<ServiceResponse> UpdateDeviceRegistration(string deviceId, string handle)
		{
			var subscribeToPushRequest = new PushSubscribeDetails
			{
				PushPlatformType = PlatformType,
				DeviceId = deviceId,
				PushHandle = handle,
				TagsToRegisterIn = _pushTagsProvider.ActivePushTags
			};

			return _backendPushRegistrationService.UpdateDeviceRegistration(subscribeToPushRequest);
		}

		protected abstract string ParseRegistrationId(string registrationId);

		internal void NotifyThatRegistrationSucceed(string result) => _registerPushTcs?.SetResult(result);

		internal void NotifyThatRegistrationFailed(Exception withException) => _registerPushTcs?.SetException(withException);

		internal void NotifyThatUnregistrationSucceed() => _unregisterPushTcs?.SetResult(true);

		internal void NotifyThatUnregistrationFailed(Exception withException)
			=> _unregisterPushTcs?.SetException(withException);
	}
}