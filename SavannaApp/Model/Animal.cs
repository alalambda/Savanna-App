using SavannaApp.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace SavannaApp.Model
{
    public class Animal
    {
        public int VisionRange { get { return ConstantValues.VisionRange; } }
        public Coordinates Coordinates { get; set; }
        public char Symbol { get; set; }
    }
}
