using NUnit.Framework;
using SavannaApp.Constants;
using SavannaApp.Interfaces;
using SavannaApp.Logic;
using SavannaApp.Model;
using System.Collections.Generic;
using System.Linq;
using Contracts;

namespace SavannaApp.Tests
{
    [TestFixture]
    public class HealthLogicTests
    {
        private IHealthLogic _classUnderTest;

        [SetUp]
        public void Setup()
        {
            _classUnderTest = new HealthLogic();
        }

        [Test]
        public void IncreaseHealth_When_PositiveHealth_Expect_GreaterHealth()
        {
            //Arrange
            decimal value = 10m;
            decimal result = 0;

            //Act
            result = _classUnderTest.IncreaseHealth(value);

            //Assert
            Assert.Greater(result, value);
            Assert.AreEqual(result, 20);
            Assert.AreEqual(result.GetType(), typeof(decimal));
        }

        [Test]
        public void DecreaseHealth_When_PositiveHealth_Expect_LessHealth()
        {
            //Arrange
            decimal value = 5m;

            //Act
            var result = _classUnderTest.DecreaseHealth(value);

            //Assert
            Assert.Greater(value, result);
            Assert.AreEqual(result, 4.5);
            Assert.AreEqual(result.GetType(), typeof(decimal));
        }

        [Test]
        public void Die_When_ZeroHealth_Expect_MarkedWithX()
        {
            //Arrange
            var animal = new Lion() { Health = 0 };
            var animalList = new List<IAnimal> { animal };
            
            //Act
            var result = _classUnderTest.Die(animalList).Where(x => x == animal).FirstOrDefault();

            //Assert
            Assert.AreEqual(ConstantValues.Dead, result.Symbol);
        }
    }
}
