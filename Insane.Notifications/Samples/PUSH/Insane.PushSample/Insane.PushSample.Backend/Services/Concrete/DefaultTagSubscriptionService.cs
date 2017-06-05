using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Insane.PushSample.Backend.Services.Abstract;
using Insane.PushSample.Backend.Utilities;

namespace Insane.PushSample.Backend.Services.Concrete
{
    public class DefaultTagSubscriptionService : TagSubscriptionService
    {
        protected override IEnumerable<string> GetAvailableTags()
        {
            var tags = base.GetAvailableTags().ToList();
            tags.Add("new_message");
            tags.Add("new_product");
            tags.Add("some_notification");

            return tags;
        }

        public override Task<LayerResponse> ValidateTags(IEnumerable<string> tagsToSubscribe, string forUserName)
        {
            var tagAccessValidation = ValidateDoesUserHasAccessTo(tagsToSubscribe, forUserName);

            return Task.FromResult(tagAccessValidation);
        }

        private LayerResponse ValidateDoesUserHasAccessTo(IEnumerable<string> providedTags, string forUsername)
        {
            var availableTagsForUser = GetAllAvailableTags(forUsername);

            var invalidProvidedTags = providedTags.Where(tagToCheck => !availableTagsForUser.Contains(tagToCheck));

            LayerResponse validationResponse = LayerResponse.Build();

            foreach (var invalidTag in invalidProvidedTags)
                validationResponse.AddErrorMessage($"User does not have access to: {invalidTag}");

            return validationResponse;
        }
    }
}