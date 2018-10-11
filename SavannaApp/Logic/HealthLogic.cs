using SavannaApp.Constants;
using SavannaApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SavannaApp.Model;

namespace SavannaApp.Logic
{
    public class HealthLogic : IHealthLogic
    {
        public decimal DecreaseHealth(IAnimal animal)
        {
            return animal.Health -= ConstantValues.HealthDecrease;
        }

        public IEnumerable<IAnimal> Die(IAnimal animal, IEnumerable<IAnimal> animals)
        {
            if (animal.Health <= 0)
            {
                var animalsList = animals.ToList();
                animalsList.Remove(animal);
                return animalsList;
            }

            return animals;
        }

        public decimal IncreaseHealth(IAnimal animal)
        {
            return animal.Health += ConstantValues.HealthIncrease;
        }
    }
}
