using Moq;
using NUnit.Framework;
using SavannaApp.Interfaces;
using SavannaApp.Logic;
using System;
using System.Collections.Generic;
using System.Text;

namespace SavannaApp.Tests
{
    [TestFixture]
    public class MovementLogicTests
    {
        private Mock<ICoordinatesLogic> _coordinatesLogicMock;
        private Mock<IHealthLogic> _healthLogicMock;
        private Mock<IAnimalLogic> _animalLogicMock;

        private Mock<IAnimal> _animalMock;
        private Mock<IList<IAnimal>> _animalListMock;

        private IMovementLogic _classUnderTest;

        [SetUp]
        public void Setup()
        {
            _coordinatesLogicMock = new Mock<ICoordinatesLogic>();
            _healthLogicMock = new Mock<IHealthLogic>();
            _animalLogicMock = new Mock<IAnimalLogic>();

            _classUnderTest = new MovementLogic(_coordinatesLogicMock.Object, _healthLogicMock.Object, _animalLogicMock.Object);
        }

        //[Test]
        //public void TryGiveBirth_When_MatingIndexIs3_Expect_BiggerAnimalList()
        //{
        //   // _animalMock.Setup(x => x.MatingIndex).Returns(3);
        //    _animalLogicMock
        //        .Setup(x => x.Spawn(_animalMock.Object, _animalListMock.Object))
        //        .Returns(new Mock<IAnimal>().Object);


        //}

        //[Test]
        //public void TryGiveBirth_When_MatingIndexIsLessThan3_Expect_SameAnimalList()
        //{
        //    //_animalMock.Setup(x => x.MatingIndex).Returns(1);
        //    _animalLogicMock
        //       .Setup(x => x.Spawn(_animalMock.Object, _animalListMock.Object))
        //       .Returns(() => null);

        //    var result = _classUnderTest.TryGiveBirth()
        //}
    }
}
