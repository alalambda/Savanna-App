using System;
using System.Collections.Generic;
using System.Text;

namespace SavannaApp.Interfaces
{
    public interface IHealthLogic
    {
        decimal IncreaseHealth(decimal currentHealth);
        decimal DecreaseHealth(decimal currentHealth);
        IEnumerable<IAnimal> Die(IAnimal animal, IEnumerable<IAnimal> animals);
    }
}
