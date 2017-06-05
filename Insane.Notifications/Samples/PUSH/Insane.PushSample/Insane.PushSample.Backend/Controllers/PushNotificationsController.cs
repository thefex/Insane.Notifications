using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Insane.PushSample.Backend.Extensions;
using Insane.PushSample.Backend.Models;
using Insane.PushSample.Backend.Models.ResponseModels;
using Insane.PushSample.Backend.Services;
using Insane.PushSample.Backend.Services.Abstract;
using Microsoft.Azure.NotificationHubs;
using Microsoft.Azure.NotificationHubs.Messaging;

namespace Insane.PushSample.Backend.Controllers
{

    public class PushNotificationsController : ApiController
    {
        private readonly NotificationHubClient _notificationHubClient;
        private readonly TagSubscriptionService _tagSubscriptionService;

        public PushNotificationsController(NotificationHubRegistrationServices notificationHubRegistrationServices)
        {
            _notificationHubClient = notificationHubRegistrationServices.NotificationHub;
            _tagSubscriptionService = notificationHubRegistrationServices.TagSubscriptionService;
        }

        [HttpPost]
        [Route("api/push-notifications/")]
        [ResponseType(typeof(RegistrationIdResponse))]
        public async Task<IHttpActionResult> Register(RegistrationRequestData registrationRequest)
        {
            var registrationId = await _notificationHubClient.CreateRegistrationIdAsync().ConfigureAwait(false);
            return Ok(new RegistrationIdResponse(){RegistrationId = registrationId});
        }

        [HttpPut]
        [Route("api/push-notifications/")]
        public async Task<IHttpActionResult> UpdateRegistration(DeviceRegistrationUpdate deviceRegistrationUpdate)
        {
            var uniqueUserId = User?.Identity?.Name ?? Guid.NewGuid().ToString("d"); //User.Identity.Name; //Guid.NewGuid().ToString("d");
            var tagValidationResponse =
                await _tagSubscriptionService.ValidateTags(deviceRegistrationUpdate.Tags, uniqueUserId);

            if (!tagValidationResponse.IsSuccess)
                return this.BuildHttpActionResult(tagValidationResponse);

            var registrationDescription =
                deviceRegistrationUpdate.Platform.Value.GetRegistrationDescription(deviceRegistrationUpdate.Handle);
            registrationDescription.RegistrationId = deviceRegistrationUpdate.Id;
            registrationDescription.Tags = _tagSubscriptionService.GetDefaultTags(uniqueUserId);

            try
            {
                var result = await _notificationHubClient.CreateOrUpdateRegistrationAsync(registrationDescription);
                return Ok();
            }
            catch (MessagingException e)
            {
                if (HasRegistrationIdGone(e))
                    throw new HttpRequestException(HttpStatusCode.Gone.ToString());

                throw;
            }
        }

        private bool HasRegistrationIdGone(MessagingException e)
        {
            var webex = e.InnerException as WebException;
            if (webex == null || webex.Status != WebExceptionStatus.ProtocolError)
                return false;

            var response = (HttpWebResponse)webex.Response;
            return response.StatusCode == HttpStatusCode.Gone;
        }

        [HttpDelete]
        [Route("api/push-notifications/")]
        public async Task<IHttpActionResult> Delete(DeleteDeviceRegistration deleteDeviceRegistration)
        {
            await _notificationHubClient.DeleteRegistrationAsync(deleteDeviceRegistration.Id);
            return Ok();
        }

        [HttpPut]
        [Route("api/push-notifications/modify-tags")]
        public async Task<IHttpActionResult> ModifyTags(ModifyTagsRequest modifyTagsRequest)
        {
            var uniqueUserId = User?.Identity?.Name ?? Guid.NewGuid().ToString("d");//User.Identity.Name; // modifyTagsRequest.UserId ?? Guid.NewGuid().ToString("d"); 
            var newTags = _tagSubscriptionService.GetDefaultTags(uniqueUserId).Union(modifyTagsRequest.Tags).ToList();
            var validationResponse = await _tagSubscriptionService.ValidateTags(newTags, uniqueUserId);

            if (!validationResponse.IsSuccess)
                return this.BuildHttpActionResult(validationResponse);

            var userHubRegistration =
                (await _notificationHubClient.GetRegistrationsByTagAsync(uniqueUserId, 1)).Single();

            userHubRegistration.Tags = new HashSet<string>(newTags);
            await _notificationHubClient.UpdateRegistrationAsync(userHubRegistration);

            return Ok();
        }

        [HttpPost]
        [Route("api/push")]
        public async Task<IHttpActionResult> SendPushNotification(SendPushNotificationRequest request)
        {
            var notificationModel = "{ \"data\" : {\"message\":\"" + "From " + request.Title + ": " + request.Content + "\"}}";
            NotificationOutcome outcome = null;

            switch (request.Platform)
            {
                case PushPlatformType.WNS:
                    // Windows 8.1 / Windows Phone 8.1
                    var toast = @"<toast><visual><binding template=""ToastText01""><text id=""1"">" +
                                "From " + request.Title + ": " + request.Content + "</text></binding></visual></toast>";
                    outcome = await _notificationHubClient.SendWindowsNativeNotificationAsync(toast, request.Tag);
                    break;
                case PushPlatformType.APNS:
                    // iOS
                    var alert = "{\"aps\":{\"alert\":\"" + "From " + request.Title + ": " + request.Content + "\"}}";
                    outcome = await _notificationHubClient.SendAppleNativeNotificationAsync(alert, request.Tag);
                    break;
                case PushPlatformType.GCM:
                    // Android
                    var notif = "{ \"data\" : {\"message\":\"" + "From " + request.Title + ": " + request.Content + "\"}}";
                    outcome = await _notificationHubClient.SendGcmNativeNotificationAsync(notif, request.Tag);
                    break;
            }

            if (outcome != null)
            {
                if (!((outcome.State == NotificationOutcomeState.Abandoned) ||
                    (outcome.State == NotificationOutcomeState.Unknown)))
                {
                    return Ok();
                }
            }

            return InternalServerError();
        }
    }
}
