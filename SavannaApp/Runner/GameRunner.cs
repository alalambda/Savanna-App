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
            Field field;
            var animals = new List<Animal>();

            do
            {
                field = _fieldLogic.MakeField(animals);
                Console.Clear();
                _userInterface.PrintField(field);

                while (Console.KeyAvailable == false && animals.Count != 0)
                {
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
                    field = _fieldLogic.MakeField(animals);
                    Thread.Sleep(1000);
                    Console.Clear();
                    _userInterface.PrintField(field);
                }

                char animalChar = _userInterface.GetAnimalChar();
                if (animalChar != ConstantValues.Antelope && animalChar != ConstantValues.Lion)
                {
                    continue;
                }

                var animalCoordinates = _coordinatesLogic.GenerateAnimalCoordinates();
                var animalState = animalChar == ConstantValues.Antelope ? State.Antelope : State.Lion;
                Animal unknownAnimal;
                if (animalState == State.Antelope)
                {
                    unknownAnimal = new Antelope() { Coordinates = animalCoordinates };
                }
                else
                {
                    unknownAnimal = new Lion() { Coordinates = animalCoordinates };
                }
                animals.Add(unknownAnimal);
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
            Console.ReadLine();
        }

        public Animal MoveAnimal(Animal animal, Field field)
        {
            //foreach (var animal in animals)
            //{
                Coordinates coordinates = _animalLogic.MakeRandomMove(animal.Coordinates, field);

                // free cell from animal
                int x = animal.Coordinates.X;
                int y = animal.Coordinates.Y;
                //field.Cells[x, y].Animal = null;
                //field.Cells[x, y].State = State.Empty;

                // assign animal new cell
                animal.Coordinates = coordinates;
                //field.Cells[coordinates.X, coordinates.Y].Animal = animal;

                //var state = typeof(Antelope) == animal.GetType() ? State.Antelope : State.Lion;
                //field.Cells[coordinates.X, coordinates.Y].State = state;
            //}
            return animal;
        }

        //public Field UpdateField(Animal animal, Field field)
        //{
        //    field.Cells[x, y].Animal = null;
        //    field.Cells[x, y].State = State.Empty;
        //    field.Cells[coordinates.X, coordinates.Y].Animal = animal;
        //    var state = typeof(Antelope) == animal.GetType() ? State.Antelope : State.Lion;
        //    field.Cells[coordinates.X, coordinates.Y].State = state;
        //    return field;
        //}
    }
}
