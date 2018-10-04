using SavannaApp.Constants;
using SavannaApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SavannaApp.Model
{
    public class Animal
    {
        public int VisionRange { get { return ConstantValues.VisionRange; } }
        public Coordinates Coordinates { get; set; }
    }
}
