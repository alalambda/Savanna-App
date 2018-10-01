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
            for (int x = 0; x < field.DimX; x++)
            {
                for (int y = 0; y < field.DimY; y++)
                {
                    //Console.Write((int) field.Cells[x, y].State);
                }
                Console.WriteLine();
            }
        }

        public int GetUserInput(string paramName)
        {
            int dimension;
            Console.WriteLine($"{paramName} = ");
            string input = Console.ReadLine();
            while (!int.TryParse(input, out dimension))
            {
                InvalidInputMessage();
                Console.WriteLine($"{paramName} = ");
                input = Console.ReadLine();
            }
            return dimension;
        }

        private void InvalidInputMessage()
        {
            Console.WriteLine("Invalid input.");
        }
    }
}
