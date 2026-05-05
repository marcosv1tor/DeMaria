using System;
using GestaoOS.Domain.Enums;
using GestaoOS.WinForms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GestaoOS.Tests
{
    [TestClass]
    public class RelatorioFiltroTests
    {
        [TestMethod]
        public void Criar_QuandoPeriodoNaoEstaMarcado_NaoFiltraPorDatas()
        {
            var filtro = RelatorioFiltroFactory.Criar(
                new DateTime(2026, 4, 4),
                new DateTime(2026, 5, 4),
                false,
                3,
                StatusOrdemServico.Concluida);

            Assert.IsNull(filtro.DataInicio);
            Assert.IsNull(filtro.DataFim);
            Assert.AreEqual(3, filtro.ClienteId);
            Assert.AreEqual(StatusOrdemServico.Concluida, filtro.Status);
        }

        [TestMethod]
        public void Criar_QuandoPeriodoEstaMarcado_FiltraPorDatas()
        {
            var filtro = RelatorioFiltroFactory.Criar(
                new DateTime(2026, 4, 4, 15, 20, 0),
                new DateTime(2026, 5, 4, 18, 30, 0),
                true,
                null,
                "Todos");

            Assert.AreEqual(new DateTime(2026, 4, 4), filtro.DataInicio);
            Assert.AreEqual(new DateTime(2026, 5, 4), filtro.DataFim);
            Assert.IsNull(filtro.ClienteId);
            Assert.IsNull(filtro.Status);
        }
    }
}
