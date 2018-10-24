using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using SavannaApp.Constants;
using SavannaApp.Interfaces;
using SavannaApp.Logic;
using SavannaApp.Model;

namespace SavannaApp.Tests
{
    [TestFixture]
    public class MovementLogicTests
    {
        [SetUp]
        public void Setup()
        {
            _coordinatesLogicMock = new Mock<ICoordinatesLogic>();
            _healthLogicMock = new Mock<IHealthLogic>();
            _animalLogicMock = new Mock<IAnimalLogic>();

            _movementLogic = new MovementLogic(_coordinatesLogicMock.Object, _healthLogicMock.Object,
                _animalLogicMock.Object);
        }

        private Mock<ICoordinatesLogic> _coordinatesLogicMock;
        private Mock<IHealthLogic> _healthLogicMock;
        private Mock<IAnimalLogic> _animalLogicMock;

        private IMovementLogic _movementLogic;

        [Test]
        public void Should_Spawn()
        {
            // Arrange
            var testAnimal = new Lion();

            // Act
            _movementLogic.Spawn(testAnimal, new List<IAnimal>());

            // Assert
            _coordinatesLogicMock.Verify(x => x.GetRandomAvailableCoordinates(It.IsAny<IAnimal>(), It.IsAny<List<IAnimal>>()), Times.Once);
        }

        [Test]
        public void Behave_When_AnimalIsCarnivore_And_NoPredatorInVisionRange_Expect_GetPathCall()
        {
            // Arrange
            var carnivore = new Antelope() { Coordinates = new Coordinates(0, 1) };
            var animalList = new List<IAnimal> { carnivore };

            _animalLogicMock
                .Setup(x => x.RemoveDeadAnimals(animalList))
                .Returns(() => animalList);

            _coordinatesLogicMock
                .Setup(x => x.GetPredatorsCoordinatesInVisionRange(carnivore, It.IsAny<List<IAnimal>>()))
                .Returns(() => new List<Coordinates>());

            _coordinatesLogicMock
                .Setup(x => x.GetPath(carnivore.Coordinates))
                .Returns(() => carnivore.Coordinates);

            // Act
            _movementLogic.Behave(carnivore, animalList);

            // Assert
            _coordinatesLogicMock.Verify(x => x.GetPath(carnivore.Coordinates));
        }

        [Test]
        public void Behave_When_AnimalIsCarnivore_And_PredatorInVisionRange_Expect_GetEscapePathCall()
        {
            // Arrange
            var carnivore = new Antelope() { Coordinates = new Coordinates(0, 1) };
            var predator = new Lion() { Coordinates = new Coordinates(0, 0) };
            var animalList = new List<IAnimal> { carnivore, predator };

            _animalLogicMock
                .Setup(x => x.RemoveDeadAnimals(animalList))
                .Returns(() => animalList);

            _coordinatesLogicMock
                .Setup(x => x.GetPredatorsCoordinatesInVisionRange(carnivore, It.IsAny<List<IAnimal>>()))
                .Returns(() => new List<Coordinates> { predator.Coordinates });

            // Act
            _movementLogic.Behave(carnivore, animalList);

            // Assert
            _coordinatesLogicMock.Verify(x => x.GetEscapePath(carnivore, It.IsAny<List<Coordinates>>()));
        }

        [Test]
        public void Behave_When_PredatorAndCarnivoreCoordinatesNotEqual_Expect_SameCarnivore()
        {
            // Arrange
            var predator = new Lion() { Coordinates = new Coordinates(6, 5) };
            var carnivore1 = new Antelope() { Coordinates = new Coordinates(4, 5) };
            var carnivore2 = new Antelope() { Coordinates = new Coordinates(5, 5) };
            var animalList = new List<IAnimal> { predator, carnivore1, carnivore2 };

            _animalLogicMock
                .Setup(x => x.RemoveDeadAnimals(animalList))
                .Returns(() => animalList);

            _coordinatesLogicMock
                .Setup(x => x.GetPath(predator.Coordinates))
                .Returns(() => predator.Coordinates);

            // Act 
            var result = _movementLogic.Behave(predator, animalList);

            // Assert
            Assert.False(result.Exists(x => !x.IsPredator && x.Symbol == ConstantValues.Dead));
        }

