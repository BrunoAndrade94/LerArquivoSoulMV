using ConsoleApp1.Entidades;
using ConsoleApp1.Excessoes.Relatorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Relatorios
{
    static class R_POS_EST_S
    {
        public static string Nome { get; private set; } = "R_POS_EST_S.csv";


        public static void Escrita(string arquivo, List<Produto> listaProduto)
        {
            try
            {
                Verificar.ListaValida(listaProduto);
                Le11Linhas(arquivo, listaProduto);
                Le13Linhas(arquivo, listaProduto);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static List<Produto> Le11Linhas(string arquivo, List<Produto> listaProduto)
        {
            string[] vetor = arquivo.Split(';', '"');
            if (vetor.Length == 11)
            {
                // verifica os produtos inativos
                if (vetor[0].Split('-').Length == 3) return null;

                string[] vet1 = vetor[0].Split('-');
                string[] vet2 = vet1[1].Split(',');
                listaProduto.Add(new Produto()
                {
                    Codigo = int.Parse(vet1[0].Trim(',')),
                    Unidade = vet2[2].ToLower(),
                    Nome = vet2[0].Trim().ToLower(),
                    SaldoHospital = double.Parse(vetor[1])
                });
                return listaProduto;
            }
            return null;
        }

        private static List<Produto> Le13Linhas(string arquivo, List<Produto> listaProduto)
        {
            string[] vetor = arquivo.Split(';', '"');
            if (vetor.Length == 13)
            {
                string[] vet1 = null;
                // verfica o tamanho da string
                if (vetor[1].Split('-').Length == 2) vet1 = vetor[1].Split('-');
                if (vetor[0].Split('-').Length == 2 || vetor[0].Split('-').Length == 3) vet1 = vetor[0].Split('-');
                // adiciona produto na lista
                listaProduto.Add(new Produto()
                {
                    Codigo = int.Parse(vet1[0].Trim(',')),
                    Unidade = vetor[2].ToLower().Trim(),
                    Nome = vet1[1].Trim().ToLower(),
                    SaldoHospital = double.Parse(vetor[3])
                });
                return listaProduto;
            }
            return null;
        }
    }
}
