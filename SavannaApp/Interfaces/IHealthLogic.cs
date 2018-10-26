using Contracts;
using System.Collections.Generic;

namespace SavannaApp.Interfaces
{
    public interface IHealthLogic
    {
        decimal IncreaseHealth(decimal currentHealth);
        decimal DecreaseHealth(decimal currentHealth);
        IList<IAnimal> Die(IList<IAnimal> animals);
    }
}
