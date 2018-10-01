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
        private readonly List<string> _possibleStates;

        public FieldLogic()
        {
            _possibleStates = new List<string> { ConstantValues.Lion, ConstantValues.Antelope, ConstantValues.Empty };
        }

        public Field GetInitialField(int lionCount, int antilopeCount)
        {
            var randomizer = new RandomizerUtil();
            var field = new Field(ConstantValues.FieldDimensionX, ConstantValues.FieldDimensionY);
            for (int y = 0; y < ConstantValues.FieldDimensionY; y++)
            {
                for (int x = 0; x < ConstantValues.FieldDimensionX; x++)
                {
                    var randomState = _possibleStates[randomizer.GetRandomInRange(0, 2)];
                    if (ConstantValues.Antelope == randomState && antilopeCount != 0)
                    {
                        var cell = new Cell() { State = State.Antelope, Animal = new Antelope() };
                        field.Cells[x, y] = cell;
                        antilopeCount--;
                    }
                    else if (ConstantValues.Lion == randomState && lionCount != 0)
                    {
                        var cell = new Cell() { State = State.Lion, Animal = new Lion() };
                        field.Cells[x, y] = cell;
                        lionCount--;
                    }
                    else
                    {
                        var cell = new Cell() { State = State.Empty };
                        field.Cells[x, y] = cell;
                    }
                }
            }
            return field;
        }
    }
}