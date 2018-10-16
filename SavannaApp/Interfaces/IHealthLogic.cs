using System.Collections.Generic;

namespace SavannaApp.Interfaces
{
    public interface IHealthLogic
    {
        decimal IncreaseHealth(decimal currentHealth);
        decimal DecreaseHealth(decimal currentHealth);
        IEnumerable<IAnimal> Die(IAnimal animal, IEnumerable<IAnimal> animals);
    }
}
