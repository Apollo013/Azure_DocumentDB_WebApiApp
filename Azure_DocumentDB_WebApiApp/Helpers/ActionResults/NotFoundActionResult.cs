using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Azure_DocumentDB_WebApiApp.Helpers.ActionResults
{
    /// <summary>
    /// Custom Action Result That Overloads The NotFound() Action Result to return a plain text message within the response body.
    /// </summary>
    public class NotFoundActionResult : IHttpActionResult
    {
        public string Message { get; private set; }
        public HttpRequestMessage Request { get; private set; }

        public NotFoundActionResult(HttpRequestMessage request, string message)
        {
            this.Request = request;
            this.Message = message;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(ExecuteResult());
        }

        public Task<IHttpActionResult> ExecuteAsync()
        {
            return Task.FromResult<IHttpActionResult>(this);
        }

        public HttpResponseMessage ExecuteResult()
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NotFound);
            response.Content = new StringContent(Message);
            response.RequestMessage = Request;
            return response;
        }
    }
}