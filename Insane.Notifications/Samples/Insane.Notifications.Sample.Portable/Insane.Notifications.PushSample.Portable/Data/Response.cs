using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Insane.Notifications.PushSample.Portable.Data
{
    public class Response
	{
		private bool _forceFailedResponse;

		public Response()
		{
			ErrorMessages = new List<string>();
		}

		[JsonIgnore]
		public bool IsSuccess => !ErrorMessages.Any() && !_forceFailedResponse;

		[JsonProperty("errorMessages")]
		public IList<string> ErrorMessages { get; set; }

		[JsonIgnore]
		public string FormattedErrorMessages => ErrorMessages.Any()
			? ErrorMessages.Aggregate((prev, current) => prev + Environment.NewLine + current)
			: string.Empty;


		public Response AddErrorMessage(string errorMsg)
		{
			ErrorMessages.Add(errorMsg);
			return this;
		}

		public Response SetAsFailureResponse()
		{
			_forceFailedResponse = true;
			return this;
		}
	}

	public sealed class Response<TResult> : Response
	{
		public Response(TResult results)
		{
			Results = results;
		}

		public Response()
		{
		}

		public TResult Results { get; }

		public new Response<TResult> AddErrorMessage(string errorMsg)
		{
			base.AddErrorMessage(errorMsg);
			return this;
		}

		public new Response<TResult> SetAsFailureResponse()
		{
			base.SetAsFailureResponse();
			return this;
		}
	}
}
