using SavannaApp.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SavannaApp.Interfaces
{
    public interface IAnimalLogic
    {
        Coordinates MakeRandomMove(Coordinates currentCoordinates, Field field);
        Animal GetAnimalByCoordinates(List<Animal> animals, Coordinates coordinates);
        int CountAntelopes(List<Animal> animals);
    }
}
