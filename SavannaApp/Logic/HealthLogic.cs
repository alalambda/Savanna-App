using Contracts;
using SavannaApp.Constants;
using SavannaApp.Interfaces;
using System.Collections.Generic;

namespace SavannaApp.Logic
{
    public class HealthLogic : IHealthLogic
    {
        public decimal DecreaseHealth(decimal currentHealth)
        {
            return currentHealth -= ConstantValues.HealthDecrease;
        }

        public IList<IAnimal> Die(IList<IAnimal> animals)
        {
            foreach (var item in animals)
            {
                if (item.Health <= 0)
                {
                    item.Symbol = ConstantValues.Dead;
                }
            }

            return animals;
        }

        public decimal IncreaseHealth(decimal currentHealth)
        {
            return currentHealth += ConstantValues.HealthIncrease;
        }
    }
}
