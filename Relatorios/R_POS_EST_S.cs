using ConsoleApp1.Arquivos;
using ConsoleApp1.Entidades;
using ConsoleApp1.Abstratas;
using System.Collections.Generic;

namespace ConsoleApp1.Relatorios
{
    static class R_POS_EST_S
    {
        // nome do arquivo
        public static string Nome { get; private set; } = "R_POS_EST_S.csv";

        // entrada
        public static void Escrita(string arquivo, List<Produto> listaProduto)
        {
            AVerificar.ListaValida(listaProduto);
            Le_R_POS_EST_S(arquivo, listaProduto);
        }

        private static void Le_R_POS_EST_S(string arquivo, List<Produto> listaProduto)
        {
            string[] vetor = arquivo.Split(';', '"');
            if (vetor.Length == 11)
            {
                // verifica os produtos inativos
                if (vetor[0].Split('-').Length == 3) return;
                // separa a string
                string[] vet1 = vetor[0].Split('-');
                string[] vet2 = vet1[1].Split(',');
                // adiciona produto na lista
                listaProduto.Add(new Produto()
                {
                    Codigo = int.Parse(vet1[0].Trim(',')),
                    Unidade = vet2[2].ToLower(),
                    Nome = vet2[0].Trim().ToLower(),
                    SaldoHospital = double.Parse(vetor[1])
                });
                return;
            }
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
                return;
            }
            if (vetor.Length == 23)
            {
                string[] vet1 = vetor[1].Split('-');
                string[] vet2 = vet1[1].Split(',');
                listaProduto.Add(new Produto()
                {
                    Codigo = int.Parse(vet1[0].Trim(',')),
                    Unidade = vet2[2].ToLower(),
                    Nome = vet2[0].Trim().ToLower(),
                    SaldoHospital = double.Parse(vetor[3])
                });
            }
            if (vetor.Length == 27)
            {
                string[] vet1 = vetor[3].Split('-');
                listaProduto.Add(new Produto()
                {
                    Codigo = int.Parse(vet1[0].Trim()),
                    Nome = vet1[1].Trim().ToLower(),
                    Unidade = vetor[5].ToLower().Trim(','),
                    SaldoHospital = double.Parse(vetor[7])
                });
            }
            if (vetor.Length == 31)
            {
                string[] vet1 = vetor[3].Split('-');
                listaProduto.Add(new Produto()
                {
                    Codigo = int.Parse(vet1[0]),
                    Unidade = vetor[7].ToLower(),
                    Nome = vet1[1].Trim().ToLower(),
                    SaldoHospital = double.Parse(vetor[11])
                });
            }
        }

        public static bool EhMeuNome(string nome)
        {
            LerArquivo.NomeArquivo = nome;
            return (Nome == nome) ? true : false;
        }
    }
}
