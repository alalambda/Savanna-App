using SavannaApp.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SavannaApp.Interfaces
{
    public interface IFieldLogic
    {
        Field GetNewField(Field field, List<Animal> animals);
    }
}
