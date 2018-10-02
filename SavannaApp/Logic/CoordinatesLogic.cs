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
        private readonly IAnimalLogic _animalLogic;
        private readonly Random _random;

        public CoordinatesLogic(Random random)
        {
            _random = random;
            _animalLogic = new AnimalLogic(random);
        }

        public List<Animal> InitAnimalCoordinates(List<string> animalStrings)
        {
            var animals = new List<Animal>(animalStrings.Count);
            foreach (var animal in animalStrings)
            {
                int x = _random.Next(0, ConstantValues.FieldDimensionX - 1);
                int y = _random.Next(0, ConstantValues.FieldDimensionY - 1);
                Coordinates coordinates = new Coordinates(x, y);
                while (_animalLogic.GetAnimalByCoordinates(animals, coordinates) != null)
                {
                    x = _random.Next(0, ConstantValues.FieldDimensionX - 1);
                    y = _random.Next(0, ConstantValues.FieldDimensionY - 1);
                    coordinates = new Coordinates(x, y);
                }
                if (ConstantValues.Antelope == animal)
                {
                    Animal antelope = new Antelope() { Coordinates = coordinates };
                    animals.Add(antelope);
                }
                else if (ConstantValues.Lion == animal)
                {
                    Animal lion = new Lion() { Coordinates = coordinates };
                    animals.Add(lion);
                }
            }
            return animals;
        }
    }
}
