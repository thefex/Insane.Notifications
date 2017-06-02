using System;

namespace Insane.Notifications.Exceptions
{
    public class PushRegistrationFailedException : InvalidOperationException
    {
        internal PushRegistrationFailedException()
        {
        }

        internal PushRegistrationFailedException(string message) : base(message)
        {
        }

        internal PushRegistrationFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}