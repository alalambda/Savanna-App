using System.Collections.Generic;
using SavannaApp.Model;

namespace SavannaApp.Interfaces
{
    internal interface IMovementLogic
    {
        void Move(IAnimal animal, IEnumerable<IAnimal> animals);
        void Spawn(IAnimal animal, List<IAnimal> animals);
    }
}
