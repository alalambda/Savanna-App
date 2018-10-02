using SavannaApp.Constants;
using SavannaApp.Interfaces;
using SavannaApp.Logic;
using SavannaApp.Model;
using SavannaApp.UserInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SavannaApp.Runner
{
    public class GameRunner
    {
        private readonly IUserInterface _userInterface;
        private readonly IFieldLogic _fieldLogic;
        private readonly IAnimalLogic _animalLogic;
        private readonly ICoordinatesLogic _coordinatesLogic;

        public GameRunner()
        {
            _userInterface = new ConsoleUserInterface();
            _fieldLogic = new FieldLogic();
            _animalLogic = new AnimalLogic();
            _coordinatesLogic = new CoordinatesLogic();
        }

        public void Start()
        {
            var animalsInput = _userInterface.GetAnimalInput();
            List<Animal> animals = _coordinatesLogic.InitAnimalCoordinates(animalsInput);
            Field field = _fieldLogic.InitField(animals);
            _userInterface.PrintField(field);
            //foreach (var animal in animals)
            //{
            //    Coordinates coordinates = _animalLogic.MoveRandomly(field);
            //}

            Console.ReadLine();
        }
    }
}
