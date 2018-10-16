using System.Collections.Generic;

namespace SavannaApp.Interfaces
{
    internal interface IMovementLogic
    {
        List<IAnimal> Move(IAnimal animal, IEnumerable<IAnimal> animals);
        void Spawn(IAnimal animal, List<IAnimal> animals);
    }
}
