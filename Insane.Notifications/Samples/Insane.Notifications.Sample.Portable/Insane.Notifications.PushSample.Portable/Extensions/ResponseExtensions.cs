using System;
using Insane.Notifications.Data;
using Insane.Notifications.PushSample.Portable.Data;

namespace Insane.Notifications.PushSample.Portable.Extensions
{
    public static class ResponseExtensions
    {
        public static ServiceResponse ToServiceResponse(this Response response){
            ServiceResponse serviceResponse = new ServiceResponse();

            if (!response.IsSuccess){
                foreach (var errorMsg in response.ErrorMessages)
                    serviceResponse.AddErrorMessage(errorMsg);
            }

            return serviceResponse;
        }

        public static ServiceResponse<TResult> ToServiceResponse<TResult>(this Response<TResult> result){
            if (result.IsSuccess)
                return ServiceResponse<TResult>.Build(result.Results);

            var serviceResponse = new ServiceResponse<TResult>();
            foreach (var errorMsg in result.ErrorMessages)
                serviceResponse.AddErrorMessage(errorMsg);

            return serviceResponse;
        }
    }
}
