using System;
using GestaoOS.WinForms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GestaoOS.Tests
{
    [TestClass]
    public class OrdemServicoDateTimeTests
    {
        [TestMethod]
        public void ResolverDataAbertura_QuandoNovaOrdem_UsaDataSelecionadaComHoraDoSalvar()
        {
            var dataSelecionada = new DateTime(2026, 5, 4);
            var momentoSalvar = new DateTime(2026, 5, 4, 16, 42, 31);

            var dataAbertura = OrdemServicoDateTime.ResolverDataAbertura(dataSelecionada, momentoSalvar, true);

            Assert.AreEqual(new DateTime(2026, 5, 4, 16, 42, 31), dataAbertura);
        }

        [TestMethod]
        public void ResolverDataAbertura_QuandoOrdemExistente_PreservaDataHoraOriginal()
        {
            var dataOriginal = new DateTime(2026, 5, 4, 9, 15, 0);
            var momentoSalvar = new DateTime(2026, 5, 4, 16, 42, 31);

            var dataAbertura = OrdemServicoDateTime.ResolverDataAbertura(dataOriginal, momentoSalvar, false);

            Assert.AreEqual(dataOriginal, dataAbertura);
        }
    }
}
