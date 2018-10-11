using System;
using System.Collections.Generic;
using System.Text;

namespace SavannaApp.Interfaces
{
    public interface IHealthLogic
    {
        decimal IncreaseHealth(IAnimal animal);
        decimal DecreaseHealth(IAnimal animal);
        IEnumerable<IAnimal> Die(IAnimal animal, IEnumerable<IAnimal> animals);
    }
}
