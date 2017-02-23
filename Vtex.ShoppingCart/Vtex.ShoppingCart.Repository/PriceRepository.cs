using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vtex.ShoppingCart.Repo.Interface;

namespace Vtex.ShoppingCart.Repo
{
    public class PriceRepository : IPriceRepository
    {
        private Dictionary<PriceKeys, decimal> _pricesDatabase;

        public PriceRepository()
        {
            _pricesDatabase = new Dictionary<PriceKeys, decimal>();
            _pricesDatabase.Add(PriceKeys.Base_Price, 50m);
            _pricesDatabase.Add(PriceKeys.Followers_Bonus_Price, 3m);
            _pricesDatabase.Add(PriceKeys.Public_Repositories_Bonus_Price, 5m);
            _pricesDatabase.Add(PriceKeys.Stars_Bonus_Price, 10m);

        }

        public decimal GetPrice(PriceKeys key)
        {
            return _pricesDatabase[key];
        }
    }
}
