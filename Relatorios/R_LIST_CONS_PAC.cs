using System.Collections.Generic;
using ConsoleApp1.Abstratas;
using ConsoleApp1.Arquivos;
using ConsoleApp1.Entidades;
using ConsoleApp1.Interfaces;

namespace ConsoleApp1.Relatorios
{
     static class R_LIST_CONS_PAC
     {
        public static string Nome { get; private set; } = "R_LIST_CONS_PAC.csv";

        public static void Escrita(string arquivo, List<Produto> listaProduto)
        {
            AVerificar.ListaValida(listaProduto);
            Le_R_LIST_CONS_PAC(arquivo, listaProduto);
        }

        // le e adicionar produtos a lista
        private static void Le_R_LIST_CONS_PAC(string arquivo, List<Produto> listaProduto)
        {
            string[] vetor = arquivo.Split(';', '"');
            if (vetor.Length == 3 && vetor[2] == "")
            {
                string[] Codigo_Nome_Unidade = vetor[0].Split(',');
                listaProduto.Add(new Produto(int.Parse(Codigo_Nome_Unidade[0]),
                    Codigo_Nome_Unidade[1].ToLower(),
                    Codigo_Nome_Unidade[2].ToLower(),
                    double.Parse(vetor[1])));
                return;
            }
            if (vetor.Length == 5)
            {
                listaProduto.Add(new Produto(int.Parse(vetor[0].Trim(',')),
                    vetor[1].ToLower(),
                    vetor[2].Trim(',').ToLower(),
                    double.Parse(vetor[3])));
                return;
            }
            if (vetor.Length == 7)
            {
                // uma linha especifica
                if(vetor[2] == ",")
                {
                    listaProduto.Add(new Produto()
                    {
                        Codigo = int.Parse(vetor[0].Trim(',')),
                        Nome = vetor[1].ToLower(),
                        Unidade = vetor[3].ToLower(),
                        Consumo = double.Parse(vetor[5])
                    });
                    return;
                }
                // uma linha especifica
                else
                {
                    string[] vet1 = vetor[1].Split(',');
                    listaProduto.Add(new Produto()
                    {
                        Codigo = int.Parse(vet1[0].Trim(',')),
                        Nome = vet1[1].ToLower(),
                        Unidade = vet1[2].Trim(',').ToLower(),
                        Consumo = double.Parse(vetor[3])
                    });
                    return;
                }
            }
            if (vetor.Length == 15)
            {

                listaProduto.Add(new Produto()
                {
                    Codigo = int.Parse(vetor[1].Trim(',')),
                    Nome = vetor[3].ToLower(),
                    Unidade = vetor[7].Trim(',').ToLower(),
                    Consumo = double.Parse(vetor[11])
                });
                return;
            }
            if (vetor.Length == 11)
            {
                listaProduto.Add(new Produto()
                {
                    Codigo = int.Parse(vetor[1].Trim(',')),
                    Nome = vetor[3].ToLower(),
                    Unidade = vetor[5].Trim(',').ToLower(),
                    Consumo = double.Parse(vetor[7])
                });
                return;
            }

        }

        // verifica o nome do arquivo e armazena
        public static bool EhMeuNome(string nome)
        {
            LerArquivo.NomeArquivo = nome;
            return (Nome == nome) ? true : false;
        }
    }
}
