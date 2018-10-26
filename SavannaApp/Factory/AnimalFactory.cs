using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Contracts;

namespace SavannaApp.Factory
{
    public class AnimalFactory
    {
        public IAnimal CreateAnimal(char animalChar)
        {
            var availableChars = GetAvailableChars();

            foreach (var availableChar in availableChars)
            {
                if (availableChar == animalChar)
                {
                    var animal = GetAnimalInstance(availableChar);
                    return animal;
                }
            }
            return null;
        }

        private IAnimal GetAnimalInstance(char availableChar)
        {
            var implementations = GetImplementations();
            foreach (var implementation in implementations)
            {
                var symbol = implementation.GetProperty("Symbol");
                if (symbol != null)
                {
                    var animal = Activator.CreateInstance(implementation);
                    var animalChar = (char)symbol.GetValue(animal);
                    if (availableChar == animalChar)
                    {
                        return (IAnimal) animal;
                    }
                }
            }
            return null;
        }

        private List<char> GetAvailableChars()
        {
            var implementations = GetImplementations();

            var animalCharList = new List<char>();

            foreach (var implementation in implementations)
            {
                var symbol = implementation.GetProperty("Symbol");
                if (symbol != null)
                {
                    var animal = Activator.CreateInstance(implementation);
                    animalCharList.Add((char) symbol.GetValue(animal));
                }
            }

            return animalCharList;
        }

        private IEnumerable<Type> GetImplementations()
        {
            var parentType = typeof(IAnimal);
            var assembly = Assembly.GetExecutingAssembly();
            var types = assembly.GetTypes();

            var implementations = types.Where(t => t.GetInterfaces().Contains(parentType));

            return implementations;
        }
    }
}