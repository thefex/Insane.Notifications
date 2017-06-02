using System;

namespace MvvmCross.Plugins.Notifications.Exceptions
{
    public class PushDeregistrationFailedException : InvalidOperationException
    {
        internal PushDeregistrationFailedException()
        {
        }

        internal PushDeregistrationFailedException(string message) : base(message)
        {
        }

        internal PushDeregistrationFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}