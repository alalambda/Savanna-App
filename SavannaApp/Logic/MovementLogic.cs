using System.Collections.Generic;
using SavannaApp.Interfaces;
using SavannaApp.Model;
using System;
using System.Linq;
using SavannaApp.Constants;

namespace SavannaApp.Logic
{
    class MovementLogic : IMovementLogic
    {
        private readonly ICoordinatesLogic _coordinatesLogic;

        public MovementLogic()
        {
            _coordinatesLogic = new CoordinatesLogic();
        }

        public List<IAnimal> Move(IAnimal animal, IEnumerable<IAnimal> animals)
        {
            //animals = RemoveCorpses(animals);
            if (animal.IsPredator)
            {
                animal.Coordinates = TryChasePrey(animal, animals);
                animals = TryEatPrey(animal, animals);
            }

            else
                animal.Coordinates = TryEscape(animal, animals);

            return animals.ToList();
        }

        private IEnumerable<IAnimal> TryEatPrey(IAnimal animal, IEnumerable<IAnimal> animals)
        {
            if (animals.Any(x => !x.IsPredator && animal.Coordinates.Equals(x.Coordinates)))
            {
                //TODO: refactor the remove part
                IAnimal animalToDead = animals.FirstOrDefault(x => !x.IsPredator && animal.Coordinates.Equals(x.Coordinates));
                var animalsList = animals.ToList();
                animalsList.Remove(animalToDead);
                return animalsList;
            }

            return animals;
        }

        //private IEnumerable<IAnimal> RemoveCorpses(IEnumerable<IAnimal> animals)
        //{
        //    if (animals.Any(x => x.Symbol == ConstantValues.Dead))
        //    {
        //        return animals.Where(x => x.Symbol != ConstantValues.Dead).ToList();
        //    }

        //    return animals;
        //}

        private Coordinates TryEscape(IAnimal carnivore, IEnumerable<IAnimal> animals)
        {
            var predators = animals.Where(a => a.IsPredator).ToList();
            var predatorsInVisionRange = _coordinatesLogic.GetPredatorsCoordinatesInVisionRange(carnivore, predators);

            return predatorsInVisionRange != null
                ? _coordinatesLogic.GetEscapePath(carnivore, predatorsInVisionRange)
                : _coordinatesLogic.GetPath(carnivore.Coordinates);
        }

        private Coordinates TryChasePrey(IAnimal predator, IEnumerable<IAnimal> animals)
        {
            var carnivores = animals.Where(a => !a.IsPredator).ToList();
            var closestPrey = _coordinatesLogic.GetClosestPreyCoordinatesInVisionRange(predator, carnivores);

            return closestPrey != null
                ? _coordinatesLogic.GetPathToPrey(predator, closestPrey)
                : _coordinatesLogic.GetPath(predator.Coordinates);
        }

        public void Spawn(IAnimal animal, List<IAnimal> animals)
        {
            animal.Coordinates = _coordinatesLogic.GetRandomAvailableCoordinates(animal, animals);
        }
    }
}
