using SavannaApp.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SavannaApp.Interfaces
{
    public interface ICoordinatesLogic
    {
        Coordinates Move(Animal animal, List<Animal> animals);
    }
}
