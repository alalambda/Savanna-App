using Newtonsoft.Json;
using SavannaApp.Factory;
using SavannaApp.Interfaces;
using SavannaApp.Logic;
using SavannaApp.Model;
using SavannaApp.UserInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SavannaApp.Runner
{
    public class GameRunner
    {
        private readonly AnimalFactory _animalFactory;

        private readonly List<IAnimal> _animals;

        private readonly IUserInterface _userInterface;

        private readonly IMovementLogic _movementLogic;

        public GameRunner()
        {
            _animalFactory = new AnimalFactory();
            _animals = new List<IAnimal>();

            _userInterface = new ConsoleUserInterface();
            _movementLogic = new MovementLogic();
        }

        public void Start()
        {
            ConsoleKeyInfo? keyPressedInfo = null;
            do
            {
                RefreshField();

                var animal = CreateAnimal(keyPressedInfo);
                if (animal != null)
                {
                    _animals.Add(animal);
                    _movementLogic.Spawn(animal, _animals);
                }

                while (Console.KeyAvailable == false && _animals.Any())
                {
                    MoveCarnivores();
                    Thread.Sleep(1000);

                    MovePredators();
                    Thread.Sleep(1000);
                }

                keyPressedInfo = Console.ReadKey(true);

            } while (keyPressedInfo.HasValue && keyPressedInfo.Value.Key != ConsoleKey.Escape);
        }

        private void MovePredators()
        {
            foreach (var predator in _animals.Where(a => a.IsPredator))
            {
                _movementLogic.Move(predator, _animals);
                RefreshField();
            }
        }

        private void MoveCarnivores()
        {
            foreach (var carnivore in _animals.Where(a => !a.IsPredator))
            {
                _movementLogic.Move(carnivore, _animals);
                RefreshField();
            }
        }

        private IAnimal CreateAnimal(ConsoleKeyInfo? keyPressedInfo)
        {
            if (!keyPressedInfo.HasValue)
                return null;

            var animalChar = char.ToUpper(keyPressedInfo.Value.KeyChar);
            return _animalFactory.CreateAnimal(animalChar);
        }

        private void RefreshField()
        {
            Console.Clear();
            _userInterface.PrintField(_animals);
        }
    }
}
