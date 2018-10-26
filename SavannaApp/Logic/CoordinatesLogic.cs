using Contracts;
using SavannaApp.Constants;
using SavannaApp.Interfaces;
using SavannaApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;

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

        private Coordinates GetRandomDirection()
        {
            var direction = GenerateRandomCoordinates(-1, 2);
            while (direction.X == 0 && direction.Y == 0)
            {
                direction = GenerateRandomCoordinates(-1, 2);
            }

            return direction;
        }

        public Coordinates GetPath(Coordinates coordinates)
        {
            var direction = GetRandomDirection();
            int x = coordinates.X + direction.X;
            int y = coordinates.Y + direction.Y;

            var newCoordinates = AdjustCoordinates(new Coordinates(x, y));

            return newCoordinates;
        }

        private IEnumerable<Coordinates> GetEscapeDirections
            (IAnimal animal, IEnumerable<Coordinates> animalsCoordinates)
        {
            int x = 0;
            int y = 0;
            var escapeDirections = new List<Coordinates>();
            foreach (var predatorCoordinate in animalsCoordinates)
            {
                x = animal.Coordinates.X - predatorCoordinate.X;
                y = animal.Coordinates.Y - predatorCoordinate.Y;
                Coordinates escapeDirection = new Coordinates(x, y);
                escapeDirections.Add(escapeDirection);
            }
            return escapeDirections;
        }

        private int GetDirectionToAnimal(int animalCoordinate1, int animalCoordinate2)
        {
            if (animalCoordinate1 > animalCoordinate2)
            {
                return -1;
            }
            else if (animalCoordinate1 < animalCoordinate2)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        private IEnumerable<Coordinates> GetAnimalsCoordinatesInVisionRange
            (IAnimal animal, IEnumerable<Coordinates> animalsCoordinates)
        {
            int xFrom = AdjustCoordinate(animal.Coordinates.X - animal.VisionRange);
            int yFrom = AdjustCoordinate(animal.Coordinates.Y - animal.VisionRange);
            int xTo = AdjustCoordinate(animal.Coordinates.X + animal.VisionRange);
            int yTo = AdjustCoordinate(animal.Coordinates.Y + animal.VisionRange);

            var animalsCoordinatesInVisionRange = new List<Coordinates>();
            for (int y = yFrom; y < yTo; y++)
            {
                for (int x = xFrom; x < xTo; x++)
                {
                    var coordinates = new Coordinates(x, y);
                    if (!coordinates.Equals(animal.Coordinates))
                    {
                        var foundCoordinates = animalsCoordinates.FirstOrDefault(q => coordinates.Equals(q));
                        if (foundCoordinates != null)
                            animalsCoordinatesInVisionRange.Add(coordinates);
                    }
                }
            }

            return animalsCoordinatesInVisionRange;
        }

        public Coordinates GetRandomAvailableCoordinates(IAnimal animal, IEnumerable<IAnimal> animals)
        {
            var coordinates = GenerateRandomCoordinates(0, ConstantValues.FieldDimensionX - 1);
            while (_animalLogic.FindAnimalByCoordinates(animals.ToList(), coordinates) != null)
                coordinates = GenerateRandomCoordinates(0, ConstantValues.FieldDimensionX - 1);

            return coordinates;
        }

        private Coordinates GetDirectionToPrey(IAnimal predator, Coordinates closestPreyCoordinates)
        {
            if (closestPreyCoordinates == null)
                return null;

            var x = GetDirectionToAnimal(predator.Coordinates.X, closestPreyCoordinates.X);
            var y = GetDirectionToAnimal(predator.Coordinates.Y, closestPreyCoordinates.Y);

            return new Coordinates(x, y);
        }

        public Coordinates GetPathToPrey(IAnimal predator, Coordinates closestPreyCoordinates)
        {
            if (closestPreyCoordinates == null)
                return null;

            var directionToPrey = GetDirectionToPrey(predator, closestPreyCoordinates);
            int x = predator.Coordinates.X + directionToPrey.X;
            int y = predator.Coordinates.Y + directionToPrey.Y;

            return AdjustCoordinates(new Coordinates(x, y));
        }

        private Coordinates GetEscapeDirection(IAnimal carnivore, IEnumerable<Coordinates> predatorCoordinatesInVisionRange)
        {
            if (predatorCoordinatesInVisionRange == null)
                return null;

            int x = 0;
            int y = 0;
            var escapeDirections = GetEscapeDirections(carnivore, predatorCoordinatesInVisionRange);
            foreach (var escapeDirection in escapeDirections)
            {
                x += escapeDirection.X;
                y += escapeDirection.Y;
            }

            return new Coordinates(Math.Sign(x), Math.Sign(y));
        }

        public Coordinates GetEscapePath(IAnimal carnivore, IEnumerable<Coordinates> predatorCoordinatesInVisionRange)
        {
            if (predatorCoordinatesInVisionRange == null)
            {
                return null;
            }

            var escapeDirection = GetEscapeDirection(carnivore, predatorCoordinatesInVisionRange);
            var x = carnivore.Coordinates.X + escapeDirection.X;
            var y = carnivore.Coordinates.Y + escapeDirection.Y;

            var escapeDirectionCoordinates = AdjustCoordinates(new Coordinates(x, y));

            if (carnivore.Coordinates.Equals(escapeDirectionCoordinates))
            {
                escapeDirectionCoordinates.X = carnivore.Coordinates.X + escapeDirection.Y;
                escapeDirectionCoordinates.Y = carnivore.Coordinates.Y + escapeDirection.X;
            }

            return AdjustCoordinates(escapeDirectionCoordinates);
        }

        public Coordinates GetClosestPreyCoordinatesInVisionRange(IAnimal predator, IEnumerable<IAnimal> carnivores)
        {
            var carnivoresCoordinates = carnivores.Select(x => x.Coordinates);
            var animalsInVisionRange = GetAnimalsCoordinatesInVisionRange(predator, carnivoresCoordinates).ToList();
            int[] stepsRequired = GetRequiredStepCountToPrey(predator, animalsInVisionRange);

            if (stepsRequired == null)
                return null;

            var indexOfClosestPrey = Array.IndexOf(stepsRequired, stepsRequired.Min());

            return animalsInVisionRange[indexOfClosestPrey];
        }

        private int[] GetRequiredStepCountToPrey(IAnimal predator, List<Coordinates> animalsInVisionRange)
        {
            if (animalsInVisionRange.Count == 0)
                return null;

            int[] stepsRequired = new int[animalsInVisionRange.Count];
            for (int i = 0; i < animalsInVisionRange.Count; i++)
            {
                var x = predator.Coordinates.X - animalsInVisionRange[i].X;
                var y = predator.Coordinates.Y - animalsInVisionRange[i].Y;
                stepsRequired[i] = Math.Abs(x - y);
            }

            return stepsRequired;
        }

        public IEnumerable<Coordinates> GetPredatorsCoordinatesInVisionRange(IAnimal carnivore, IEnumerable<IAnimal> predators)
        {
            if (predators == null || predators.Count() == 0)
                return null;

            var predatorsCoordinates = predators.Select(x => x.Coordinates);
            return GetAnimalsCoordinatesInVisionRange(carnivore, predatorsCoordinates);
        }
    }
}