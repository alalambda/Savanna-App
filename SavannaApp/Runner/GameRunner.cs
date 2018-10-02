using SavannaApp.Constants;
using SavannaApp.Enum;
using SavannaApp.Interfaces;
using SavannaApp.Logic;
using SavannaApp.Model;
using SavannaApp.UserInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

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
            var animals = _coordinatesLogic.GenerateAnimalCoordinates(animalsInput);
            Field field;

            while(_animalLogic.CountAntelopes(animals) != 0)
            {
                Thread.Sleep(1000);
                Console.Clear();
                field = _fieldLogic.MakeField(animals);
                _userInterface.PrintField(field);
                foreach (var animal in animals)
                {
                    Coordinates coordinates = _animalLogic.MakeRandomMove(animal.Coordinates, field);

                    // free cell from animal
                    int x = animal.Coordinates.X;
                    int y = animal.Coordinates.Y;
                    field.Cells[x, y].Animal = null;
                    field.Cells[x, y].State = State.Empty;

                    // assign animal new cell
                    animal.Coordinates = coordinates;
                    field.Cells[coordinates.X, coordinates.Y].Animal = animal;
                    
                    var state = typeof(Antelope) == animal.GetType() ? State.Antelope : State.Lion;
                    field.Cells[coordinates.X, coordinates.Y].State = state;
                }   
            }
            Console.ReadLine();
        }
    }
}
