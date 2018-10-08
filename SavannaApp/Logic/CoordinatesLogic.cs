using SavannaApp.Constants;
using SavannaApp.Interfaces;
using SavannaApp.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SavannaApp.Logic
{
    public class CoordinatesLogic : ICoordinatesLogic
    {
        private readonly Random _random;

        private readonly IAnimalLogic _animalLogic;

        public CoordinatesLogic()
        {
            _random = new Random(Guid.NewGuid().GetHashCode());
            _animalLogic = new AnimalLogic();
        }

        private Coordinates GenerateRandomCoordinates(int from, int toExclusive)
        {
            int x = _random.Next(from, toExclusive);
            int y = _random.Next(from, toExclusive);
            return new Coordinates(x, y);
        }

        private Coordinates GetNewRandomCoordinates(List<Animal> animals, Coordinates currentCoordinates)
        {
            if (currentCoordinates == null)
            {
                return GenerateRandomCoordinates(0, ConstantValues.FieldDimensionX);
            }

            Coordinates newCoordinates = ApplyDifferenceOnCoordinates(currentCoordinates);
            newCoordinates = AdjustCoordinates(newCoordinates);

            if (_animalLogic.FindAnimalByCoordinates(animals, newCoordinates) != null)
            {
                return currentCoordinates;
            }

            return newCoordinates;
        }

        private Coordinates AdjustCoordinates(Coordinates newCoordinates)
        {
            newCoordinates.X = AdjustCoordinate(newCoordinates.X);
            newCoordinates.Y = AdjustCoordinate(newCoordinates.Y);

            return newCoordinates;
        }

        private int AdjustCoordinate(int coordinate)
        {
            if (coordinate < 0)
            {
                coordinate = 0;
            }
            else if (coordinate > ConstantValues.FieldDimensionX - 1)
            {
                coordinate = ConstantValues.FieldDimensionX - 1;
            }
            return coordinate;
        }

        private Coordinates ApplyDifferenceOnCoordinates(Coordinates currentCoordinates)
        {
            var move = GenerateRandomCoordinates(-1, 2);
            while (move.X == 0 && move.Y == 0)
            {
                move = GenerateRandomCoordinates(-1, 2);
            }
            int newX = currentCoordinates.X + move.X;
            int newY = currentCoordinates.Y + move.Y;

            return new Coordinates(newX, newY);
        }

        private int GetDirectionForCoordinate(int carnivoreCoordinate, int predatorCoordinate)
        {
            if (carnivoreCoordinate - predatorCoordinate < 0)
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }

        private Coordinates GetDirection(Antelope antelope, Lion lion)
        {
            int x = GetDirectionForCoordinate(antelope.Coordinates.X, lion.Coordinates.X);
            int y = GetDirectionForCoordinate(antelope.Coordinates.Y, lion.Coordinates.Y);

            return new Coordinates(x, y);
        }

        private List<Coordinates> GetForbiddenCoordinates(Animal animal, List<Animal> animals)
        {
            int forbiddenX = 0;
            int forbiddenY = 0;
            var forbiddenCoordinates = new List<Coordinates>();
            foreach (var detectedAnimal in animals)
            {
                forbiddenX = GetForbiddenCoordinate(animal.Coordinates.X, detectedAnimal.Coordinates.X);
                forbiddenY = GetForbiddenCoordinate(animal.Coordinates.Y, detectedAnimal.Coordinates.Y);
                Coordinates forbiddenCoordinate = new Coordinates(forbiddenX, forbiddenY);
                forbiddenCoordinates.Add(forbiddenCoordinate);
            }
            return forbiddenCoordinates;
        }

        private int GetForbiddenCoordinate(int animalCoordinate, int detectedAnimalCoordinate)
        {
            if (animalCoordinate > detectedAnimalCoordinate)
            {
                return -1;
            }
            else if (animalCoordinate < detectedAnimalCoordinate)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        private List<Animal> GetAnimalsInVisionRange(Animal animal, List<Animal> animals)
        {
            int xFrom = AdjustCoordinate(animal.Coordinates.X - animal.VisionRange);
            int yFrom = AdjustCoordinate(animal.Coordinates.Y - animal.VisionRange);
            int xTo = AdjustCoordinate(animal.Coordinates.X + animal.VisionRange);
            int yTo = AdjustCoordinate(animal.Coordinates.Y + animal.VisionRange);

            var detectedAnimals = new List<Animal>();
            for (int y = yFrom; y < yTo; y++)
            {
                for (int x = xFrom; x < xTo; x++)
                {
                    var coordinates = new Coordinates(x, y);
                    if (!coordinates.Equals(animal.Coordinates))
                    {
                        var foundAnimal = _animalLogic.FindAnimalByCoordinates(animals, coordinates);
                        if (foundAnimal != null && animal.GetType() != foundAnimal.GetType())
                        {
                            detectedAnimals.Add(foundAnimal);
                        }
                    }
                }
            }

            return detectedAnimals;
        }

        public Coordinates Move(Animal animal, List<Animal> animals)
        {
            if (animal.Coordinates == null)
            {
                return GetNewRandomCoordinates(animals, animal.Coordinates);
            }
            List<Animal> animalsInVisionRange = GetAnimalsInVisionRange(animal, animals);

            var forbiddenCoordinates = new List<Coordinates>();
            if (animals.Count > 1)
            {
                forbiddenCoordinates = GetForbiddenCoordinates(animal, animals);
            }

            Coordinates coordinates = GetNewRandomCoordinates(animals, animal.Coordinates);
            if (animalsInVisionRange.Count != 0 && forbiddenCoordinates.Count != 0)
            {
                if (animal is Antelope)
                {
                    while (forbiddenCoordinates.Find(x => coordinates.Equals(x)) != null)
                    {
                        coordinates = GetNewRandomCoordinates(animals, animal.Coordinates);
                    }
                }
                else
                {
                    while (forbiddenCoordinates.Find(x => coordinates.Equals(x)) == null)
                    {
                        coordinates = GetNewRandomCoordinates(animals, animal.Coordinates);
                    }
                }
            }

            return coordinates;
        }
    }
}