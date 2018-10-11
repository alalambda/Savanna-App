﻿using SavannaApp.Constants;
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
        public decimal DecreaseHealth(decimal currentHealth)
        {
            return currentHealth -= ConstantValues.HealthDecrease;
        }

        public IEnumerable<IAnimal> Die(IAnimal animal, IEnumerable<IAnimal> animals)
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
