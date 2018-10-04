using SavannaApp.Constants;
using SavannaApp.Enum;
using SavannaApp.Interfaces;
using SavannaApp.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SavannaApp.Logic
{
    public class FieldLogic : IFieldLogic
    {
        private readonly IAnimalLogic _animalLogic;

        public FieldLogic()
        {
            _animalLogic = new AnimalLogic();
        }

        public Field GetNewField(Field field, List<Animal> animals)
        {
            for (int y = 0; y < ConstantValues.FieldDimensionY; y++)
            {
                for (int x = 0; x < ConstantValues.FieldDimensionX; x++)
                {
                    if (animals.Count == 0)
                    {
                        field.Cells[x, y] = new Cell() { State = State.Empty };
                    }
                    else
                    {
                        var coordinates = new Coordinates(x, y);
                        var animal = _animalLogic.FindAnimalByCoordinates(animals, coordinates);

                        var state = GetCellState(animal);
                        field.Cells[x, y] = new Cell() { State = state };
                    }
                }
            }

            return field;
        }

        public State GetCellState(Animal animal)
        {
            if (animal == null)
            {
                return State.Empty;
            }

            return typeof(Antelope) == animal.GetType() ? State.Antelope : State.Lion;
        }
    }
}