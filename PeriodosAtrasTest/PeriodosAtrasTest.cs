using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

using PeriodosAtras.Periodo;

namespace PeriodosAtrasTest
{
    [TestClass]
    public class PeriodosAtrasTest
    {
        [TestMethod]
        public void DeveVerificar30segundos()
        {
            DateTime dataInicial = DateTime.Parse("10/10/2020 15:00:30");
            DateTime dataFinal = DateTime.Parse("10/10/2020 15:00:00");

            Humunizar humunizar = new Humunizar(dataInicial, dataFinal);

            Assert.AreEqual("trinta segundos", humunizar.valor_saida);
        }

        [TestMethod]
        public void DeveVerificar1minutoe35segundos()
        {
            DateTime dataInicial = DateTime.Parse("10/10/2020 15:01:35");
            DateTime dataFinal = DateTime.Parse("10/10/2020 15:00:00");

            Humunizar humunizar = new Humunizar(dataInicial, dataFinal);

            Assert.AreEqual("um minuto e trinta e cinco segundos", humunizar.valor_saida);
        }

        [TestMethod]
        public void DeveVerificar1hora15minutos49segundos()
        {
            DateTime dataInicial = DateTime.Parse("10/10/2020 16:15:49");
            DateTime dataFinal = DateTime.Parse("10/10/2020 15:00:00");

            Humunizar humunizar = new Humunizar(dataInicial, dataFinal);

            Assert.AreEqual("uma hora e quinze minutos e quarenta e nove segundos", humunizar.valor_saida);
        }

        [TestMethod]
        public void DeveVerificar1dia()
        {
            DateTime dataInicial = DateTime.Parse("11/10/2020");
            DateTime dataFinal = DateTime.Parse("10/10/2020");

            Humunizar humunizar = new Humunizar(dataInicial, dataFinal);

            Assert.AreEqual("um dia", humunizar.valor_saida);
        }

        [TestMethod]
        public void DeveVerificar2semanas5dias()
        {
            DateTime dataInicial = DateTime.Parse("29/10/2020");
            DateTime dataFinal = DateTime.Parse("10/10/2020");

            Humunizar humunizar = new Humunizar(dataInicial, dataFinal);

            Assert.AreEqual("duas semanas e cinco dias", humunizar.valor_saida);
        }

        [TestMethod]
        public void DeveVerificar7meses2semanas6dias()
        {
            DateTime dataInicial = DateTime.Parse("21/08/2020");
            DateTime dataFinal = DateTime.Parse("01/01/2020");

            Humunizar humunizar = new Humunizar(dataInicial, dataFinal);

            Assert.AreEqual("sete meses e duas semanas e seis dias", humunizar.valor_saida);
        }

        [TestMethod]
        public void DeveVerificar3anos11meses3semanas5dias()
        {
            DateTime dataInicial = DateTime.Parse("26/12/2020");
            DateTime dataFinal = DateTime.Parse("01/01/2017");

            Humunizar humunizar = new Humunizar(dataInicial, dataFinal);

            Assert.AreEqual("tres anos e onze meses e tres semanas e cinco dias", humunizar.valor_saida);
        }
    }
}
