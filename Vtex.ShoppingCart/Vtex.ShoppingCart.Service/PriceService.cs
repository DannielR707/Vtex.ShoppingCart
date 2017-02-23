using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vtex.ShoppingCart.Service.Interface;
using Vtex.ShoppingCart.Service.Models;
using Vtex.ShoppingCart.Repo;
using Vtex.ShoppingCart.Repo.Interface;

namespace Vtex.ShoppingCart.Service
{
    public class PriceService : IPriceService
    {
        IPriceRepository _priceRepository;

        public PriceService(IPriceRepository priceRepository)
        {
            _priceRepository = priceRepository;
        }

        public decimal CalculateDeveloperPrice(PricingCriteria criteria)
        {
            decimal totalPrice = _priceRepository.GetPrice(PriceKeys.Base_Price);

            totalPrice += criteria.Followers * _priceRepository.GetPrice(PriceKeys.Followers_Bonus_Price);
            totalPrice += criteria.Stars * _priceRepository.GetPrice(PriceKeys.Public_Repositories_Bonus_Price);
            totalPrice += criteria.PublicRepositories * _priceRepository.GetPrice(PriceKeys.Stars_Bonus_Price);

            return totalPrice;
        }
    }
}
