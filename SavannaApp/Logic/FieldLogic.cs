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
        private readonly ICellLogic _cellLogic;
        private readonly IAnimalLogic _animalLogic;

        public FieldLogic()
        {
            _cellLogic = new CellLogic();
            _animalLogic = new AnimalLogic();
        }

        public Field MakeField(Field field, List<Animal> animals)
        {
            for (int y = 0; y < ConstantValues.FieldDimensionY; y++)
            {
                for (int x = 0; x < ConstantValues.FieldDimensionX; x++)
                {
                    if (animals.Count == 0)
                    {
                        field.Cells[x, y] = new Cell() { State = State.Empty };
                    }
                    
                    var coordinates = new Coordinates(x, y);
                    var animal = _animalLogic.FindAnimalByCoordinates(animals, coordinates);
                    if (animal != null)
                    {
                        var state = _cellLogic.GetCellState(animal);
                        field.Cells[x, y] = new Cell() { State = state };
                    }
                    else
                    {
                        field.Cells[x, y] = new Cell() { State = State.Empty };
                    }
                }
            }

            return field;
        }
    }
}