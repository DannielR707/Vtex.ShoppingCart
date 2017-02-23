using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Vtex.ShoppingCart.Service;
using Vtex.ShoppingCart.Service.Models;

namespace Vtex.ShoppingCart.Controllers
{
    [Authorize]
    public class ValuesController : ApiController
    {
       // GET api/values
       [AllowAnonymous]
        public async Task<IEnumerable<Developer>> Get()
        {
            String organizationName = "lincolnloop";

            DeveloperService userService = new DeveloperService();
            return await userService.getDevelopersByOrganization(organizationName, ConfigurationManager.AppSettings["GitHubAPIToken"]);
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
       
    }
}
