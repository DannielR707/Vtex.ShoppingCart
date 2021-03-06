﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vtex.ShoppingCart.Service.Models;

namespace Vtex.ShoppingCart.Service.Interface
{
    public interface IDeveloperService
    {
        Task<IEnumerable<Developer>> GetDevelopersByOrganization(String organizationName);
    }
}
