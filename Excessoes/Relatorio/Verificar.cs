﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Excessoes.Relatorio
{
    abstract class Verificar
    {
        public static bool ListaValida<T>(List<T> NLProduto)
        {
            return (NLProduto == null) ? false : true;
        }
    }
}