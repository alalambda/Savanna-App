using SavannaApp.Constants;
using SavannaApp.Interfaces;
using SavannaApp.Model;
using SavannaApp.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace SavannaApp.Logic
{
    public class AnimalLogic : IAnimalLogic
    {
        public Coordinates MakeRandomMove(Coordinates currentCoordinates, Field field)
        {
            RandomizerUtil randomizer = new RandomizerUtil();
            int x = randomizer.GetRandomInRange(-1, 1);
            int y = randomizer.GetRandomInRange(-1, 1);
            while (x == 0 && y == 0)
            {
                x = randomizer.GetRandomInRange(-1, 1);
                y = randomizer.GetRandomInRange(-1, 1);
            }

            int newX = currentCoordinates.X + x;
            int newY = currentCoordinates.Y + y;

            if (newX < 0) newX = 0;
            else if (newX > ConstantValues.FieldDimensionX - 1) newX = ConstantValues.FieldDimensionX - 1;
            if (newY < 0) newY = 0;
            else if (newY > ConstantValues.FieldDimensionY - 1) newY = ConstantValues.FieldDimensionY - 1;

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
    }
}
