using SavannaApp.Constants;
using SavannaApp.Enum;
using SavannaApp.Interfaces;
using SavannaApp.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SavannaApp.Logic
{
    public class AnimalLogic : IAnimalLogic
    {
        public Animal CreateAnimal(char animalChar)
        {
            Animal animal = null;
            if (animalChar == ConstantValues.Antelope)
            {
                animal = new Antelope();
            }
            else if (animalChar == ConstantValues.Lion)
            {
                animal = new Lion();
            }

            return animal;
        }

        public Animal FindAnimalByCoordinates(List<Animal> animals, Coordinates coordinates)
        {
            return animals.Find(x => x.Coordinates.Equals(coordinates));
        }

        public int CountAnimalsByType(List<Animal> animals, Type animalType)
        {
            return animals.FindAll(x => animalType == x.GetType()).Count;
        }
    }
}
