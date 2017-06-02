using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace InsaneNotifications.UWP
{
    public static class LaunchArgumentSerializator
    {
        private const string DelimiterLaunchArgsString = "$INSANELAB_NOTIFICATIONS$";
        public static string SerializeLaunchArgument(string notificationId, object objectToSerialize)
        {
            var serializedObject = JsonConvert.SerializeObject(objectToSerialize);

            var serializedObjectBytes = System.Text.Encoding.UTF8.GetBytes(serializedObject);
            return $"{notificationId}{DelimiterLaunchArgsString}{System.Convert.ToBase64String(serializedObjectBytes)}";

        }

        public static bool IsPushServicesLaunchArgumentFormat(string launchArgument)
        {
            return launchArgument.IndexOf(DelimiterLaunchArgsString) != -1;
        }

        public static string GetLaunchId(string launchArgument)
        {
            var delimiterIndex = launchArgument.IndexOf(DelimiterLaunchArgsString);
            if (delimiterIndex == -1)
                throw new InvalidOperationException("It does not look like Launch Arguments are InsaneNotifications serialized arguments. Use " + nameof(LaunchArgumentSerializator) + " to serialize launch args before.");

            return launchArgument.Substring(0, delimiterIndex);
        }

        public static string GetLaunchArgumentJsonString(string launchArguments)
        {
            var delimiterIndex = launchArguments.IndexOf(DelimiterLaunchArgsString);
            if (delimiterIndex == -1)
                throw new InvalidOperationException("It does not look like Launch Arguments are InsaneNotifications serialized arguments. Use " + nameof(LaunchArgumentSerializator) + " to serialize launch args before.");

            var base64String = launchArguments.Substring(delimiterIndex + DelimiterLaunchArgsString.Length);

            var decodedBase64String = System.Convert.FromBase64String(base64String);
            var launchArgumentJson = System.Text.Encoding.UTF8.GetString(decodedBase64String);

            return launchArgumentJson;
        }
    }
}
