using SavannaApp.Constants;
using SavannaApp.Interfaces;
using SavannaApp.Model;
using SavannaApp.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace SavannaApp.Logic
{
    public class CoordinatesLogic : ICoordinatesLogic
    {
        private readonly IAnimalLogic _animalLogic;

        public CoordinatesLogic()
        {
            _animalLogic = new AnimalLogic();
        }

        public List<Animal> InitAnimalCoordinates(List<string> animalStrings)
        {
            RandomizerUtil randomizer = new RandomizerUtil();
            var animals = new List<Animal>(animalStrings.Count);
            foreach (var animal in animalStrings)
            {
                int x = randomizer.GetRandomInRange(0, ConstantValues.FieldDimensionX - 1);
                int y = randomizer.GetRandomInRange(0, ConstantValues.FieldDimensionY - 1);
                Coordinates coordinates = new Coordinates(x, y);
                while (_animalLogic.GetAnimalByCoordinates(animals, coordinates) != null)
                {
                    x = randomizer.GetRandomInRange(0, ConstantValues.FieldDimensionX - 1);
                    y = randomizer.GetRandomInRange(0, ConstantValues.FieldDimensionY - 1);
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
