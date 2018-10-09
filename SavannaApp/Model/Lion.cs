using SavannaApp.Constants;

namespace SavannaApp.Model
{
    public class Lion : Animal
    {
        public Lion()
        {
            Health = ConstantValues.Health;
            VisionRange = ConstantValues.VisionRange;
            Symbol = 'L';
        }

        public override bool IsPredator => true;
    }
}
