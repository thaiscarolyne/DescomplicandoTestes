﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DescomplicandoTestes.Model
{
    public class Prova
    {
        public string Nome_Prova { get; set; }

        public int Valor { get; set; }

        public Prova(string nomeprova, int valor)
        {
            Nome_Prova = nomeprova;
            Valor = valor;
        }
    }
}
