using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using Insane.PushSample.Backend.Utilities;
using Microsoft.AspNet.Identity;

namespace Insane.PushSample.Backend.Extensions
{
    public static class LayerResponseExtensions
    {
        public static IHttpActionResult BuildHttpActionResult(this ApiController controller, LayerResponse layerResponse)
        {
            if (!layerResponse.IsSuccess)
            {
                var forbiddenResponseMessage = controller.Request.CreateResponse(HttpStatusCode.Forbidden, new ErrorResponse(layerResponse.ErrorMessages));
                return new ResponseMessageResult(forbiddenResponseMessage);
            }

            return new OkResult(controller);
        }

        public static IHttpActionResult BuildHttpActionResult<TResult>(this ApiController controller, LayerResponse<TResult> layerResponse)
        {
            if (!layerResponse.IsSuccess)
            {
                var forbiddenResponseMessage = controller.Request.CreateResponse(HttpStatusCode.Forbidden, new ErrorResponse(layerResponse.ErrorMessages));
                return new ResponseMessageResult(forbiddenResponseMessage);
            }

            return new OkNegotiatedContentResult<TResult>(layerResponse.Result, controller);
        }

        public static LayerResponse BuildLayerResponse(this IdentityResult fromIdentityResult)
        {
            var identityLayerResponse = LayerResponse.Build();

            foreach (var error in fromIdentityResult.Errors)
                identityLayerResponse.AddErrorMessage(error);

            return identityLayerResponse;
        }
    }
}