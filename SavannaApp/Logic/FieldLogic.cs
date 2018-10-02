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
        private readonly IAnimalLogic _animalLogic;

        public FieldLogic()
        {
            _animalLogic = new AnimalLogic();
        }

        public Field MakeField(List<Animal> animals)
        {
            var field = new Field(ConstantValues.FieldDimensionX, ConstantValues.FieldDimensionY);
            for (int y = 0; y < ConstantValues.FieldDimensionY; y++)
            {
                for (int x = 0; x < ConstantValues.FieldDimensionX; x++)
                {
                    var coordinates = new Coordinates(x, y);
                    var animal = _animalLogic.GetAnimalByCoordinates(animals, coordinates);
                    if (animal != null) 
                    {
                        var state = typeof(Antelope) == animal.GetType() ? State.Antelope : State.Lion;
                        field.Cells[x, y] = new Cell() { Animal = animal, State = state };
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