        [Test]
        public void Behave_When_PredatorAndCarnivoreCoordinatesAreEqual_Expect_MarkCarnivoreWithX()
        {
            // Arrange
            var predator = new Lion() { Coordinates = new Coordinates(4, 5) };
            var carnivore1 = new Antelope() { Coordinates = new Coordinates(4, 5) };
            var carnivore2 = new Antelope() { Coordinates = new Coordinates(5, 5) };
            var animalList = new List<IAnimal> { predator, carnivore1, carnivore2 };

            _animalLogicMock
                .Setup(x => x.RemoveDeadAnimals(animalList))
                .Returns(() => animalList);

            _coordinatesLogicMock
                .Setup(x => x.GetPath(predator.Coordinates))
                .Returns(() => predator.Coordinates);

            // Act
            var result = _movementLogic.Behave(predator, animalList);

            // Assert
            Assert.IsTrue(result.Exists(x => !x.IsPredator 
                            && x.Symbol == ConstantValues.Dead 
                            && x.Coordinates.Equals(predator.Coordinates)));

            Assert.IsTrue(result.Exists(x => !x.IsPredator
                            && x.Symbol != ConstantValues.Dead
                            && x.Coordinates.Equals(carnivore2.Coordinates)));
        }

        [Test]
        public void Behave_When_AnimalIsPredator_And_ClosestPrey_Expect_GetPathToPreyCall()
        {
            // Arrange
            var animal = new Lion();
            var animalList = new List<IAnimal> { animal };
            var preyCoordinates = new Coordinates(0, 0);

            _animalLogicMock
                .Setup(x => x.RemoveDeadAnimals(animalList))
                .Returns(() => animalList);

            _animalLogicMock
                .Setup(x => x.Spawn(animal, animalList))
                .Returns(() => null);

            _coordinatesLogicMock
                .Setup(x => x.GetClosestPreyCoordinatesInVisionRange(animal, It.IsAny<List<IAnimal>>()))
                .Returns(() => preyCoordinates);

            // Act
            _movementLogic.Behave(animal, animalList);

            // Assert
            _coordinatesLogicMock.Verify(x => x.GetPathToPrey(animal, preyCoordinates));
        }

        [Test]
        public void Behave_When_AnimalIsPredator_And_NoClosestPrey_Expect_GetPathCall()
        {
            // Arrange
            var animal = new Lion();
            var animalList = new List<IAnimal> { animal };

            _animalLogicMock
                .Setup(x => x.RemoveDeadAnimals(animalList))
                .Returns(() => animalList);

            _animalLogicMock
                .Setup(x => x.Spawn(animal, animalList))
                .Returns(() => null);

            _coordinatesLogicMock.Setup(x => x.GetClosestPreyCoordinatesInVisionRange(animal, animalList))
                .Returns(() => null);

            // Act
            _movementLogic.Behave(animal, animalList);

            // Assert
            _coordinatesLogicMock.Verify(x => x.GetPath(animal.Coordinates));
        }

        [Test]
        public void Behave_When_HealthEqualsOrLessThan0_Expect_SmallerAnimalList()
        {
            //Arrange
            var animal = new Antelope { Health = 0 };
            var animalList = new List<IAnimal> { animal };

            _animalLogicMock
                .Setup(x => x.RemoveDeadAnimals(animalList))
                .Returns(() => animalList);

            // Act
            var result = _movementLogic.Behave(animal, animalList);

            // Assert
            Assert.IsEmpty(result);
        }

        [Test]
        public void Behave_When_HealthGreatedThan0_Expect_SameAnimalList()
        {
            // Arrange
            var animal = new Lion { Health = 7.5m };
            var animalList = new List<IAnimal> { animal };

            _animalLogicMock
                .Setup(x => x.RemoveDeadAnimals(animalList))
                .Returns(() => animalList);

            // Act
            var result = _movementLogic.Behave(animal, animalList);

            // Assert
            Assert.AreEqual(animalList, result);
        }

        [Test]
        public void Behave_When_SpawnResultIsNewAnimal_Expect_BiggerAnimalList()
        {
            // Arrange
            var animal = new Lion();
            var animalList = new List<IAnimal> { animal };

            _animalLogicMock
                .Setup(x => x.RemoveDeadAnimals(animalList))
                .Returns(() => animalList);
            _animalLogicMock
                .Setup(x => x.Spawn(animal, animalList))
                .Returns(() => new Lion());

            // Act
            var result = _movementLogic.Behave(animal, animalList);

            // Assert
            Assert.Less(animalList.Count, result.Count);
        }

        [Test]
        public void Behave_When_SpawnResultIsNull_Expect_SameAnimalList()
        {
            // Arrange
            var animal = new Lion();
            var animalList = new List<IAnimal> { animal };

            _animalLogicMock
                .Setup(x => x.RemoveDeadAnimals(animalList))
                .Returns(() => animalList);
            _animalLogicMock
                .Setup(x => x.Spawn(animal, animalList))
                .Returns(() => null);

            // Act
            var result = _movementLogic.Behave(animal, animalList);

            // Assert
            Assert.AreEqual(animalList, result);
        }
    }
}