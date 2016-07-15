using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Azure_DocumentDB_WebApiApp.Helpers
{
    public class NoContentActionResult : IHttpActionResult
    {
        public string Message { get; private set; }
        public HttpRequestMessage Request { get; private set; }

        public NoContentActionResult(HttpRequestMessage request)
        {
            this.Request = request;
            this.Message = "Ok";
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
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NoContent);
            response.Content = new StringContent(Message);
            response.RequestMessage = Request;
            return response;
        }
    }
}