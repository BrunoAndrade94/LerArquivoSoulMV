using ConsoleApp1.Abstratas;
using ConsoleApp1.Entidades;
using ConsoleApp1.Interfaces;
using ConsoleApp1.Views;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using ConsoleApp1.Excessoes.Relatorio;

namespace ConsoleApp1.Arquivos
{
    class EscreverArquivo : ADiretorioArquivos
    {
        // consegue escrever em arquivo .csv
        public static void Escrever<T>(IEnumerable<T> lista)
        {
            var NomeArquivo = new FileInfo(R_CONS_PREV_KIT_COVID).Name;
            try
            {
                Produto[] listaProduto = lista.ToArray() as Produto[];
                RelatorioException.SeEhListaVazia(lista);
                EscreveR_CONS_PREV_KIT_COVID(listaProduto);
                Console.WriteLine($"\n\nArquivo {NomeArquivo} gerado com sucesso!");
            }
            catch (IOException)
            {
                Console.WriteLine($"\n\n--- ATENÇÃO!\n Erro ao escrever\n O arquivo {NomeArquivo}\n está em uso por outro programa.");
                Console.ReadLine();
                VMenu.Executar();
            }
        }

        private static void EscreveR_CONS_PREV_KIT_COVID(Produto[] listaProduto)
        {
            using (StreamWriter sw = File.CreateText(R_CONS_PREV_KIT_COVID))
            {
                sw.WriteLine("Codigo;Nome;Consumo Previsto;Saldo Hospital;Consumo");
                for (int i = 0; i < listaProduto.Length; i++)
                {
                    EscreverProduto(listaProduto, sw, i);
                }
            } // cria e escreve no arquivo em csv
        }

        // recebe os dados dos produtos e escreve no arquivo
        private static void EscreverProduto(Produto[] listaProduto, StreamWriter sw, int i)
        {
            sw.WriteLine(
                $"{listaProduto[i].Codigo};" +
                $"{listaProduto[i].Nome};" +
                $"{listaProduto[i].ConsumoPrevisto};" +
                $"{listaProduto[i].SaldoHospital.ToString("F0", CultureInfo.GetCultureInfo("pt-BR"))};" +
                $"{listaProduto[i].Consumo.ToString("F0", CultureInfo.GetCultureInfo("pt-BR"))}");
        }
    }
}
