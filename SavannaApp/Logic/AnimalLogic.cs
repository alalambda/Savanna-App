using SavannaApp.Constants;
using SavannaApp.Interfaces;
using SavannaApp.Model;
using System.Collections.Generic;

namespace SavannaApp.Logic
{
    public class AnimalLogic : IAnimalLogic
    {
        public IAnimal FindAnimalByCoordinates(List<IAnimal> animals, Coordinates coordinates)
        {
            if (coordinates == null)
                return null;
            return animals?.Find(x => coordinates.Equals(x.Coordinates));
        }

        public decimal DecreaseHealth(IAnimal animal)
        {
            decimal health = animal.Health - ConstantValues.HealthDecrease;
            if (health < 0)
            {
                health = 0;
            }

            return health;
        }

        public List<IAnimal> Die(IAnimal animal, List<IAnimal> animals)
        {
            if (animal.Health == 0)
            {
                animals.Remove(animal);
            }
            return animals;
        }
    }
}
