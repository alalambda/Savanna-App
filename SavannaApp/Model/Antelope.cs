using SavannaApp.Constants;

namespace SavannaApp.Model
{
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
