﻿using SavannaApp.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SavannaApp.Interfaces
{
    public interface IUserInterface
    {
        void PrintField(Field field);
        List<string> GetAnimalInput();

    }
}
