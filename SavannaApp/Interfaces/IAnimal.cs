using SavannaApp.Model;

namespace SavannaApp.Interfaces
{
    public interface IAnimal
    {
        decimal Health { get; set; }
        Coordinates Coordinates { get; set; }

        int VisionRange { get; }
        char Symbol { get; set; }
        bool IsPredator { get; }
    }
}
