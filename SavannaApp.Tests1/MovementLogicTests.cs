using Moq;
using NUnit.Framework;
using SavannaApp.Interfaces;
using SavannaApp.Logic;
using SavannaApp.Model;
using System.Collections.Generic;

namespace SavannaApp.Tests
{
    [TestFixture]
    public class MovementLogicTests
    {
        private Mock<ICoordinatesLogic> _coordinatesLogicMock;
        private Mock<IHealthLogic> _healthLogicMock;
        private Mock<IAnimalLogic> _animalLogicMock;

        private IMovementLogic _classUnderTest;

        [SetUp]
        public void Setup()
        {
            _coordinatesLogicMock = new Mock<ICoordinatesLogic>();
            _healthLogicMock = new Mock<IHealthLogic>();
            _animalLogicMock = new Mock<IAnimalLogic>();

            _classUnderTest = new MovementLogic(_coordinatesLogicMock.Object, _healthLogicMock.Object, _animalLogicMock.Object);
        }
        
        [Test]
        public void Behave_When_HealthEqualsOrLessThan0_Expect_SmallerAnimalList()
        {
            //Arrange
            var animal = new Antelope() { Health = 0 };
            var animalList = new List<IAnimal> { animal };

            _animalLogicMock.Setup(x => x.RemoveDeadAnimals(animalList)).Returns(() => animalList);

            // Act
            var result = _classUnderTest.Behave(animal, animalList);

            // Assert
            Assert.IsEmpty(result);
        }

        [Test]
        public void Behave_When_HealthGreatedThan0_Expect_SameAnimalList()
        {
            // Arrange
            var animal = new Lion() { Health = 7.5m };
            var animalList = new List<IAnimal> { animal };

            _animalLogicMock.Setup(x => x.RemoveDeadAnimals(animalList)).Returns(() => animalList);

            // Act
            var result = _classUnderTest.Behave(animal, animalList);

            // Assert
            Assert.AreEqual(animalList, result);
        }

        [Test]
        public void Behave_When_SpawnResultIsNull_Expect_SameAnimalList()
        {
            // Arrange
            var animal = new Lion();
            var animalList = new List<IAnimal> { animal };

            _animalLogicMock.Setup(x => x.RemoveDeadAnimals(animalList)).Returns(() => animalList);
            _animalLogicMock.Setup(x => x.Spawn(animal, animalList)).Returns(() => null);

            // Act
            var result = _classUnderTest.Behave(animal, animalList);

            // Assert
            Assert.AreEqual(animalList, result);
        }

        [Test]
        public void Behave_When_SpawnResultIsNewAnimal_Expect_BiggerAnimalList()
        {
            // Arrange
            var animal = new Lion();
            var animalList = new List<IAnimal> { animal };

            _animalLogicMock.Setup(x => x.RemoveDeadAnimals(animalList)).Returns(() => animalList);
            _animalLogicMock.Setup(x => x.Spawn(animal, animalList)).Returns(() => new Lion());

            // Act
            var result = _classUnderTest.Behave(animal, animalList);

            // Assert
            Assert.Less(animalList.Count, result.Count);
        }

        [Test]
        public void Behave_When_AnimalIsPredator_And_NoClosestPrey_Expect_GetPathCall()
        {
            // Arrange
            var animal = new Lion();
            var animalList = new List<IAnimal> { animal };

            _animalLogicMock.Setup(x => x.RemoveDeadAnimals(animalList)).Returns(() => animalList);
            _animalLogicMock.Setup(x => x.Spawn(animal, animalList)).Returns(() => null);

            _coordinatesLogicMock.Setup(x => x.GetClosestPreyCoordinatesInVisionRange(animal, animalList)).Returns(() => null);

            // Act
            var result = _classUnderTest.Behave(animal, animalList);

            // Assert
            _coordinatesLogicMock.Verify(x => x.GetPath(animal.Coordinates));
        }

        [Test]
        public void Behave_When_AnimalIsPredator_And_ClosestPrey_Expect_GetPathToPreyCall()
        {
            // Arrange
            var animal = new Lion();
            var animalList = new List<IAnimal> { animal };

            _animalLogicMock.Setup(x => x.RemoveDeadAnimals(animalList)).Returns(() => animalList);
            _animalLogicMock.Setup(x => x.Spawn(animal, animalList)).Returns(() => null);

            _coordinatesLogicMock.Setup(x => x.GetClosestPreyCoordinatesInVisionRange(animal, animalList)).Returns(() => null);

            // Act
            var result = _classUnderTest.Behave(animal, animalList);

            // Assert
            _coordinatesLogicMock.Verify(x => x.GetPath(animal.Coordinates));
        }
    }
}
