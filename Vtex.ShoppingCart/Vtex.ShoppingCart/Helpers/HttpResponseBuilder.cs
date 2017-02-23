using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Vtex.ShoppingCart.Helpers
{
    public static class HttpResponseBuilder
    {
        public static HttpResponseMessage ConstructHttpResponseMessage(this Exception exception)
        {
            var resp = new HttpResponseMessage(exception.ConvertToHttpStatusCode())
            {
                Content = new StringContent(exception.Message),
                ReasonPhrase = exception.GetType().ToString()
            };
            return resp;
        }
    }
}