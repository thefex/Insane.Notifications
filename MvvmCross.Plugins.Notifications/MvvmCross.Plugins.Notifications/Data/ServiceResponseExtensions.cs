using System;

namespace MvvmCross.Plugins.Notifications.Data
{
	public static class ServiceResponseExtensions
	{
		public static ServiceResponse CloneFailedResponse(this ServiceResponse layerResponse)
		{
			var clonedResponse = ServiceResponse.Build();

			foreach (var error in layerResponse.ErrorMessages)
				clonedResponse.AddErrorMessage(error);

			return clonedResponse;
		}

		public static ServiceResponse<TResult> CloneFailedResponse<TResult>(this ServiceResponse layerResponse)
		{
			if (layerResponse.IsSuccess)
				throw new InvalidOperationException("Can't clone success response with result!");

			var clonedResponse = ServiceResponse<TResult>.BuildEmptyLayerResponse();

			foreach (var error in layerResponse.ErrorMessages)
				clonedResponse.AddErrorMessage(error);

			return clonedResponse;
		}
	}
}
