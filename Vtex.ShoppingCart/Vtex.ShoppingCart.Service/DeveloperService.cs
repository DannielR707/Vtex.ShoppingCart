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

                int stars = countRepostoriesStars(repositories);

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

     

        public int countRepostoriesStars(IReadOnlyList<Repository> repositories)
        {
            int stars = 0;
            
            foreach (var repository in repositories)
            {
                stars = stars + repository.StargazersCount;
            }

            return stars;
        }

        
        public int countRepostoriesCommits(GitHubClient client, IReadOnlyList<Repository> repositories, string userLogin)
        {
            int commits = 0;
            
            foreach (var repository in repositories)
            {
                var commitsCount = countRepositoryCommits(client, repository.Id, userLogin);
            //    commits = commits + commitsCount;
            }

            return commits;
        }

        public async Task<int> countRepositoryCommits(GitHubClient client, long repositoryId, string userLogin)
        {
            int commitsCount = 0;
            var commits = await client.Repository.Commit.GetAll(repositoryId);

        //    commitsCount = commits.Count();
           
            return commitsCount;

        }

        private static async Task<int> getCommitsByUser(GitHubClient client, string userLogin)
        {
            var commitsCount = 0;
            var repositories = await client.Repository.GetAllForUser(userLogin);
            foreach (var repository in repositories)
            {
                var commitsList = await client.Repository.Commit.GetAll(userLogin, repository.Name);
                commitsCount += commitsList.Count;
            }
            return commitsCount;
        }
    }
}
