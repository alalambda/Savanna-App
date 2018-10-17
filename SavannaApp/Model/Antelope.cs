using SavannaApp.Constants;
using SavannaApp.Interfaces;
using System.Composition;

namespace SavannaApp.Model
{
    //[Export(typeof(IAnimal))]
    public class Antelope : Animal
    {
        public Antelope()
        {
            MatingIndex = 0;
            Health = ConstantValues.Health;
            VisionRange = ConstantValues.VisionRange;
            Symbol = 'A';
        }

        public override bool IsPredator => false;
    }
}
