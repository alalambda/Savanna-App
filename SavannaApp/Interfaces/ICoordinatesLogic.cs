using SavannaApp.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SavannaApp.Interfaces
{
    public interface ICoordinatesLogic
    {
        List<Animal> GenerateAnimalCoordinates(List<string> animalStrings);
    }
}
