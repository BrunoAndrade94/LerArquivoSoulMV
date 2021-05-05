using ConsoleApp1.Arquivos;
using ConsoleApp1.Entidades;
using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp1.Excessoes.Relatorio
{
    class RelatorioException : Exception
    {
        public RelatorioException(string Message) : base(Message) { }

        public string ArqNaoContemDados = "arquivo não contêm dados";
        public string ErroAoEscrever = "Erro ao Escrever!";

        public static void SeEhListaVazia<T>(IEnumerable<T> listaProduto)
        {
            // faz um casting para lista de produtos
            // e se tiver vazia é lançada uma execessão
            if ((listaProduto as List<Produto>).Count == 0) throw new RelatorioException(
                $"\n\n--- ATENÇÃO!\n\n  Erro ao ler. O arquivo { LerArquivo.NomeArquivo } " +
                $"não contêm dados\n  Por favor verifique o arquivo e tente novamente.");
        }
    }
}
