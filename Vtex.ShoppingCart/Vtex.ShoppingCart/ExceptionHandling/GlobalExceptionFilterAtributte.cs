using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;
using Vtex.ShoppingCart.Helpers;

namespace Vtex.ShoppingCart.ExceptionHandling
{
    public class GlobalExceptionFilterAtributte : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            {
                HttpResponseMessage message = context.Exception.ConstructHttpResponseMessage();
                throw new HttpResponseException(message);
            }
        }
    }
}