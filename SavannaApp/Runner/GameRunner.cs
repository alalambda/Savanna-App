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

        public GameRunner()
        {
            _userInterface = new ConsoleUserInterface();
            _fieldLogic = new FieldLogic();
        }

        public void Start()
        {
            var animals = _userInterface.GetAnimalInput();

            int lionCount = 0;
            int antelopeCount = 0;
            foreach (var a in animals)
            {
                if (string.Equals(ConstantValues.Lion, a, StringComparison.OrdinalIgnoreCase)) lionCount++;
                else antelopeCount++;
            }
            Field field = _fieldLogic.InitAnimalsOnField(animals);
            _userInterface.PrintField(field);
            Console.ReadLine();
        }
    }
}
