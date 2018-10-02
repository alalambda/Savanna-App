using SavannaApp.Constants;
using SavannaApp.Interfaces;
using SavannaApp.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SavannaApp.Logic
{
    public class CoordinatesLogic : ICoordinatesLogic
    {
        private readonly Random _random;

        public CoordinatesLogic()
        {
            _random = new Random(Guid.NewGuid().GetHashCode());
        }

        public Coordinates GenerateAnimalCoordinates()
        {
            int x = _random.Next(0, ConstantValues.FieldDimensionX);
            int y = _random.Next(0, ConstantValues.FieldDimensionY);

            return new Coordinates(x, y);
        }
    }
}
