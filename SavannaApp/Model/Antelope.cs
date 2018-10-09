using SavannaApp.Constants;
using SavannaApp.Interfaces;

namespace SavannaApp.Model
{
    public class Antelope : Animal
    {
        public Antelope()
        {
            Health = ConstantValues.Health;
            VisionRange = ConstantValues.VisionRange;
            Symbol = 'A';
        }

        public override bool IsPredator => false;
    }
}
