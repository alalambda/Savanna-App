using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SavannaApp.Interfaces;
using SavannaApp.Model;

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
            var properties = GetProperties();
            foreach (var property in properties)
            {
                if (property.Name == "Symbol")
                {
                    var animalType = property.ReflectedType;
                    var animal = Activator.CreateInstance(animalType);
                    var animalChar = (char) property.GetValue(animal);
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
            var properties = GetProperties();

            var animalCharList = new List<char>();
            foreach (var property in properties)
            {
                if (property.Name == "Symbol")
                {
                    var animalType = property.ReflectedType;
                    var animal = Activator.CreateInstance(animalType);
                    animalCharList.Add((char) property.GetValue(animal));
                }
            }

            return animalCharList;
        }

        private IEnumerable<PropertyInfo> GetProperties()
        {
            var properties = Assembly
                .GetAssembly(typeof(Animal))
                .GetTypes()
                .Where(t => t.IsSubclassOf(typeof(Animal)))
                .SelectMany(t => t.GetProperties());

            return properties;
        }
    }
}