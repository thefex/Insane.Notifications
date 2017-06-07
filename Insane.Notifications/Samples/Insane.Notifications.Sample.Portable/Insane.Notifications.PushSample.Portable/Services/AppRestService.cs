using System;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Insane.Notifications.PushSample.Portable.Data;
using MvvmCross.Platform;
using Refit;

namespace Insane.Notifications.PushSample.Portable.Services
{
    public class AppRestService
    {
		private readonly Func<HttpClient> _httpClientFactory;

		public AppRestService() : this(Mvx.Resolve<HttpClient>)
        {

		}

		public AppRestService(Func<HttpClient> httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

        private Uri GetBaseUriFor<TApi>()
        {
			return new Uri(Constants.ApiPath);
		}

		public async Task<Response<TResult>> Execute<TApi, TResult>(Expression<Func<TApi, Task<TResult>>> executeApiMethod)
		{
			var httpClient = _httpClientFactory();
			var baseUri = GetBaseUriFor<TApi>();
			if (httpClient.BaseAddress != baseUri)
				httpClient.BaseAddress = baseUri;
			var restApi = RestService.For<TApi>(httpClient);

			try
			{
				var responseData = await executeApiMethod.Compile()(restApi).ConfigureAwait(false);
				return new Response<TResult>(responseData);
			}
			catch (ApiException refitApiException)
			{
				if (refitApiException.StatusCode == HttpStatusCode.Forbidden)
					return GetResponse<TResult>(refitApiException);

				throw;
			}
		}

		public async Task<Response> Execute<TApi>(Expression<Func<TApi, Task>> executeApiMethod)
		{
			var httpClient = _httpClientFactory();
			httpClient.BaseAddress = GetBaseUriFor<TApi>();
            var restApi = RestService.For<TApi>(httpClient);

			try
			{
				await executeApiMethod.Compile()(restApi).ConfigureAwait(false);
				return new Response();
			}
			catch (ApiException refitApiException)
			{
				if (refitApiException.StatusCode == HttpStatusCode.Forbidden)
					return GetResponse(refitApiException);

				throw;
			}
		}

		private Response<TResult> GetResponse<TResult>(ApiException fromApiException)
		{
			try
			{
				var response = fromApiException.GetContentAs<Response<TResult>>();
				return response.SetAsFailureResponse();
			}
			catch (Exception e)
			{
				throw fromApiException;
			}
		}

		private Response GetResponse(ApiException fromApiException)
		{
			try
			{
				var response = fromApiException.GetContentAs<Response>();
				return response.SetAsFailureResponse();
			}
			catch (Exception e)
			{
				throw fromApiException;
			}
		}
    }
}
