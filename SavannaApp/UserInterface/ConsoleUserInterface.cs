using SavannaApp.Constants;
using SavannaApp.Enum;
using SavannaApp.Interfaces;
using SavannaApp.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SavannaApp.UserInterface
{
    public class ConsoleUserInterface : IUserInterface
    {
        public void PrintField(Field field)
        {
            for (int y = 0; y < field.Dimensions.Y; y++)
            {
                for (int x = 0; x < field.Dimensions.X; x++)
                {
                    char outputValue;
                    if (field.Cells[x, y].State == State.Empty)
                        outputValue = ConstantValues.Empty;
                    else if (field.Cells[x, y].State == State.Antelope)
                        outputValue = ConstantValues.Antelope;
                    else if (field.Cells[x, y].State == State.Lion)
                        outputValue = ConstantValues.Lion;
                    else if (field.Cells[x, y].State == State.LionCatchesAntelope)
                        outputValue = ConstantValues.LionCatchesAntelope;
                    else outputValue = ConstantValues.LionEatsAntelope;

                    Console.Write($"{outputValue} ");
                }
                Console.WriteLine();
            }
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
