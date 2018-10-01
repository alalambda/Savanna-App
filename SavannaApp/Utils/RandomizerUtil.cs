using System;
using System.Collections.Generic;
using System.Text;

namespace SavannaApp.Utils
{
    public class RandomizerUtil
    {
        public int GetRandomInRange(int from, int to)
        {
            Random random = new Random();
            int value = random.Next(from, to);
            return value;
        }
    }
}
