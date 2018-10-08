using SavannaApp.Constants;
using SavannaApp.Interfaces;
using SavannaApp.Model;
using System.Collections.Generic;

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
            if (coordinates == null)
                return null;
            return animals?.Find(x => coordinates.Equals(x.Coordinates));
        }

        public decimal DecreaseHealth(Animal animal)
        {
            decimal health = animal.Health - ConstantValues.HealthDecrease;
            if (health < 0)
            {
                health = 0;
            }

            return health;
        }

        public List<Animal> Die(Animal animal, List<Animal> animals)
        {
            if (animal.Health == 0)
            {
                animals.Remove(animal);
            }
            return animals;
        }
    }
}
