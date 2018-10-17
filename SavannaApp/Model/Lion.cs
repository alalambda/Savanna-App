using SavannaApp.Constants;
using SavannaApp.Interfaces;
using System.Composition;

namespace SavannaApp.Model
{
    //[Export(typeof(IAnimal))]
    public class Lion : Animal
    {
        public Lion()
        {
            MatingIndex = 0;
            Health = ConstantValues.Health;
            VisionRange = ConstantValues.VisionRange;
            Symbol = 'L';
        }

        public override bool IsPredator => true;
    }
}
