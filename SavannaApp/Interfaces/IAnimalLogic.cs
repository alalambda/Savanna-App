using SavannaApp.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SavannaApp.Interfaces
{
    public interface IAnimalLogic
    {
        IAnimal FindAnimalByCoordinates(List<IAnimal> animals, Coordinates coordinates);
        IAnimal CreateAnimal(char animalChar);
        decimal DecreaseHealth(IAnimal animal);
        List<IAnimal> Die(IAnimal animal, List<IAnimal> animals);
    }
}
