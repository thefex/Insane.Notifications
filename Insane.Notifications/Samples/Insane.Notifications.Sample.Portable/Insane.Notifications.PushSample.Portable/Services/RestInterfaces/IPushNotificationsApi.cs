using System;
using System.Threading;
using System.Threading.Tasks;
using Insane.Notifications.PushSample.Portable.Data.Push;
using Refit;

namespace Insane.Notifications.PushSample.Portable.Services.RestInterfaces
{
	public interface IPushNotificationsApi
	{
		[Delete("/push-notifications")]
		Task Unsubscribe([Body] UnsubscribeFromPushRequest request, CancellationToken cancellationToken = default(CancellationToken));

		[Post("/push-notifications")]
		Task<PushRegistrationResponse> Subscribe([Body] SubscribeToPushRequest request, CancellationToken cancellationToken = default(CancellationToken));

		[Put("/push-notifications")]
		Task UpdateSubscription([Body] UpdatePushSubscriptionRequest request, CancellationToken cancellationToken = default(CancellationToken));

		[Put("/push-notifications/modify-tags")]
		Task ModifyTags([Body] ModifyPushTagsRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}
}
