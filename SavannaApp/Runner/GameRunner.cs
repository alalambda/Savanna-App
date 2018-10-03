using SavannaApp.Constants;
using SavannaApp.Enum;
using SavannaApp.Interfaces;
using SavannaApp.Logic;
using SavannaApp.Model;
using SavannaApp.UserInterface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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

            do
            {
                Console.Clear();
                field = _fieldLogic.MakeField(field, animals);
                _userInterface.PrintField(field);

                char animalChar = _userInterface.GetAnimalChar();
                if (animalChar != ConstantValues.Antelope && animalChar != ConstantValues.Lion)
                {
                    continue;
                }
                var newAnimal = _animalLogic.CreateAnimal(animalChar);
                if (newAnimal != null) animals.Add(newAnimal);

                foreach (var animal in animals)
                {
                    var coordinates = 
                        _coordinatesLogic.GenerateRandomCoordinates(0, ConstantValues.FieldDimensionX);
                    animal.Coordinates = coordinates;
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
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }
    }
}
