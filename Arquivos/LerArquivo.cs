using ConsoleApp1.Entidades;
using ConsoleApp1.Relatorios;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Arquivos
{
    static class LerArquivo
    {
        public static string C_R_POS_EST_S { get; private set; } = (@"G:\Visual_Studio\Documentos Texto\R_POS_EST_S.csv");
        public static string C_R_LIST_CONS_PAC { get; private set; } = (@"G:\Visual_Studio\Documentos Texto\R_LIST_CONS_PAC.csv");
        public static string NovoArquivoKitIntubacao { get; private set; } = (@"G:\Visual_Studio\Documentos Texto\Consumo_Kit_Intubacao.csv");

        // ainda nao testei
        public static List<Produto> Ler(string arquivo)
        {
            List<Produto> listaProduto = new List<Produto>();
            Escreve_R_POS_EST_S(arquivo, listaProduto);
            Escreve_R_LIST_CONS_PAC(arquivo, listaProduto);
            return listaProduto;
        }

        private static string NomeDoArquivo(string arquivo)
        {
            FileInfo fd = new FileInfo(arquivo);
            return fd.Name;
        }

        private static List<Produto> Escreve_R_POS_EST_S(string arquivo, List<Produto> listaProduto)
        {
            if (R_POS_EST_S.Nome == NomeDoArquivo(arquivo))
            {
                FileStream fs = new FileStream(arquivo, FileMode.Open);
                using (StreamReader ler = new StreamReader(fs))
                {
                    while (!ler.EndOfStream)
                    {
                        R_POS_EST_S.Escrita(ler.ReadLine(), listaProduto);
                    }
                }
                return listaProduto;
            }
            return null;
        }

        private static List<Produto> Escreve_R_LIST_CONS_PAC(string arquivo, List<Produto> listaProduto)
        {
            if (R_LIST_CONS_PAC.Nome == NomeDoArquivo(arquivo))
            {
                FileStream fs = new FileStream(arquivo, FileMode.Open);
                using (StreamReader ler = new StreamReader(fs))
                {
                    while (!ler.EndOfStream)
                    {
                        R_LIST_CONS_PAC.Escrita(ler.ReadLine(), listaProduto);
                    }
                }
                return listaProduto;
            }
            return null;
        }
    }
}
