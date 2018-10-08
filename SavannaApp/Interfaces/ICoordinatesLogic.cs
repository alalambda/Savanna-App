using SavannaApp.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SavannaApp.Interfaces
{
    public interface ICoordinatesLogic
    {
        Coordinates Move(IAnimal animal, List<IAnimal> animals);
    }
}
