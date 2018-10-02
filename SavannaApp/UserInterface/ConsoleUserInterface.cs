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
                    string outputValue;
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
            Console.WriteLine("Enter animals. A - antelope, L - lion. End your input with 0.");
        }

        public List<string> GetAnimalInput()
        {
            var animals = new List<string>();

            EnterAnimalsMessage();
            string input = Console.ReadLine();
            while (input != "0")
            {
                while (input != ConstantValues.Antelope && input != ConstantValues.Lion)
                {
                    InvalidInputMessage();
                    EnterAnimalsMessage();
                    input = Console.ReadLine();
                }
                animals.Add(input);
                input = Console.ReadLine();
            }
            
            return animals;
        }

        private void InvalidInputMessage()
        {
            Console.WriteLine("Invalid input.");
        }
    }
}
