using SavannaApp.Model;
using System.Collections.Generic;

namespace SavannaApp.Interfaces
{
    public interface ICoordinatesLogic
    {
        Coordinates GetRandomAvailableCoordinates(IAnimal animal, IEnumerable<IAnimal> animals);
        Coordinates GetPathToPrey(IAnimal predator, Coordinates closestPreyCoordinates);
        Coordinates GetEscapePath(IAnimal carnivore, IEnumerable<Coordinates> predatorCoordinatesInVisionRange);
        Coordinates GetClosestPreyCoordinatesInVisionRange(IAnimal predator, IEnumerable<IAnimal> carnivores);
        IEnumerable<Coordinates> GetPredatorsCoordinatesInVisionRange(IAnimal carnivore, IEnumerable<IAnimal> predators);
        Coordinates GetPath(Coordinates coordinates);
    }
}
