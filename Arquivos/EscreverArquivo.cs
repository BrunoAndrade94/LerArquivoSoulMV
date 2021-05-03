﻿using ConsoleApp1.Abstratas;
using ConsoleApp1.Entidades;
using ConsoleApp1.Interfaces;
using ConsoleApp1.Views;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Arquivos
{
    class EscreverArquivo : AArquivo
    {
        // consegue escrever em arquivo .csv
        public static void Escrever<T>(IEnumerable<T> lista)
        {
            Produto[] p = lista.ToArray() as Produto[];
            using (StreamWriter sw = File.CreateText(R_CONS_PREV_KIT_COVID))
            {
                sw.WriteLine("Codigo;Nome;Consumo Previsto;Saldo Hospital");
                for (int i = 0; i < p.Length; i++)
                {
                    sw.WriteLine($"{p[i].Codigo};" +
                        $"{p[i].Nome};" +
                        $"{p[i].ConsumoPrevisto};" +
                        $"{p[i].SaldoHospital.ToString("F2", CultureInfo.InvariantCulture)}");
                }
            } // cria e escreve no arquivo em csv
            Console.WriteLine("\n\nArquivo R_CONS_PREV_KIT_COVID gerado com sucesso!");
        }
    }
}