using System;
using System.Collections.Generic;
using System.Text;

namespace SavannaApp.Model
{
    public class Field
    {
        public int DimX { get; set; }
        public int DimY { get; set; }
        public Cell[,] Cells { get; set; }

        public Field(int x, int y)
        {
            DimX = x;
            DimY = y;
            Cells = new Cell[x, y];
        }
    }
}
