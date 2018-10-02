using SavannaApp.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SavannaApp.Interfaces
{
    public interface IAnimalLogic
    {
        Coordinates MoveRandomly(Field field);
        Animal GetAnimalByCoordinates(List<Animal> animals, Coordinates coordinates);
    }
}
