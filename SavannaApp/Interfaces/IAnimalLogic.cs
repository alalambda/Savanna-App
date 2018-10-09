using SavannaApp.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SavannaApp.Interfaces
{
    public interface IAnimalLogic
    {
        IAnimal FindAnimalByCoordinates(IEnumerable<IAnimal> animals, Coordinates coordinates);
        decimal DecreaseHealth(IAnimal animal);
        IEnumerable<IAnimal> Die(IAnimal animal, IEnumerable<IAnimal> animals);
    }
}
