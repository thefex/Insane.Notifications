using System;
using System.Collections.Generic;
using System.Linq;

namespace MvvmCross.Plugins.Notifications.Data
{
    public class ServiceResponse
    {
        public ServiceResponse()
        {
            ErrorMessages = new List<string>();
        }

        public bool IsSuccess => !ErrorMessages.Any();

        public IList<string> ErrorMessages { get; }

        public string FormattedErrorMessage
        {
            get
            {
                return ErrorMessages.Any()
                    ? ErrorMessages.Aggregate((prev, current) => prev + Environment.NewLine + current)
                    : string.Empty;
            }
        }

        public ServiceResponse AddErrorMessage(string errorMessage)
        {
            ErrorMessages.Add(errorMessage);
            return this;
        }

        public static ServiceResponse Build()
        {
            return new ServiceResponse();
        }
    }

    public sealed class ServiceResponse<TResult> : ServiceResponse
    {
        public ServiceResponse()
        {
        }

        public TResult Result { get; private set; }

        public new ServiceResponse<TResult> AddErrorMessage(string errorMsg)
        {
            base.AddErrorMessage(errorMsg);
            return this;
        }

        public ServiceResponse BuildResponseWithoutResult()
        {
            var newLayerResponse = Build();
            foreach (var errorMessage in ErrorMessages)
                newLayerResponse.AddErrorMessage(errorMessage);

            return newLayerResponse;
        }

        public static ServiceResponse<TResult> Build(TResult result)
        {
            return new ServiceResponse<TResult> {Result = result};
        }

        public static ServiceResponse<TResult> BuildEmptyServiceResponse()
        {
            return new ServiceResponse<TResult>();
        }
    }
}