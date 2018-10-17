using SavannaApp.Model;

namespace SavannaApp.Interfaces
{
    public interface IAnimal
    {
        decimal Health { get; set; }
        Coordinates Coordinates { get; set; }
        char Symbol { get; set; }
        int MatingIndex { get; set; }

        int VisionRange { get; }
        bool IsPredator { get; }
    }
}
