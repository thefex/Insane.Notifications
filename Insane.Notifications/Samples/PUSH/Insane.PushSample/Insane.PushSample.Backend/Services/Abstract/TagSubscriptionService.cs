using System.Collections.Generic;
using System.Threading.Tasks;
using Insane.PushSample.Backend.Utilities;

namespace Insane.PushSample.Backend.Services.Abstract
{
    public abstract class TagSubscriptionService
    {
        /// <summary>
        /// Make sure that tags can not be "matched" with username (create tag names which are in contradiction to user names rules)
        /// </summary>
        /// <returns></returns>
        public HashSet<string> GetDefaultTags(string uniqueUserId)
        {
            var tags = new HashSet<string>
            {
                uniqueUserId
            };

            foreach (var defaultTag in GetDefaultTags())
                tags.Add(defaultTag);

            return tags;
        }

        protected virtual IEnumerable<string> GetDefaultTags()
        {
            return new[] { "test" };
        }

        public HashSet<string> GetAllAvailableTags(string forUserName)
        {
            var availableTags = new HashSet<string>(GetAvailableTags());
            var defaultTags = GetDefaultTags(forUserName);

            availableTags.UnionWith(defaultTags);
            return availableTags;
        }

        /// <summary>
        /// Make sure that tags can not be "matched" with username (create tag names which are in contradiction to user names rules)
        /// </summary>
        /// <returns></returns>
        protected virtual IEnumerable<string> GetAvailableTags()
        {
            return new[] {"test"};
        }

        public abstract Task<LayerResponse> ValidateTags(IEnumerable<string> tagsToSubscribe, string forUserName);
    }
}