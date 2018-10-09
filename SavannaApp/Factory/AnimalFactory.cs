using SavannaApp.Interfaces;
using SavannaApp.Model;

namespace SavannaApp.Factory
{
    public class AnimalFactory
    {
        public IAnimal CreateAnimal(char animalChar)
        {
            switch (animalChar)
            {
                case 'A':
                    return new Antelope();
                case 'L':
                    return new Lion();
                default:
                    return null;
            }
        }
    }
}
