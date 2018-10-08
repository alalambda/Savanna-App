using SavannaApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SavannaApp.Model
{
    public class Predator : IAnimal
    {
        public decimal Health { get; set; }
        public int VisionRange { get; set; }
        public Coordinates Coordinates { get; set; }
        public char Symbol { get; set; }
    }
}
