using SavannaApp.Interfaces;

namespace SavannaApp.Model
{
    public abstract class Animal : IAnimal
    {
        public decimal Health { get; set; }
        public Coordinates Coordinates { get; set; }

        public int VisionRange { get; protected set; }
        public char Symbol { get; set; }

        public abstract bool IsPredator { get; }
    }
}
