using Moq;
using NUnit.Framework;
using SavannaApp.Interfaces;
using SavannaApp.Logic;
using SavannaApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SavannaApp.HealthLogicTests
{
    [TestFixture]
    public class HealthLogicTests
    {
        private IHealthLogic _classUnderTest;
        private Mock<IList<IAnimal>> _IAnimalList;
        private Mock<IAnimal> _animalMock;

        //private IEnumerable _mock;

        [SetUp]
        public void Setup()
        {
            _classUnderTest = new HealthLogic();
            _animalMock = new Mock<IAnimal>();
            _IAnimalList = new Mock<IList<IAnimal>>();
            //var itemMock = new Mock<IAnimal>();
            //_items = new List<IAnimal> { itemMock.Object }; //<--IEnumerable<IMyObject>

            //_mock = new Mock<IEnumerable>();
            //mock.Setup(m => m.Count).Returns(() => items.Count);
            //mock.Setup(m => m[It.IsAny<int>()]).Returns<int>(i => items.ElementAt(i));
            //mock.Setup(m => m.GetEnumerator()).Returns(() => items.GetEnumerator());
            ////var value = 0;

            _animalMock.Setup(x => x.Health).Returns(0);
            _animalMock.SetupAllProperties();
            _IAnimalList.Object.Add(_animalMock.Object);
            _IAnimalList.Setup(x => x.Count).Returns(1);
            //_AnimalListMock.SetupGet<IAnimal>(x => x.GetEnumerator<IList<IAnimal>>)
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
        }

        [Test]
        public void Die_When_ZeroHealth_Expect_MarkedWithX()
        {
            //Arrange
            //var value = 0;
            var expected = new Mock<IAnimal>();
            expected.Setup(x => x.Symbol).Returns('X');


            //Act
            var result = _classUnderTest.Die(_IAnimalList.Object);

            //Assert
            Assert.Equals(result, expected);
        }
    }
}
