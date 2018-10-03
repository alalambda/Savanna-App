using SavannaApp.Constants;
using SavannaApp.Enum;
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

        private Coordinates GenerateRandomCoordinates(int from, int toExclusive)
        {
            int x = _random.Next(from, toExclusive);
            int y = _random.Next(from, toExclusive);
            return new Coordinates(x, y);
        }

        public Coordinates GetNewCoordinates(Coordinates currentCoordinates, Field field)
        {
            if (currentCoordinates == null)
            {
                return GenerateRandomCoordinates(0, ConstantValues.FieldDimensionX);
            }

            var move = GenerateRandomCoordinates(-1, 2);
            while (move.X == 0 && move.Y == 0)
            {
                move = GenerateRandomCoordinates(-1, 2);
            }

            int newX = currentCoordinates.X + move.X;
            int newY = currentCoordinates.Y + move.Y;

            if (newX < 0)
            {
                newX = 0;
            }
            else if (newX > ConstantValues.FieldDimensionX - 1)
            {
                newX = ConstantValues.FieldDimensionX - 1;
            }
            if (newY < 0)
            {
                newY = 0;
            }
            else if (newY > ConstantValues.FieldDimensionY - 1)
            {
                newY = ConstantValues.FieldDimensionY - 1;
            }

            if (field.Cells[newX, newY].State != State.Empty)
            {
                return currentCoordinates;
            }
            return new Coordinates(newX, newY);
        }
    }
}
