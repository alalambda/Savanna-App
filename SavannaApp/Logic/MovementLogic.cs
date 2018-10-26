using System.Collections.Generic;
using SavannaApp.Interfaces;
using System.Linq;
using SavannaApp.Constants;
using Contracts;

namespace SavannaApp.Logic
{
    public class MovementLogic : IMovementLogic
    {
        private readonly ICoordinatesLogic _coordinatesLogic;
        private readonly IHealthLogic _healthLogic;
        private readonly IAnimalLogic _animalLogic;

        public MovementLogic(ICoordinatesLogic coordinatesLogic, IHealthLogic healthLogic, IAnimalLogic animalLogic)
        {
            _coordinatesLogic = coordinatesLogic;
            _healthLogic = healthLogic;
            _animalLogic = animalLogic;
        }

        public MovementLogic()
        {
            _coordinatesLogic = new CoordinatesLogic();
            _healthLogic = new HealthLogic();
            _animalLogic = new AnimalLogic();
        }

        public List<IAnimal> Behave(IAnimal animal, IEnumerable<IAnimal> animals)
        {
            animals = TryDie(animal, animals);

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

        private IEnumerable<IAnimal> TryDie(IAnimal animal, IEnumerable<IAnimal> animals)
        {
            if (animal.Health <= 0)
            {
                animals = _healthLogic.Die(animals.ToList());
            }

            animals = _animalLogic.RemoveDeadAnimals(animals);

            return animals;
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

            if (predatorsInVisionRange == null || predatorsInVisionRange.Count() == 0)
            {
                return _coordinatesLogic.GetPath(carnivore.Coordinates);
            }

            return _coordinatesLogic.GetEscapePath(carnivore, predatorsInVisionRange);
        }

        private Coordinates TryChasePrey(IAnimal predator, IEnumerable<IAnimal> animals)
        {
            var carnivores = animals.Where(a => !a.IsPredator).ToList();
            var closestPrey = _coordinatesLogic.GetClosestPreyCoordinatesInVisionRange(predator, carnivores);

            if (closestPrey == null)
                return _coordinatesLogic.GetPath(predator.Coordinates);

            return _coordinatesLogic.GetPathToPrey(predator, closestPrey);
        }

        public void Spawn(IAnimal animal, List<IAnimal> animals)
        {
            animal.Coordinates = _coordinatesLogic.GetRandomAvailableCoordinates(animal, animals);
        }
    }
}
