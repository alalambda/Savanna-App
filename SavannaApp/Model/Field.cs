using System;
using System.Collections.Generic;
using System.Text;

namespace SavannaApp.Model
{
    public class Field
    {
        public Coordinates Dimensions { get; set; }
        public Cell[,] Cells { get; set; }

        public Field(int x, int y)
        {
            Cells = new Cell[x, y];
            Dimensions = new Coordinates(x, y);
        }
    }
}
