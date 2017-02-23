using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vtex.ShoppingCart.Repo;
using Vtex.ShoppingCart.Repo.Interface;
using Vtex.ShoppingCart.Service.Models;

namespace Vtex.ShoppingCart.Service.Test
{
    [TestFixture]
    public class PriceServiceTest
    {
        Mock<IPriceRepository> _mockPriceRepository;


        [SetUp]
        public void TestInit()
        {
            _mockPriceRepository = new Mock<IPriceRepository>();
        }


        [Test]
        public void Price_Is_Price_Base_When_No_Bonuses()
        {
            //Arrange
            decimal basePrice = 100m;
            decimal bonusPrice = 10m;
            _mockPriceRepository.Setup(x => x.GetPrice(PriceKeys.Base_Price)).Returns(basePrice);
            _mockPriceRepository.Setup(x => x.GetPrice(PriceKeys.Followers_Bonus_Price)).Returns(bonusPrice);
            _mockPriceRepository.Setup(x => x.GetPrice(PriceKeys.Public_Repositories_Bonus_Price)).Returns(bonusPrice);
            _mockPriceRepository.Setup(x => x.GetPrice(PriceKeys.Stars_Bonus_Price)).Returns(bonusPrice);
            //Act
            //sut means subject under test
            var sut = new PriceService(_mockPriceRepository.Object);

            PricingCriteria criteria = new PricingCriteria()
            {
                Followers = 0,
                Stars = 0,
                PublicRepositories = 0,
            };

            decimal output = sut.CalculateDeveloperPrice(criteria);

            //Assert
            Assert.AreEqual(basePrice, output);
        }

        [Test]
        public void Price_With_Three_Criterias()
        {
            //Arrange
            decimal basePrice = 100m;
            decimal bonusPrice = 10m;
            _mockPriceRepository.Setup(x => x.GetPrice(PriceKeys.Base_Price)).Returns(basePrice);
            _mockPriceRepository.Setup(x => x.GetPrice(PriceKeys.Followers_Bonus_Price)).Returns(bonusPrice);
            _mockPriceRepository.Setup(x => x.GetPrice(PriceKeys.Public_Repositories_Bonus_Price)).Returns(bonusPrice);
            _mockPriceRepository.Setup(x => x.GetPrice(PriceKeys.Stars_Bonus_Price)).Returns(bonusPrice);
            //Act
            //sut means subject under test
            var sut = new PriceService(_mockPriceRepository.Object);

            PricingCriteria criteria = new PricingCriteria()
            {
                Followers = 15,
                Stars = 30,
                PublicRepositories = 10,
            };

            decimal output = sut.CalculateDeveloperPrice(criteria);

            //Assert
            Assert.AreEqual(output, 650);
        }
    }
}
