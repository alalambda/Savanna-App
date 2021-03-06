﻿using Contracts;
using SavannaApp.Runner;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Threading;

namespace SavannaApp
{
    class Program
    {
        [ImportMany(typeof(IAnimal))]
        public List<IAnimal> Animals { get; set; }

        CompositionContainer _container;

        static void Main(string[] args)
        {
            var p = new Program();
            p.Run();

            new GameRunner().Start();
        }

        public void Run()
        {
            Compose();

            foreach (var animal in Animals)
            {
                Console.WriteLine(animal.Symbol);
            }
            Console.WriteLine();
            Thread.Sleep(2000);
        }

        public void Compose()
        {
            var catalog = new DirectoryCatalog(Environment.CurrentDirectory);
            _container = new CompositionContainer(catalog);
            _container.ComposeParts(this);
        }
    }
}