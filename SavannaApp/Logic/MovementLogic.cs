using System.Collections.Generic;
using SavannaApp.Interfaces;
using SavannaApp.Model;
using System.Linq;
using SavannaApp.Constants;

namespace SavannaApp.Logic
{
    class MovementLogic : IMovementLogic
    {
        private readonly ICoordinatesLogic _coordinatesLogic;
        private readonly IHealthLogic _healthLogic;
        private readonly IAnimalLogic _animalLogic;

        public MovementLogic()
        {
            _coordinatesLogic = new CoordinatesLogic();
            _healthLogic = new HealthLogic();
            _animalLogic = new AnimalLogic();
        }

        public List<IAnimal> Move(IAnimal animal, IEnumerable<IAnimal> animals)
        {
            if (animal.Health <= 0)
            {
                animals = _healthLogic.Die(animal, animals);
            }

            animals = _animalLogic.RemoveDeadAnimals(animals);

            animals = TryGiveBirth(animal, animals);

            if (animal.IsPredator)
            {
                animal.Coordinates = TryChasePrey(animal, animals);
                animal.Health = _healthLogic.DecreaseHealth(animal.Health);
                animals = TryEatPrey(animal, animals);
            }

            else
            {
                animal.Coordinates = TryEscape(animal, animals);
                animal.Health = _healthLogic.DecreaseHealth(animal.Health);
            }

            return animals.ToList();
        }

        private IEnumerable<IAnimal> TryGiveBirth(IAnimal animal, IEnumerable<IAnimal> animals)
        {
            var newAnimal = _animalLogic.Spawn(animal, animals);
            if (newAnimal != null)
            {
                var animalsList = animals.ToList();
                Spawn(newAnimal, animals.ToList());
                animalsList.Add(newAnimal);
                return animalsList;
            }
            return animals;
        }

        private IEnumerable<IAnimal> TryEatPrey(IAnimal animal, IEnumerable<IAnimal> animals)
        {
            if (animals.Any(x => !x.IsPredator && animal.Coordinates.Equals(x.Coordinates)))
            {
                animals
                    .ToList()
                    .FirstOrDefault(x => !x.IsPredator && animal.Coordinates.Equals(x.Coordinates))
                    .Symbol = ConstantValues.Dead;

                animals
                    .ToList()
                    .FirstOrDefault(x => x.IsPredator && animal.Coordinates.Equals(x.Coordinates))
                    .Health = _healthLogic.IncreaseHealth(animal.Health);
            }

            return animals;
        }

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
