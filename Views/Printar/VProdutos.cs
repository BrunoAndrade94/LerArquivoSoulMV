using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Views.Printar
{
    class VProdutos
    {
        public static void Imprime_R_CONS_PREV_KIT_COVID<T>(string mensagem, IEnumerable<T> lista)
        {
            Console.WriteLine(mensagem);
            Console.WriteLine();
            foreach (T objeto in lista) Console.WriteLine(objeto);
        }
    }
}
