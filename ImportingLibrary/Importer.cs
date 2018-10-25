using Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Reflection;

namespace ImportingLibrary
{
    public class Importer
    {
        [ImportMany(typeof(IAnimal))]
        private readonly List<Lazy<IAnimal>> _animals;

        public void DoImport()
        {
            //An aggregate catalog that combines multiple catalogs
            var catalog = new AggregateCatalog();

            //Add all the parts found in all assemblies in
            //the same directory as the executing program
            catalog.Catalogs.Add(
                new DirectoryCatalog(
                    Path.GetDirectoryName(
                    Assembly.GetExecutingAssembly().Location
                    )
                )
            );

            //Create the CompositionContainer with the parts in the catalog.
            CompositionContainer container = new CompositionContainer(catalog);

            //Fill the imports of this object
            container.ComposeParts(this);
        }

        public int AvailableNumberOfAnimals
        {
            get { return _animals != null ? _animals.Count : 0; }
        }

        public List<string> CallAllComponents(params double[] args)
        {
            var result = new List<IAnimal>();

            foreach (Lazy<IAnimal> component in _animals)
            {
                Console.WriteLine(component.Value.Symbol);
                result.Add(component.Value);
            }

            return result;
        }
    }
}
