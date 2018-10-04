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

            Coordinates newCoordinates = ApplyDifferenceOnCoordinates(currentCoordinates);

            newCoordinates = AdjustCoordinates(newCoordinates);

            if (field.Cells[newCoordinates.X, newCoordinates.Y].State != State.Empty)
            {
                return currentCoordinates;
            }
            return newCoordinates;
        }

        private Coordinates AdjustCoordinates(Coordinates newCoordinates)
        {
            newCoordinates.X = AdjustCoordinate(newCoordinates.X);
            newCoordinates.Y = AdjustCoordinate(newCoordinates.Y);

            return newCoordinates;
        }

        private int AdjustCoordinate(int coordinate)
        {
            if (coordinate < 0)
            {
                coordinate = 0;
            }
            else if (coordinate > ConstantValues.FieldDimensionX - 1)
            {
                coordinate = ConstantValues.FieldDimensionX - 1;
            }
            return coordinate;
        }

        private Coordinates ApplyDifferenceOnCoordinates(Coordinates currentCoordinates)
        {
            var move = GenerateRandomCoordinates(-1, 2);
            while (move.X == 0 && move.Y == 0)
            {
                move = GenerateRandomCoordinates(-1, 2);
            }
            int newX = currentCoordinates.X + move.X;
            int newY = currentCoordinates.Y + move.Y;

            return new Coordinates(newX, newY);
        }
    }
}
