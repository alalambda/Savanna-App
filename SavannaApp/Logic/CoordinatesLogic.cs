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

        private readonly IAnimalLogic _animalLogic;

        public CoordinatesLogic()
        {
            _random = new Random(Guid.NewGuid().GetHashCode());
            _animalLogic = new AnimalLogic();
        }

        private Coordinates GenerateRandomCoordinates(int from, int toExclusive)
        {
            int x = _random.Next(from, toExclusive);
            int y = _random.Next(from, toExclusive);
            return new Coordinates(x, y);
        }

        public Coordinates GetNewCoordinates(List<Animal> animals, Coordinates currentCoordinates)
        {
            if (currentCoordinates == null)
            {
                return GenerateRandomCoordinates(0, ConstantValues.FieldDimensionX);
            }

            Coordinates newCoordinates = ApplyDifferenceOnCoordinates(currentCoordinates);
            newCoordinates = AdjustCoordinates(newCoordinates);

            if (_animalLogic.FindAnimalByCoordinates(animals, newCoordinates) != null)
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

        // there should be logic for animals to avoid/catch each other
        public void MeaningfulMethodName(List<Animal> animals)
        {
            for (int y = 0; y < ConstantValues.FieldDimensionY; y++)
            {
                for (int x = 0; x < ConstantValues.FieldDimensionX; x++)
                {
                    var coordinates = new Coordinates(x, y);
                    var animal = _animalLogic.FindAnimalByCoordinates(animals, coordinates);
                    if (animal != null)
                    {
                        if (animal is Antelope antelope)
                        {
                            
                        }
                        else if (animal is Lion lion)
                        {
                            
                        }
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
