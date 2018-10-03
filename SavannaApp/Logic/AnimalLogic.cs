using SavannaApp.Constants;
using SavannaApp.Interfaces;
using SavannaApp.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SavannaApp.Logic
{
    public class AnimalLogic : IAnimalLogic
    {
        private readonly Random _random;

        public AnimalLogic()
        {
            _random = new Random(Guid.NewGuid().GetHashCode());
        }

        public Coordinates MakeRandomMove(Coordinates currentCoordinates, Field field)
        {
            int x = _random.Next(-1, 2);
            int y = _random.Next(-1, 2);
            while (x == 0 && y == 0)
            {
                x = _random.Next(-1, 2);
                y = _random.Next(-1, 2);
            }

            int newX = currentCoordinates.X + x;
            int newY = currentCoordinates.Y + y;

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

            if (field.Cells[newX, newY].Animal != null)
            {
                return currentCoordinates;
            }
            return new Coordinates(newX, newY);
        }

        public Animal GetAnimalByCoordinates(List<Animal> animals, Coordinates coordinates)
        {
            return animals.Find(x => x.Coordinates.Equals(coordinates));
        }

        public int CountAntelopes(List<Animal> animals)
        {
            return animals.FindAll(x => typeof(Antelope) == x.GetType()).Count;
        }

        public int CountLions(List<Animal> animals)
        {
            return animals.FindAll(x => typeof(Lion) == x.GetType()).Count;
        }
    }
}
