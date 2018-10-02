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

        public CoordinatesLogic()
        {
            _random = new Random(Guid.NewGuid().GetHashCode());
            _animalLogic = new AnimalLogic();
        }

        public List<Animal> GenerateAnimalCoordinates(List<string> animalStrings)
        {
            var animals = new List<Animal>(animalStrings.Count);
            foreach (var animal in animalStrings)
            {
                int x = _random.Next(0, ConstantValues.FieldDimensionX);
                int y = _random.Next(0, ConstantValues.FieldDimensionY);
                Coordinates coordinates = new Coordinates(x, y);
                while (_animalLogic.GetAnimalByCoordinates(animals, coordinates) != null)
                {
                    x = _random.Next(0, ConstantValues.FieldDimensionX);
                    y = _random.Next(0, ConstantValues.FieldDimensionY);
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
