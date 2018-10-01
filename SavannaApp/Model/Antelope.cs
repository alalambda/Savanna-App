using System;
using System.Collections.Generic;
using System.Text;

namespace SavannaApp.Model
{
    public class Antelope : Animal
    {
        public bool Avoid(Animal animal)
        {
            if (animal is Lion)
            {

            }
            return false;
        }
    }
}
