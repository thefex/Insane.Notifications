using System;
using System.Collections.Generic;
using System.Linq;

namespace Insane.PushSample.Backend.Utilities
{
    public class LayerResponse
    {
        public LayerResponse()
        {
            ErrorMessages = new List<string>();
        }

        public bool IsSuccess => !ErrorMessages.Any();

        public IList<string> ErrorMessages { get; private set; }

        public LayerResponse AddErrorMessage(string errorMessage)
        {
            ErrorMessages.Add(errorMessage);
            return this;
        }

        public string FormattedErrorMessage
        {
            get
            {
                return ErrorMessages.Any()
                    ? ErrorMessages.Aggregate((prev, current) => prev + Environment.NewLine + current)
                    : string.Empty;
            }
        }

        public static LayerResponse Build()
        {
            return new LayerResponse();
        }
    }

    public sealed class LayerResponse<TResult> : LayerResponse
    {
        private LayerResponse()
        {

        }

        public TResult Result { get; private set; }

        public new LayerResponse<TResult> AddErrorMessage(string errorMsg)
        {
            base.AddErrorMessage(errorMsg);
            return this;
        }

        public LayerResponse BuildResponseWithoutResult()
        {
            var newLayerResponse = Build();
            foreach (var errorMessage in ErrorMessages)
                newLayerResponse.AddErrorMessage(errorMessage);

            return newLayerResponse;
        }

        public static LayerResponse<TResult> Build(TResult result)
        {
            return new LayerResponse<TResult>() { Result = result };
        }

        public static LayerResponse<TResult> BuildEmptyLayerResponse()
        {
            return new LayerResponse<TResult>();
        }
    }
}