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

        //private Coordinates GetNewRandomCoordinates(List<IAnimal> animals, IAnimal animal)
        //{
        //    if (animal.Coordinates == null)
        //    {
        //        return GenerateRandomCoordinates(0, ConstantValues.FieldDimensionX);
        //    }

        //    Coordinates newCoordinates = ApplyDifferenceOnCoordinates(animal.Coordinates);
        //    newCoordinates = AdjustCoordinates(newCoordinates);

        //    if (_animalLogic.FindAnimalByCoordinates(animals, newCoordinates) != null)
        //    {
        //        return animal.Coordinates;
        //    }

        //    return newCoordinates;
        //}

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

        //private int GetDirectionForCoordinate(int carnivoreCoordinate, int predatorCoordinate)
        //{
        //    if (carnivoreCoordinate - predatorCoordinate < 0)
        //    {
        //        return -1;
        //    }
        //    else
        //    {
        //        return 1;
        //    }
        //}

        //private List<Coordinates> GetDirections(IAnimal animal, List<IAnimal> animalsInVisionRange)
        //{
        //    var directions = new List<Coordinates>();
        //    foreach (var animalInVisionRange in animalsInVisionRange)
        //    {
        //        Coordinates direction;
        //        if (animal is Antelope antelope && animalInVisionRange is Lion lion)
        //        {
        //            direction = GetDirection(antelope, lion);
        //        }
        //        else
        //        {
        //            direction = GetDirection(animalInVisionRange as Antelope, animal as Lion);
        //        }
        //        directions.Add(direction);
        //    }
        //    return directions;
        //}

        //private Coordinates GetDirection(Antelope antelope, Lion lion)
        //{
        //    int x = GetDirectionForCoordinate(antelope.Coordinates.X, lion.Coordinates.X);
        //    int y = GetDirectionForCoordinate(antelope.Coordinates.Y, lion.Coordinates.Y);

        //    return new Coordinates(x, y);
        //}

        private List<Coordinates> GetForbiddenCoordinates(IAnimal animal, List<IAnimal> animals)
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

        private IEnumerable<IAnimal> GetAnimalsInVisionRange(IAnimal animal, IEnumerable<IAnimal> animals)
        {
            int xFrom = AdjustCoordinate(animal.Coordinates.X - animal.VisionRange);
            int yFrom = AdjustCoordinate(animal.Coordinates.Y - animal.VisionRange);
            int xTo = AdjustCoordinate(animal.Coordinates.X + animal.VisionRange);
            int yTo = AdjustCoordinate(animal.Coordinates.Y + animal.VisionRange);

            var detectedAnimals = new List<IAnimal>();
            for (int y = yFrom; y < yTo; y++)
            {
                for (int x = xFrom; x < xTo; x++)
                {
                    var coordinates = new Coordinates(x, y);
                    if (!coordinates.Equals(animal.Coordinates))
                    {
                        var foundAnimal = _animalLogic.FindAnimalByCoordinates(animals, coordinates);
                        //if (foundAnimal != null && animal.GetType() != foundAnimal.GetType())
                        detectedAnimals.Add(foundAnimal);
                    }
                }
            }

            return detectedAnimals;
        }

        //public Coordinates Move(IAnimal animal, List<IAnimal> animals)
        //{
        //    if (animal.Coordinates == null)
        //    {
        //        return GetNewRandomCoordinates(animals, animal);
        //    }

        //    var animalsInVisionRange = new List<IAnimal>();
        //    if (animals.Count > 1)
        //    {
        //        animalsInVisionRange = GetAnimalsInVisionRange(animal, animals);
        //    }
            
        //    var forbiddenCoordinates = new List<Coordinates>();
        //    if (animalsInVisionRange.Count > 0)
        //    {
        //        forbiddenCoordinates = GetForbiddenCoordinates(animal, animalsInVisionRange);
        //    }

        //    if (animals.Count > 1 && animalsInVisionRange.Count > 0 && forbiddenCoordinates.Count > 0)
        //    {
        //        var directions = GetDirections(animal, animalsInVisionRange);
        //        Coordinates neededDirection = GetNeededDirection(animal, directions, forbiddenCoordinates);
        //        int x = animal.Coordinates.X + neededDirection.X;
        //        int y = animal.Coordinates.Y + neededDirection.Y;
        //        var neededCoordinates = new Coordinates(x, y);
        //        neededCoordinates = AdjustCoordinates(neededCoordinates);
        //        return neededCoordinates;
        //    }

        //    return GetNewRandomCoordinates(animals, animal);
        //}

        //private Animal CatchCarnivore(Animal predator, Animal carnivore)
        //{

        //}

        //private Animal EatCarnivore(Animal predator, Animal carnivore)
        //{

        //}

        private Coordinates GetNeededDirection(IAnimal animal, 
            List<Coordinates> directions, List<Coordinates> forbiddenCoordinates)
        {
            var neededDirection = new Coordinates(0, 0);
            if (animal is Lion)
            {
                int index = _random.Next(0, directions.Count);
                neededDirection = forbiddenCoordinates[index];
            }
            else
            {
                neededDirection = GenerateRandomCoordinates(-1, 2);
                while (forbiddenCoordinates.Find(f => neededDirection.Equals(f)) != null
                    && neededDirection.X == 0 && neededDirection.Y == 0)
                {
                    neededDirection = GenerateRandomCoordinates(-1, 2);
                }
            }

            return neededDirection;
        }

        public Coordinates GetRandomAvailableCoordinates(IAnimal animal, IEnumerable<IAnimal> animals)
        {
            var coordinates = GenerateRandomCoordinates(0, ConstantValues.FieldDimensionX - 1);
            while (_animalLogic.FindAnimalByCoordinates(animals, coordinates) != null)
                coordinates = GenerateRandomCoordinates(0, ConstantValues.FieldDimensionX - 1);

            return coordinates;
        }

        public Coordinates GetDirectionsToPrey(IAnimal predator, Coordinates closestPreyCoordinates)
        {
            throw new NotImplementedException();
        }

        public Coordinates GetEscapePath(IAnimal carnivore, IEnumerable<Coordinates> predatorCoordinatesInVisionRange)
        {
            throw new NotImplementedException();
        }

        public Coordinates GetClosestPreyCoordinatesInVisionRange(IAnimal predator, IEnumerable<IAnimal> carnivores)
        {
            var animalsInVisionRange = GetAnimalsInVisionRange(predator, carnivores).ToList();
            int[] stepsRequired = GetRequiredStepCountToPrey(predator, animalsInVisionRange);

            if (stepsRequired == null)
                return null;

            var indexOfClosestPrey = Array.IndexOf(stepsRequired, stepsRequired.Min());

            return carnivores.ToList()[indexOfClosestPrey].Coordinates;
        }

        private int[] GetRequiredStepCountToPrey(IAnimal predator, List<IAnimal> animalsInVisionRange)
        {
            if (animalsInVisionRange.Count == 0)
                return null;

            int[] stepsRequired = new int[animalsInVisionRange.Count];
            for (int i = 0; i < animalsInVisionRange.Count; i++)
            {
                var x = predator.Coordinates.X - animalsInVisionRange[i].Coordinates.X;
                var y = predator.Coordinates.Y - animalsInVisionRange[i].Coordinates.Y;
                stepsRequired[i] = Math.Abs(x - y);
            }

            return stepsRequired;
        }

        public IEnumerable<Coordinates> GetPredatorsCoordinatesInVisionRange(IAnimal carnivore, IEnumerable<IAnimal> predators)
        {
            if (predators == null || predators.Count() == 0)
                return null;
            
            return GetAnimalsInVisionRange(carnivore, predators).Select(x => x.Coordinates);
        }
    }
}