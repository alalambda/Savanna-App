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
        decimal DecreaseHealth(Animal animal);
        List<Animal> Die(Animal animal, List<Animal> animals);
    }
}
