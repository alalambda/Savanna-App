using SavannaApp.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SavannaApp.Interfaces
{
    public interface IAnimalLogic
    {
        IAnimal FindAnimalByCoordinates(IEnumerable<IAnimal> animals, Coordinates coordinates);
        IEnumerable<IAnimal> RemoveDeadAnimals(IEnumerable<IAnimal> animals);
    }
}
