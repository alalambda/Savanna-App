using SavannaApp.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SavannaApp.Interfaces
{
    public interface IAnimalLogic
    {
        void Move(Field field, Animal animal);
    }
}
