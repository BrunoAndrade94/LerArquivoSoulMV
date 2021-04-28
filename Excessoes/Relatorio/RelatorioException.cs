using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Excessoes.Relatorio
{
    class RelatorioException : Exception
    {
        public RelatorioException(string mensagem) : base(mensagem) { }
    }
}
