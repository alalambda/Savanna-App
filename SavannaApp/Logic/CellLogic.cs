using SavannaApp.Enum;
using SavannaApp.Interfaces;
using SavannaApp.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SavannaApp.Logic
{
    public class CellLogic : ICellLogic
    {
        public State GetCellState(Animal animal)
        {
            if (animal == null)
            {
                return State.Empty;
            }

            return typeof(Antelope) == animal.GetType() ? State.Antelope : State.Lion;
        }
    }
}
