using SavannaApp.Model;
using System.Collections.Generic;

namespace SavannaApp.Interfaces
{
    public interface IAnimalLogic
    {
        IAnimal FindAnimalByCoordinates(IEnumerable<IAnimal> animals, Coordinates coordinates);
        IEnumerable<IAnimal> RemoveDeadAnimals(IEnumerable<IAnimal> animals);
        IAnimal Spawn(IAnimal animal, IEnumerable<IAnimal> animals);
        IEnumerable<IAnimal> GetNeighbours(IAnimal animal, IEnumerable<IAnimal> animals);
    }
}
