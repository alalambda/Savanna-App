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
        private Tuple<int, int> GenerateRandomCoordinate()
        {
            var randomizer = new RandomizerUtil();
            var x = randomizer.GetRandomInRange(0, ConstantValues.FieldDimensionX - 1);
            var y = randomizer.GetRandomInRange(0, ConstantValues.FieldDimensionY - 1);
            return Tuple.Create(x, y);
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
                int x = randomCoordinates.Item1;
                int y = randomCoordinates.Item2;
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
                else
                {
                    cell = new Cell() { State = State.Empty };
                    field.Cells[x, y] = cell;
                }
            }

            return field;
        }

        /*
        public Field GetInitialField(int lionCount, int antilopeCount)
        {
            Cell cell;
            var field = new Field(ConstantValues.FieldDimensionX, ConstantValues.FieldDimensionY);
            for (int y = 0; y < ConstantValues.FieldDimensionY; y++)
            {
                for (int x = 0; x < ConstantValues.FieldDimensionX; x++)
                {
                    if (ConstantValues.Antelope == randomState && antilopeCount != 0)
                    {
                        cell = new Cell() { State = State.Antelope, Animal = new Antelope() };
                        field.Cells[y, x] = cell;
                        antilopeCount--;
                    }
                    else if (ConstantValues.Lion == randomState && lionCount != 0)
                    {
                        cell = new Cell() { State = State.Lion, Animal = new Lion() };
                        field.Cells[y, x] = cell;
                        lionCount--;
                    }
                    else
                    {
                        cell = new Cell() { State = State.Empty };
                        field.Cells[y, x] = cell;
                    }
                }
            }
            return field;
        }
        */
    }
}