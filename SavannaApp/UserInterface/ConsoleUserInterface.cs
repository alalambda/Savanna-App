﻿using Contracts;
using SavannaApp.Constants;
using SavannaApp.Interfaces;
using SavannaApp.Logic;
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
            var StringBuilder = new StringBuilder();
            for (int y = 0; y < ConstantValues.FieldDimensionY; y++)
            {
                for (int x = 0; x < ConstantValues.FieldDimensionX; x++)
                {
                    var coordinates = new Coordinates(x, y);
                    var animal = _animalLogic.FindAnimalByCoordinates(animals, coordinates);
                    if (animal != null)
                    {
                        StringBuilder.Append($"{animal.Symbol} ");
                        //if (animal.Symbol == 'A')
                        //    Console.BackgroundColor = ConsoleColor.DarkGreen;
                        //else
                        //    Console.BackgroundColor = ConsoleColor.DarkRed;

                        //Console.Write($"{animal.Symbol} ");
                        
                    }
                    else
                    {
                        StringBuilder.Append($"{ConstantValues.Empty} ");
                        //Console.BackgroundColor = ConsoleColor.Black;
                        //Console.Write($"{ConstantValues.Empty} ");
                    }
                }
                //Console.WriteLine();
                StringBuilder.AppendLine();
            }
            Console.Write(StringBuilder);
        }

        public char GetAnimalChar()
        {
            return Console.ReadKey(true).KeyChar;
        }
    }
}
