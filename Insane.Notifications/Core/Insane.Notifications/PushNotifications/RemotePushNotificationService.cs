﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Insane.Notifications.CachedStorage;
using Insane.Notifications.Data;

namespace Insane.Notifications.PushNotifications
{
    public abstract class RemotePushNotificationService : INotificationsService
    {
        private const string StoragePushDeviceRegistrationIdKey = "PushNotificationsServiceDeviceIdKey";
        private readonly IRemotePushRegistrationService _remotePushRegistrationService;
        private readonly IPersistedStorage _persistedStorage;
        private readonly IPushTagsProvider _pushTagsProvider;

		private readonly Semaphore _synchronizationSemaphore = new Semaphore(1, 1);

        private TaskCompletionSource<ServiceResponse<string>> _registerPushTcs;
        private TaskCompletionSource<ServiceResponse> _unregisterPushTcs;

        protected RemotePushNotificationService(IPersistedStorage persistedStorage,
            IRemotePushRegistrationService remotePushRegistrationService, IPushTagsProvider pushTagsProvider)
        {
            _persistedStorage = persistedStorage;
            _remotePushRegistrationService = remotePushRegistrationService;
            _pushTagsProvider = pushTagsProvider;
        }

        protected abstract PushPlatformType PlatformType { get; }

        protected abstract bool IsUserRegisteredToPushService { get; }

        public bool IsSubscribedToNotifications => IsUserRegisteredToPushService;

        public async Task<ServiceResponse> SubscribeToNotifications(bool forceSubscribe = false)
        {
			try
			{
				_synchronizationSemaphore.WaitOne();

				if (IsUserRegisteredToPushService && !forceSubscribe)
					return new ServiceResponse();

				_registerPushTcs = new TaskCompletionSource<ServiceResponse<string>>();
				var registrationLaunchProcessResponse = await LaunchRegistrationProcess(forceSubscribe);
                if (!registrationLaunchProcessResponse.IsSuccess)
                    return registrationLaunchProcessResponse;

			    try
			    {
                    var deviceRegistrationHandleResponse = await _registerPushTcs.Task;
			        if (!deviceRegistrationHandleResponse.IsSuccess)
			        {
			            await LaunchUnregistrationProcess();
			            return deviceRegistrationHandleResponse;
			        }

                    var subscribeResponse = await SubscribeToPushNotifications(deviceRegistrationHandleResponse.Result);

			        if (!subscribeResponse.IsSuccess)
			        {
			            await LaunchUnregistrationProcess();
			            return subscribeResponse;
			        }

			        return subscribeResponse;
			    }
			    catch (Exception)
			    {
			        await LaunchUnregistrationProcess();
			        throw;
			    }
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

                _unregisterPushTcs = new TaskCompletionSource<ServiceResponse>();
                var unregistrationLaunchProcessResponse = await LaunchUnregistrationProcess();
                if (!unregistrationLaunchProcessResponse.IsSuccess)
                    return unregistrationLaunchProcessResponse;

                await _unregisterPushTcs.Task;
                return await UnsubscribeFromPushNotifications();
            }
            finally
            {
                _unregisterPushTcs = null;
                _synchronizationSemaphore.Release();
            }
        }

		protected abstract Task<ServiceResponse> LaunchRegistrationProcess(bool forceSubscribe=false);

        protected abstract Task<ServiceResponse> LaunchUnregistrationProcess();

        protected async Task<ServiceResponse> SubscribeToPushNotifications(string deviceHandle)
        {
            var getDeviceRegistrationIdResponse = await GetDeviceRegistrationId(deviceHandle);

            if (!getDeviceRegistrationIdResponse.IsSuccess)
                return getDeviceRegistrationIdResponse;

            var subscribeToPushResponse =
                await
                UpdateDeviceRegistration(getDeviceRegistrationIdResponse.Result, deviceHandle);

            if (subscribeToPushResponse.IsSuccess)
                return subscribeToPushResponse;

            var newDeviceRegistrationId =
                await GetDeviceRegistrationId(deviceHandle, true);

            subscribeToPushResponse =
                await UpdateDeviceRegistration(newDeviceRegistrationId.Result, deviceHandle);

            return subscribeToPushResponse;
        }

        protected Task<ServiceResponse> UnsubscribeFromPushNotifications()
        {
            var hasUserRegisteredToPush = _persistedStorage.Has(StoragePushDeviceRegistrationIdKey);

            if (!hasUserRegisteredToPush)
                return Task.FromResult(
                    new ServiceResponse().AddErrorMessage(ErrorMessagesConstants.UserNotRegisteredToPush)
                    );

            var deviceRegistrationId =
                ParseRegistrationId(_persistedStorage.Get<string>(StoragePushDeviceRegistrationIdKey));
            return _remotePushRegistrationService.UnsubscribeFromPush(deviceRegistrationId);
        }

        private async Task<ServiceResponse<string>> GetDeviceRegistrationId(string deviceHandle,
            bool forceReplaceCacheValue = false)
        {
            var hasDeviceRegistrationId = _persistedStorage.Has(StoragePushDeviceRegistrationIdKey);

            if (!hasDeviceRegistrationId || forceReplaceCacheValue)
            {
                var deviceRegistrationIdResponse =
                    await _remotePushRegistrationService.RegisterDevice(deviceHandle);

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
                PushHandle = ParsePushHandle(handle),
                TagsToRegisterIn = _pushTagsProvider.ActivePushTags
            };

            return _remotePushRegistrationService.UpdateDeviceRegistration(subscribeToPushRequest);
        }

        protected abstract string ParseRegistrationId(string registrationId);

        protected virtual string ParsePushHandle(string pushHandle) => pushHandle.Trim(' ', '<', '>').Replace(" ", "");

        public void NotifyThatRegistrationSucceed(string result) => _registerPushTcs?.SetResult(ServiceResponse<string>.Build(result));

        public void NotifyThatRegistrationFailed(Exception withException)
            => _registerPushTcs?.SetException(withException);

        public void NotifyThatRegistrationFailed(string errorMsg) => _registerPushTcs?.SetResult(
            new ServiceResponse<string>().AddErrorMessage(errorMsg));

        public void NotifyThatUnregistrationSucceed()
            => _unregisterPushTcs?.SetResult(new ServiceResponse());

        public void NotifyThatUnregistrationFailed(Exception withException)
            => _unregisterPushTcs?.SetException(withException);

        public void NotifyThatUnregistrationFailed(string errorMsg)
            => _unregisterPushTcs?.SetResult(new ServiceResponse().AddErrorMessage(errorMsg));
    }
}