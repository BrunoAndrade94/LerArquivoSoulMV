using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Abstratas
{
    // verifica se a lista é valida
    abstract class AVerificar
    {
        public static bool ListaValida<T>(List<T> NLProduto)
        {
            return (NLProduto == null) ? false : true;
        }
    }
}
