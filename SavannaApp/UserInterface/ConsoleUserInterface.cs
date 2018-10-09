using SavannaApp.Constants;
using SavannaApp.Interfaces;
using SavannaApp.Logic;
using SavannaApp.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SavannaApp.UserInterface
{
    public class ConsoleUserInterface : IUserInterface
    {
        private readonly IAnimalLogic _animalLogic;

        public ConsoleUserInterface()
        {
            _animalLogic = new AnimalLogic();
        }

        public void PrintField(List<IAnimal> animals)
        {
            var stringToPrintOut = "";
            for (int y = 0; y < ConstantValues.FieldDimensionY; y++)
            {
                for (int x = 0; x < ConstantValues.FieldDimensionX; x++)
                {
                    var coordinates = new Coordinates(x, y);
                    var animal = _animalLogic.FindAnimalByCoordinates(animals, coordinates);
                    if (animal != null)
                    {
                        if (animal is Antelope antelope)
                        {
                            stringToPrintOut += $"{antelope.Symbol} ";
                        }
                        else if (animal is Lion lion)
                        {
                            stringToPrintOut += $"{lion.Symbol} ";
                        }
                    }
                    else
                    {
                        stringToPrintOut += $"{ConstantValues.Empty} ";
                    }
                }
                stringToPrintOut += "\r\n";
            }
            Console.Write(stringToPrintOut);
        }

        public void EnterAnimalsMessage()
        {
            Console.WriteLine("Enter animals. A - antelope, L - lion.");
        }

        public char GetAnimalChar()
        {
            return Console.ReadKey(true).KeyChar;
        }

        private void InvalidInputMessage()
        {
            Console.WriteLine("Invalid input.");
        }
    }
}
