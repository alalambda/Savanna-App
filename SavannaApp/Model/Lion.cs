﻿using System.ComponentModel.Composition;
using Contracts;

namespace SavannaApp.Model
{
    [Export(typeof(IAnimal))]
    public class Lion : IAnimal
    {
        public decimal Health { get; set; } = 50m;
        public Coordinates Coordinates { get; set; }
        public int MatingIndex { get; set; } = 0;
        public char Symbol { get; set; } = 'L';

        public int VisionRange { get; } = 5;
        public bool IsPredator { get; } = true;
    }
}
