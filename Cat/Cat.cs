using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Text;
using Contracts;

namespace Cat
{
    [Export(typeof(IAnimal))]
    class Cat : IAnimal
    {
        public decimal Health { get; set; } = 100m;
        public Coordinates Coordinates { get; set; }
        public int MatingIndex { get; set; } = 0;
        public char Symbol { get; set; } = 'C';

        public int VisionRange { get; } = 7;
        public bool IsPredator { get; } = true;
    }
}
