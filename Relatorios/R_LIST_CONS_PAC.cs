using System;
using System.Collections.Generic;
using ConsoleApp1.Entidades;
using ConsoleApp1.Excessoes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Excessoes.Relatorio;
using ConsoleApp1.Interfaces;
using ConsoleApp1.Arquivos;

namespace ConsoleApp1.Relatorios
{
    static class R_LIST_CONS_PAC// finalizado
    {
        public static string Nome { get; private set; } = "R_LIST_CONS_PAC.csv";

        public static void Escrita(string arquivo, List<Produto> listaProduto)
        {
            Verificar.ListaValida(listaProduto);
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
            if (vetor.Length == 7 && vetor[0] == null)
            {
                string[] Codigo_Nome_Unidade = vetor[1].Split(',');
                listaProduto.Add(new Produto(int.Parse(Codigo_Nome_Unidade[0].Trim(',')),
                    Codigo_Nome_Unidade[1].ToLower(),
                    Codigo_Nome_Unidade[2].Trim(',').ToLower(),
                    double.Parse(vetor[3])));
                return;
            }
            if (vetor.Length == 7 && vetor[0] != null)
            {
                listaProduto.Add(new Produto(int.Parse(vetor[0].Trim(',')),
                    vetor[1].ToLower(),
                    vetor[3].Trim(',').ToLower(),
                    double.Parse(vetor[5])));
                return;
            }
        }

        public static bool EhMeuNome(string nome)
        {
            LerArquivo.NomeArquivo = nome;
            return (Nome == nome) ? true : false;
        }
    }
}
