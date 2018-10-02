using SavannaApp.Constants;
using SavannaApp.Enum;
using SavannaApp.Interfaces;
using SavannaApp.Model;
using SavannaApp.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace SavannaApp.Logic
{
    public class FieldLogic : IFieldLogic
    {
        private Coordinates GenerateRandomCoordinate()
        {
            var randomizer = new RandomizerUtil();
            var x = randomizer.GetRandomInRange(0, ConstantValues.FieldDimensionX - 1);
            var y = randomizer.GetRandomInRange(0, ConstantValues.FieldDimensionY - 1);
            return new Coordinates(x, y);
        }

        private Field InitEmptyField()
        {
            var field = new Field(ConstantValues.FieldDimensionX, ConstantValues.FieldDimensionY);
            for (int y = 0; y < ConstantValues.FieldDimensionY; y++)
            {
                for (int x = 0; x < ConstantValues.FieldDimensionX; x++)
                {
                    field.Cells[x, y] = new Cell() { State = State.Empty };
                }
            }
            return field;
        }

        public Field InitAnimalsOnField(List<string> animals)
        {
            Cell cell;
            var field = InitEmptyField();
            foreach (var animal in animals)
            {
                var randomCoordinates = GenerateRandomCoordinate();
                int x = randomCoordinates.X;
                int y = randomCoordinates.Y;
                while (field.Cells[x, y].Animal != null)
                {
                    randomCoordinates = GenerateRandomCoordinate();
                    x = randomCoordinates.X;
                    y = randomCoordinates.Y;
                }
                if (ConstantValues.Antelope == animal)
                {
                    cell = new Cell() { State = State.Antelope, Animal = new Antelope() };
                    field.Cells[x, y] = cell;
                }
                else if (ConstantValues.Lion == animal)
                {
                    cell = new Cell() { State = State.Lion, Animal = new Lion() };
                    field.Cells[x, y] = cell;
                }
            }
            return field;
        }
    }
}