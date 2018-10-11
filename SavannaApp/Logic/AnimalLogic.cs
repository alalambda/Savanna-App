using SavannaApp.Constants;
using SavannaApp.Factory;
using SavannaApp.Interfaces;
using SavannaApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SavannaApp.Logic
{
    public class AnimalLogic : IAnimalLogic
    {
        private readonly AnimalFactory _animalFactory;

        public AnimalLogic()
        {
            _animalFactory = new AnimalFactory();
        }
        public IAnimal FindAnimalByCoordinates(IEnumerable<IAnimal> animals, Coordinates coordinates)
        {
            if (coordinates == null)
                return null;

            return animals?.Where(x => coordinates.Equals(x.Coordinates)).FirstOrDefault();
        }

        public IEnumerable<IAnimal> RemoveDeadAnimals(IEnumerable<IAnimal> animals)
        {
            if (animals.Any(x => x.Symbol == ConstantValues.Dead))
            {
                //TODO: refactor the remove part
                IAnimal animalToDead = animals.FirstOrDefault(x => x.Symbol == ConstantValues.Dead);
                var animalsList = animals.ToList();
                animalsList.Remove(animalToDead);
                return animalsList;
            }

            return animals;
        }

        public IAnimal Spawn(IAnimal animal, IEnumerable<IAnimal> animals)
        {
            animal.MatingIndex = GetMatingIndex(animal, animals);
            if (animal.MatingIndex == ConstantValues.MatingMaxIndex)
            {
                var newAnimal = _animalFactory.CreateAnimal(animal.Symbol);
                animal.MatingIndex = 0;
                return newAnimal;
            }

            return null;
        }

        public int GetMatingIndex(IAnimal animal, IEnumerable<IAnimal> animals)
        {
            int matingIndex = animal.MatingIndex;
            var neighbours = GetNeighbours(animal, animals);
            if (neighbours.Any())
                return ++matingIndex;

            return 0;
        }

        public IEnumerable<IAnimal> GetNeighbours(IAnimal animal, IEnumerable<IAnimal> animals)
        {
            var neighbours = new List<IAnimal>();
            for (int y = animal.Coordinates.Y - 1; y <= animal.Coordinates.Y + 1; y++)
            {
                for (int x = animal.Coordinates.X - 1; x <= animal.Coordinates.X + 1; x++)
                {
                    Coordinates coordinates = GetNeighbourCoordinates(animal, new Coordinates(x, y));
                    var neighbourAnimal = FindAnimalByCoordinates(animals, coordinates);
                    if (neighbourAnimal != null && animal.GetType() == neighbourAnimal.GetType())
                    {
                        neighbours.Add(neighbourAnimal);
                    }
                }
            }
            neighbours.Remove(animal);
            return neighbours;
        }

        private Coordinates GetNeighbourCoordinates(IAnimal animal, Coordinates coordinates)
        {
            if (!coordinates.Equals(animal.Coordinates))
            {

                if (coordinates.X < 0)
                {
                    coordinates.X = ConstantValues.FieldDimensionX - 1;
                }
                else if (coordinates.X > ConstantValues.FieldDimensionX - 1)
                {
                    coordinates.X = 0;
                }

                if (coordinates.Y < 0)
                {
                    coordinates.Y = ConstantValues.FieldDimensionY - 1;
                }
                else if (coordinates.Y > ConstantValues.FieldDimensionY - 1)
                {
                    coordinates.Y = 0;
                }
            }
            return coordinates;
        }
    }
}
