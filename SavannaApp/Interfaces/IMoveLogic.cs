using SavannaApp.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SavannaApp.Interfaces
{
    public interface IMoveLogic
    {
        Coordinates MakeRandomMove(Coordinates currentCoordinates, Field field);
    }
}
