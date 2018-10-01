using System;
using System.Collections.Generic;
using System.Text;

namespace SavannaApp.Model
{
    public class Lion : Animal 
    {
        public bool Catch(Animal animal)
        {
            return false;
        }

        public bool Eat(Animal animal)
        {
            return false;
        }
    }
}
