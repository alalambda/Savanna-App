using SavannaApp.Constants;
using SavannaApp.Interfaces;
using SavannaApp.Logic;
using SavannaApp.Model;
using SavannaApp.UserInterface;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SavannaApp.Runner
{
    public class GameRunner
    {
        private readonly List<Animal> _animals;
        private Field _field;

        private readonly IUserInterface _userInterface;

        private readonly IFieldLogic _fieldLogic;
        private readonly ICoordinatesLogic _coordinatesLogic;
        private readonly IAnimalLogic _animalLogic;

        public GameRunner()
        {
            _animals = new List<Animal>();
            _field = new Field(ConstantValues.FieldDimensionX, ConstantValues.FieldDimensionY);

            _userInterface = new ConsoleUserInterface();

            _fieldLogic = new FieldLogic();
            _coordinatesLogic = new CoordinatesLogic();
            _animalLogic = new AnimalLogic();
        }

        public void Start()
        {
            ConsoleKeyInfo? keyPressedInfo = null;
            do
            {
                RefreshField();

                if (keyPressedInfo.HasValue)
                {
                    char animalChar = keyPressedInfo.Value.KeyChar;
                    if (char.ToUpper(animalChar) != ConstantValues.Antelope 
                        && char.ToUpper(animalChar) != ConstantValues.Lion)
                        continue;

                    var newAnimal = _animalLogic.CreateAnimal(animalChar);
                    if (newAnimal != null)
                        _animals.Add(newAnimal);
                }

                while (Console.KeyAvailable == false && _animals.Count != 0)
                {
                    foreach (var animal in _animals)
                    {
                        var coordinates = _coordinatesLogic.GetNewCoordinates(animal.Coordinates, _field);
                        animal.Coordinates = coordinates;
                    }
                    Thread.Sleep(1000);
                    RefreshField();
                }

                keyPressedInfo = Console.ReadKey(true);
            } while (keyPressedInfo.HasValue && keyPressedInfo.Value.Key != ConsoleKey.Escape);
        }

        private void RefreshField()
        {
            Console.Clear();
            _field = _fieldLogic.MakeField(_field, _animals);
            _userInterface.PrintField(_field);
        }
    }
}
