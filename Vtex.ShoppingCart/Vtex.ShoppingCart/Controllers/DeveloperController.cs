using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Vtex.ShoppingCart.Service;
using Vtex.ShoppingCart.Service.Interface;
using Vtex.ShoppingCart.Service.Models;

namespace Vtex.ShoppingCart.Controllers
{
    [Authorize]
    public class DeveloperController : ApiController
    {
        private IDeveloperService _developerService;

        public DeveloperController(IDeveloperService developerService)
        {
            _developerService = developerService;
        }
        
        [AllowAnonymous]
        public async Task<IEnumerable<Developer>> GetDevelopersByOrganization(String organizationName)
        {           
            return await _developerService.GetDevelopersByOrganization(organizationName);
        }
    }
}
