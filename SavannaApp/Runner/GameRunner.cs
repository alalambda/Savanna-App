using SavannaApp.Factory;
using SavannaApp.Interfaces;
using SavannaApp.Logic;
using SavannaApp.UserInterface;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SavannaApp.Runner
{
    public class GameRunner
    {
        private readonly AnimalFactory _animalFactory;

        private readonly List<IAnimal> _animals;

        private readonly IUserInterface _userInterface;

        private readonly ICoordinatesLogic _coordinatesLogic;
        private readonly IAnimalLogic _animalLogic;

        public GameRunner()
        {
            _animalFactory = new AnimalFactory();

            _animals = new List<IAnimal>();

            _userInterface = new ConsoleUserInterface();

            _coordinatesLogic = new CoordinatesLogic();
            _animalLogic = new AnimalLogic();
        }

        public void Start()
        {
            ConsoleKeyInfo? keyPressedInfo = null;
            do
            {
                RefreshField();

                CharToAnimal(keyPressedInfo);

                AssignCoordinates();

                keyPressedInfo = Console.ReadKey(true);

            } while (keyPressedInfo.HasValue && keyPressedInfo.Value.Key != ConsoleKey.Escape);
        }

        private void CharToAnimal(ConsoleKeyInfo? keyPressedInfo)
        {
            if (keyPressedInfo.HasValue)
            {
                char animalChar = char.ToUpper(keyPressedInfo.Value.KeyChar);
                var newAnimal = _animalFactory.CreateAnimal(animalChar);
                if (newAnimal != null)
                    _animals.Add(newAnimal);
            }
        }

        private void AssignCoordinates()
        {
            while (Console.KeyAvailable == false && _animals.Count != 0)
            {
                _animals.Reverse();
                foreach (var animal in _animals)
                {
                    animal.Coordinates = _coordinatesLogic.Move(animal, _animals);
                }
                _animals.Reverse();
                Thread.Sleep(1000);
                RefreshField();
            }
        }

        private void RefreshField()
        {
            Console.Clear();
            _userInterface.PrintField(_animals);
        }
    }
}
