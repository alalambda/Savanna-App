using Contracts;
using System.Collections.Generic;

namespace SavannaApp.Interfaces
{
    public interface IUserInterface
    {
        void PrintField(List<IAnimal> animals);
        char GetAnimalChar();
    }
}
