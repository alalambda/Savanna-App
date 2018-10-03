using SavannaApp.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SavannaApp.Interfaces
{
    public interface IAnimalLogic
    {
        Animal FindAnimalByCoordinates(List<Animal> animals, Coordinates coordinates);
        Animal CreateAnimal(char animalChar);
        int CountAnimalsByType(List<Animal> animals, Type animalType);
    }
}
