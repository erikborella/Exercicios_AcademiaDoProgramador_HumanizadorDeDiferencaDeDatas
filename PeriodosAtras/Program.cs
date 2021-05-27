using System;
using PeriodosAtras.Periodo;

namespace PeriodosAtras
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(new Humunizar(DateTime.Parse("26/05/2021"), DateTime.Parse("24/04/2021")).valor_saida);
        }
    }
}

