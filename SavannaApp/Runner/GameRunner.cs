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
        private readonly IUserInterface _userInterface;

        private readonly IFieldLogic _fieldLogic;
        private readonly IMoveLogic _moveLogic;
        private readonly ICoordinatesLogic _coordinatesLogic;
        private readonly IAnimalLogic _animalLogic;

        public GameRunner()
        {
            _userInterface = new ConsoleUserInterface();

            _fieldLogic = new FieldLogic();
            _moveLogic = new MoveLogic();
            _coordinatesLogic = new CoordinatesLogic();
            _animalLogic = new AnimalLogic();
        }

        public void Start()
        {
            var field = new Field(ConstantValues.FieldDimensionX, ConstantValues.FieldDimensionY);
            var animals = new List<Animal>();
            ConsoleKeyInfo? keyPressedInfo = null;
            do
            {
                Console.Clear();
                field = _fieldLogic.MakeField(field, animals);
                _userInterface.PrintField(field);

                if (keyPressedInfo.HasValue)
                {
                    char animalChar = keyPressedInfo.Value.KeyChar;// _userInterface.GetAnimalChar();
                    if (animalChar != ConstantValues.Antelope && animalChar != ConstantValues.Lion)
                        continue;

                    var newAnimal = _animalLogic.CreateAnimal(animalChar);
                    if (newAnimal != null)
                        animals.Add(newAnimal);
                }

                while (Console.KeyAvailable == false && animals.Count != 0)
                {
                    foreach (var animal in animals)
                    {
                        var coordinates = _moveLogic.MakeRandomMove(animal.Coordinates, field);
                        animal.Coordinates = coordinates;
                    }
                    Thread.Sleep(1000);
                    Console.Clear();
                    field = _fieldLogic.MakeField(field, animals);
                    _userInterface.PrintField(field);
                }

                keyPressedInfo = Console.ReadKey(true);
            } while (keyPressedInfo.HasValue && keyPressedInfo.Value.Key != ConsoleKey.Escape);
        }
    }
}
