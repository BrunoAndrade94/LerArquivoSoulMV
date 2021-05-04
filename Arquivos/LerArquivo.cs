using ConsoleApp1.Entidades;
using ConsoleApp1.Abstratas;
using ConsoleApp1.Relatorios;
using System.Collections.Generic;
using System.IO;
using System;
using ConsoleApp1.Excessoes.Relatorio;
using ConsoleApp1.Interfaces;

namespace ConsoleApp1.Arquivos
{
    // erda de Abstract Arquivo que define o caminho do arquivo em pre compilação
    class LerArquivo : AArquivo
    {
        // armazena o nome do arquivo para verificar posteriormente
        public static string NomeArquivo { get; set; }

        // ponto de entrada
        // chamado por (R_CONS_PREV_KIT_COVID)
        public static List<Produto> Ler(string arquivo)
        {
            // cria lista de produtos
            var listaProduto = new List<Produto>();
            // le relatorio csv
            LeCSVArquivoRelatorio(arquivo, listaProduto);
            return listaProduto;
        }

        // retorna o nome do arquivo
        private static bool VerificaNomeArquivo(string arquivo)
        {
            var nome = new FileInfo(arquivo).Name;
            if (R_POS_EST_S.EhMeuNome(nome)) return true;
            if (R_LIST_CONS_PAC.EhMeuNome(nome)) return true;
            return false;
        }

        // responsavel por ler e gerar listas ou retorna nulo
        private static List<Produto> LeCSVArquivoRelatorio(string arquivo, List<Produto> listaProduto)
        {
            if (VerificaNomeArquivo(arquivo))
            {
                AbreELerArquivo(arquivo, listaProduto);
                RelatorioException.SeEhListaVazia(listaProduto);
                return listaProduto;
            }
            return null;
        }

        // abre e le qualquer arquivo
        private static void AbreELerArquivo(string arquivo, List<Produto> listaProduto)
        {
            using (var fs = new FileStream(arquivo, FileMode.Open, FileAccess.Read))
            {
                using (var ler = new StreamReader(fs))
                {
                    while (!ler.EndOfStream)
                    {
                        QualArquivo(listaProduto, ler);
                    }
                }
            }
        }

        // responsavel por definir o fluxo do programa, qual caminho do arquivo?
        private static void QualArquivo(List<Produto> listaProduto, StreamReader ler)
        {
            if (NomeArquivo == R_POS_EST_S.Nome) R_POS_EST_S.Escrita(ler.ReadLine(), listaProduto);
            if (NomeArquivo == R_LIST_CONS_PAC.Nome) R_LIST_CONS_PAC.Escrita(ler.ReadLine(), listaProduto);
        }
    }
}
