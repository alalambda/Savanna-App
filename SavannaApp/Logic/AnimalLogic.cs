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
        /*
        public void Move(Animal animal, Field field)
        {
            var visionRange = animal.VisionRange;
            for (int y = -visionRange; y <= visionRange; y++)
            {
                for (int x = -visionRange; x <= visionRange; x++)
                {
                    int width = x + animal.Coordinates.X;
                    int height = y + animal.Coordinates.Y;


                }
            }
        }
        */

        public Coordinates MoveRandomly(Field field)
        {
            RandomizerUtil randomizer = new RandomizerUtil();
            int x = randomizer.GetRandomInRange(-1, 1);
            int y = randomizer.GetRandomInRange(-1, 1);
            while (field.Cells[x, y].Animal != null && x != 0 && y != 0)
            {
                x = randomizer.GetRandomInRange(-1, 1);
                y = randomizer.GetRandomInRange(-1, 1);
            }
            return new Coordinates(x, y);
        }

        public Animal GetAnimalByCoordinates(List<Animal> animals, Coordinates coordinates)
        {
            return animals.Find(x => x.Coordinates.Equals(coordinates));
        }
    }
}
