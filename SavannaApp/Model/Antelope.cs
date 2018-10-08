using SavannaApp.Constants;
using SavannaApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SavannaApp.Model
{
    public class Antelope : Carnivore, IAnimal
    {
        public decimal Health { get; set; } = ConstantValues.Health;
        public int VisionRange { get; set; } = ConstantValues.VisionRange;
        public Coordinates Coordinates { get; set; }
        public char Symbol { get; set; } = 'A';
    }
}
