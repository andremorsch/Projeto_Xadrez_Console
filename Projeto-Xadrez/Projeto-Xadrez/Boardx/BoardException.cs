﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto_Xadrez.Boardx
{
    internal class BoardException : Exception
    {
        public BoardException(string message) : base(message)
        {
        }
    }
}
