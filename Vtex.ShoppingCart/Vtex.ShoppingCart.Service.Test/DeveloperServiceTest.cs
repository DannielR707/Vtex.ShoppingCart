using Moq;
using NUnit.Framework;
using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vtex.ShoppingCart.Service.Interface;
using Vtex.ShoppingCart.Service.Models;

namespace Vtex.ShoppingCart.Service.Test
{
    [TestFixture]
    public class DeveloperServiceTest
    {
        Mock<IRepositoriesClient> _mockRepositories;
        Mock<IPriceService> _mockPriceService;
        Mock<IGitHubClient> _mockGitHubClient;
        Mock<IOrganizationsClient> _OrganizationsClient;
        Mock<IOrganizationMembersClient> _OrganizationMembersClient;
        Mock<IUsersClient> _UsersClient;

        [SetUp]
        public void TestInit()
        {
            _mockRepositories = new Mock<IRepositoriesClient>();
            _mockPriceService = new Mock<IPriceService>();
            _mockGitHubClient = new Mock<IGitHubClient>();
            _OrganizationsClient = new Mock<IOrganizationsClient>();
            _OrganizationMembersClient = new Mock<IOrganizationMembersClient>();
            _UsersClient = new Mock<IUsersClient>();
        }

        [Test]
        public async Task Returns_3_Developers_With_Price_When_Organization_Has_3_Git_Users()
        {
            //Arrange
            string organizationName = "Vtex";
            decimal generalPrice = 100m;
            //string userName =
            _mockPriceService.Setup(x => x.CalculateDeveloperPrice(It.IsAny<PricingCriteria>()))
                .Returns(generalPrice);

            var data = new List<User>()
            {
                new User (),
                new User (),
                new User ()
            };

            IReadOnlyList<Repository> repositories = new List<Repository>()
            {
                new Repository (),
            };
            //Octokit Mocks

            _UsersClient.Setup(x => x.Get(It.IsAny<string>()))
                .Returns(Task.FromResult<User>(new User()));

            _OrganizationMembersClient.Setup(x => x.GetAll(organizationName))
                .Returns(Task.FromResult<IReadOnlyList<User>>(data));

            _OrganizationsClient.Setup(x => x.Member)
                .Returns(_OrganizationMembersClient.Object);

            _mockRepositories.Setup(x => x.GetAllForUser(It.IsAny<string>()))
                .Returns(Task.FromResult<IReadOnlyList<Repository>>(repositories));

            _mockGitHubClient.Setup(x => x.Repository)
                .Returns(_mockRepositories.Object);

            _mockGitHubClient.Setup(x => x.Organization)
                .Returns(_OrganizationsClient.Object);

            _mockGitHubClient.Setup(x => x.User)
                .Returns(_UsersClient.Object);

            DeveloperService sut = new DeveloperService(_mockGitHubClient.Object, _mockPriceService.Object);

            //Act
            var developers = await sut.GetDevelopersByOrganization(organizationName);

            //Assert
            Assert.AreEqual(developers.Count(), 3);
            //Price on first should be general price
            Assert.AreEqual(developers.First().Price, generalPrice);
        }
    }
}
