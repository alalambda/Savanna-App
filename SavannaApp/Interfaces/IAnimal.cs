using SavannaApp.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SavannaApp.Interfaces
{
    public interface IAnimal
    {
        decimal Health { get; set; }
        int VisionRange { get; set; }
        Coordinates Coordinates { get; set; }
        char Symbol { get; set; }
    }
}
