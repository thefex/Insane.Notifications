using System.Collections.Generic;

namespace Insane.PushSample.Backend.Utilities
{
    public class ErrorResponse
    {
        public ErrorResponse(IEnumerable<string> errorMessages)
        {
            ErrorMessages = errorMessages;
        }

        public IEnumerable<string> ErrorMessages { get; }
    }
}