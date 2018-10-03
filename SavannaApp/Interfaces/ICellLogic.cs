using SavannaApp.Enum;
using SavannaApp.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SavannaApp.Interfaces
{
    public interface ICellLogic
    {
        State DecideStateForCell(Animal animal);
    }
}
