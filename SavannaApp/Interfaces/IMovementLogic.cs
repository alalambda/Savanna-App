using System.Collections.Generic;

namespace SavannaApp.Interfaces
{
    public interface IMovementLogic
    {
        List<IAnimal> Behave(IAnimal animal, IEnumerable<IAnimal> animals);
        void Spawn(IAnimal animal, List<IAnimal> animals);
    }
}
