using SavannaApp.Constants;
using SavannaApp.Interfaces;
using SavannaApp.Model;
using System.Collections.Generic;
using System.Linq;

namespace SavannaApp.Logic
{
    public class AnimalLogic : IAnimalLogic
    {
        public IAnimal FindAnimalByCoordinates(IEnumerable<IAnimal> animals, Coordinates coordinates)
        {
            if (coordinates == null)
                return null;

            return animals?.Where(x => coordinates.Equals(x.Coordinates)).FirstOrDefault();
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

        public IEnumerable<IAnimal> Die(IAnimal animal, IEnumerable<IAnimal> animals)
        {
            if (animal.Health == 0)
            {
                animals.Where(x => x != animal);
            }
            return animals;
        }
    }
}
