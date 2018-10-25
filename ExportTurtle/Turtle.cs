using System.Composition;
using Contracts;

namespace ExportTurtle
{
    [Export(typeof(IAnimal))]
    public class Turtle : IAnimal
    {
        public decimal Health { get; set; } = 100m;
        public Coordinates Coordinates { get; set; }
        public int MatingIndex { get; set; } = 0;
        public char Symbol { get; set; } = 'T';

        public int VisionRange { get; } = 7;
        public bool IsPredator { get; } = false;
    }
}
