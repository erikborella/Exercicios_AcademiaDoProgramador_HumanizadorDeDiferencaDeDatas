using System;
using System.Collections.Generic;
using System.Text;

namespace PeriodosAtras.Periodo
{
    public class Humunizar : Dicionario
    {
        private DateTime data_inicial;
        private DateTime data_final;
        private TimeSpan dif;
        public string valor_saida;

        private int dias;
        private int semanas;
        private int meses;
        private int anos;
        private int horas;
        private int minutos;
        private int segundos;

        public Humunizar(DateTime data_inicial, DateTime data_final)
        {
            this.data_inicial = data_inicial;
            this.data_final = data_final;
            this.dif = data_inicial - data_final;
            int total_dias = Convert.ToInt32(dif.TotalDays);
            int total_segundos = Convert.ToInt32(dif.TotalSeconds);

            if (total_dias == 0)
            {
                CalcularPorSegundos(total_segundos);
                HumanizarSegundos();
            }
            else
            {
                CalcularPorDias(total_dias);
                HumanizarDias();
            }
        }

        private void HumanizarDias()
        {
            string valor_humanizado = "";

            if (anos != 0)
            {
                valor_humanizado += ConverterCentenaParaExtenso(anos) + " ";

                valor_humanizado += (anos == 1) ?
                 prefixosDeDiasEmStringSingular["ANO"] :
                 prefixosDeDiasEmStringPlural["ANO"];
            }

            if (meses != 0)
            {

                if (!string.IsNullOrEmpty(valor_humanizado))
                    valor_humanizado += " e ";

                valor_humanizado += ConverterCentenaParaExtenso(meses) + " ";
                valor_humanizado += (meses == 1) ?
                 prefixosDeDiasEmStringSingular["MES"] :
                 prefixosDeDiasEmStringPlural["MES"];
            }

            if (semanas != 0)
            {

                if (!string.IsNullOrEmpty(valor_humanizado))
                    valor_humanizado += " e ";

                valor_humanizado += ConverterCentenaParaExtenso(semanas, true) + " ";
                valor_humanizado += (semanas == 1) ?
                prefixosDeDiasEmStringSingular["SEMANA"] :
                prefixosDeDiasEmStringPlural["SEMANA"];

            }

            if (dias != 0)
            {

                if (!string.IsNullOrEmpty(valor_humanizado))
                    valor_humanizado += " e ";

                valor_humanizado += ConverterCentenaParaExtenso(dias) + " ";
                valor_humanizado += (dias == 1) ?
                prefixosDeDiasEmStringSingular["DIA"] :
                prefixosDeDiasEmStringPlural["DIA"];

            }
            valor_saida = valor_humanizado;

        }

        private void HumanizarSegundos()
        {
            string valor_humanizado = "";

            if (horas != 0)
            {
                valor_humanizado += ConverterCentenaParaExtenso(horas, true) + " ";

                valor_humanizado += (horas == 1) ?
                    prefixosDeSegundosEmStringSingular["HORA"] :
                    prefixosDeSegundosEmStringPlural["HORA"];
            }

            if (minutos != 0)
            {
                if (!string.IsNullOrEmpty(valor_humanizado))
                    valor_humanizado += " e ";

                valor_humanizado += ConverterCentenaParaExtenso(minutos) + " ";

                valor_humanizado += (minutos == 1) ?
                    prefixosDeSegundosEmStringSingular["MINUTO"] :
                    prefixosDeSegundosEmStringPlural["MINUTO"];
            }

            if (segundos != 0)
            {
                if (!string.IsNullOrEmpty(valor_humanizado))
                    valor_humanizado += " e ";

                valor_humanizado += ConverterCentenaParaExtenso(segundos) + " ";

                valor_humanizado += (segundos == 1) ?
                    prefixosDeSegundosEmStringSingular["SEGUNDO"] :
                    prefixosDeSegundosEmStringPlural["SEGUNDO"];
            }

            valor_saida = valor_humanizado;
        }

        public void CalcularPorSegundos(int total_segundos)
        {
            horas = total_segundos / 3600;
            total_segundos = total_segundos % 3600;

            minutos = total_segundos / 60;
            total_segundos = total_segundos % 60;

            segundos = total_segundos;
        }

        public void CalcularPorDias(int total_dias)
        {
            anos = CalcularTotalAnosPassados(ref total_dias);

            meses = CalcularTotalMesesPassados(ref total_dias);

            semanas = total_dias / 7;
            total_dias = total_dias % 7;

            dias = total_dias;
        }

        private int CalcularTotalAnosPassados(ref int total_dias)
        {
            int contador_de_anos_passados = 0;

            DateTime data = data_final;
            int dias_totais_do_ano = DateTime.IsLeapYear(data.Year) ? 366 : 365;
            
            while ((total_dias / dias_totais_do_ano) != 0)
            {
                contador_de_anos_passados++;
                total_dias -= dias_totais_do_ano;

                data = data.AddYears(1);
                dias_totais_do_ano = DateTime.IsLeapYear(data.Year) ? 366 : 365;
            }

            return contador_de_anos_passados;
        }

        private int CalcularTotalMesesPassados(ref int total_dias)
        {
            int contador_de_meses_passados = 0;

            DateTime data = data_final;

            while ((total_dias / DateTime.DaysInMonth(data.Year, data.Month)) != 0)
            {
                contador_de_meses_passados++;
                total_dias -= DateTime.DaysInMonth(data.Year, data.Month);

                data = data.AddMonths(1);
            }

            return contador_de_meses_passados;
        }

        private string ConverterCentenaParaExtenso(long n, bool ehFeminino = false)
        {
            if (n == 0)
                return "";

            if (n == 100)
                return "cem";

            if (ehFeminino && n == 1)
                return "uma";
            if (ehFeminino && n == 2)
                return "duas";

            int contador = 0;
            Stack<string> saida = new Stack<string>();

            long dezena = n % 100;

            if (DezenaEstaEntre10e20(dezena))
            {
                saida.Push(dezenasPorExtenso[dezena]);
                n /= 100;
                contador = 2;
            }

            while (n != 0)
            {
                long unidade = n % 10;
                n /= 10;
                contador++;

                if (unidade == 0)
                    continue;

                saida.Push(dicionariosPorUnidadeDeCentena[contador][unidade]);
            }

            return StackParaStringAdicionandoLigadura(saida, "e");
        }

        private string StackParaStringAdicionandoLigadura(Stack<string> s, string ligadura)
        {
            StringBuilder saida = new StringBuilder();
            string ligaduraUsada;

            if (ligadura == "")
                ligaduraUsada = " ";
            else
                ligaduraUsada = $" {ligadura} ";

            while (s.Count > 0)
            {
                string str = s.Pop();

                if (string.IsNullOrEmpty(str))
                    continue;

                saida.Append(str);

                if (s.Count != 0)
                    saida.Append(ligaduraUsada);
            }

            return saida.ToString();
        }

        private bool DezenaEstaEntre10e20(long dezena)
        {
            return dezena >= 10 && dezena < 20;
        }

    }
}
