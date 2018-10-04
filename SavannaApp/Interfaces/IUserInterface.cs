using SavannaApp.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SavannaApp.Interfaces
{
    public interface IUserInterface
    {
        void PrintField(List<Animal> animals);
        char GetAnimalChar();
        void EnterAnimalsMessage();

    }
}
