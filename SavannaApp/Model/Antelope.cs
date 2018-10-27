using System.ComponentModel.Composition;
using Contracts;

namespace SavannaApp.Model
{
    [Export(typeof(IAnimal))]
    public class Antelope : IAnimal
    {
        public decimal Health { get; set; } = 50m;
        public Coordinates Coordinates { get; set; }
        public int MatingIndex { get; set; } = 0;
        public char Symbol { get; set; } = 'A';

        public int VisionRange { get; } = 4;
        public bool IsPredator { get; } = false;
    }
}
