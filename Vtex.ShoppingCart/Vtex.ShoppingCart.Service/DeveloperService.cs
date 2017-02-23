using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vtex.ShoppingCart.Service.Models;
using Octokit;
using Vtex.ShoppingCart.Service.Interface;

namespace Vtex.ShoppingCart.Service
{
    public class DeveloperService : IDeveloperService
    {
        private IGitHubClient _client;
        private IPriceService _priceService;

        public DeveloperService(IGitHubClient client, IPriceService priceService)
        {
            _client = client;
            _priceService = priceService;
        }
        
        public async Task<IEnumerable<Developer>> GetDevelopersByOrganization(String organizationName)
        {                      
            var members = await _client.Organization.Member.GetAll(organizationName);

            List<Developer> developers = new List<Developer>();

            foreach (var member in members)
            {                
                var user = await _client.User.Get(member.Login);
                var repositories = await _client.Repository.GetAllForUser(member.Login);

                int stars = countStarsInRepositories(repositories);

                decimal developerPrice = _priceService.CalculateDeveloperPrice(new PricingCriteria()
                {
                    Followers = user.Followers,
                    PublicRepositories = user.PublicRepos,
                    Stars = stars,
                });

                  developers.Add(new Developer()
                {
                    Name = user.Name == null ? user.Login : user.Name,
                    Avatar = user.AvatarUrl,            
                    Price = developerPrice,

                });

            }

            return developers;
        }

     

        public int countStarsInRepositories(IReadOnlyList<Repository> repositories)
        {
            int stars = 0;
            foreach (var repository in repositories)
            {
                stars += repository.StargazersCount;
            }
            return stars;
        }

    }
}
