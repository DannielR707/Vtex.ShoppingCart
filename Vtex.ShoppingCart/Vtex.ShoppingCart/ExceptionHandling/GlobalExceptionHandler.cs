using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Vtex.ShoppingCart.Helpers;

namespace Vtex.ShoppingCart.ExceptionHandling
{
    public class GlobalExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            context.Result = new ExceptionResult(context.Exception);
        }

        public class ExceptionResult : IHttpActionResult
        {
            public ExceptionResult(Exception exception)
            {
                if (exception == null)
                {
                    throw new ArgumentNullException("Exception");
                }
                _exception = exception;
            }

            private Exception _exception;
            public Exception Exception { get { return _exception; } }

            public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
            {
                return Task.FromResult(Execute());
            }

            private HttpResponseMessage Execute()
            {
                return _exception.ConstructHttpResponseMessage();
            }
        }
    }
}
