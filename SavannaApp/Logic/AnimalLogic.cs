using SavannaApp.Constants;
using SavannaApp.Interfaces;
using SavannaApp.Model;
using System.Collections.Generic;
using System.Linq;

namespace SavannaApp.Logic
{
    public class AnimalLogic : IAnimalLogic
    {
        public IAnimal FindAnimalByCoordinates(IEnumerable<IAnimal> animals, Coordinates coordinates)
        {
            if (coordinates == null)
                return null;

            return animals?.Where(x => coordinates.Equals(x.Coordinates)).FirstOrDefault();
        }

        public IEnumerable<IAnimal> RemoveDeadAnimals(IEnumerable<IAnimal> animals)
        {
            if (animals.Any(x => x.Symbol == ConstantValues.Dead))
            {
                //TODO: refactor the remove part
                IAnimal animalToDead = animals.FirstOrDefault(x => x.Symbol == ConstantValues.Dead);
                var animalsList = animals.ToList();
                animalsList.Remove(animalToDead);
                return animalsList;
            }

            return animals;
        }
    }
}
