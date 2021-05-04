using ConsoleApp1.Entidades;
using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp1.Excessoes.Relatorio
{
    class RelatorioException : Exception
    {
        public RelatorioException() { }

        public static void SeEhListaVazia(List<Produto> listaProduto)
        {
            if (listaProduto.Count == 0) throw new RelatorioException();
        }
    }
}
