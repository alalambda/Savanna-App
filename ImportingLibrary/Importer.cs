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
        //[ImportMany(typeof(IAnimal))]
        //private readonly List<Lazy<IAnimal>> _animals;

        public void Compose()
        {
            var catalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
            var container = new CompositionContainer(catalog);
            container.SatisfyImportsOnce(this);

            //An aggregate catalog that combines multiple catalogs
            //var catalog = new AggregateCatalog();

            //Add all the parts found in all assemblies in
            //the same directory as the executing program
            //catalog.Catalogs.Add(
            //    new DirectoryCatalog(
            //        Path.GetDirectoryName(
            //        Assembly.GetExecutingAssembly().Location
            //        )
            //    )
            //);

            //Create the CompositionContainer with the parts in the catalog.
            //CompositionContainer container = new CompositionContainer(catalog);

            //Fill the imports of this object
            //container.ComposeParts(this);
        }
    }
}
