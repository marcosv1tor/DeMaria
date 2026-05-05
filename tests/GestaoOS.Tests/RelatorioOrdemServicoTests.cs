using GestaoOS.Domain.DTOs;
using GestaoOS.Domain.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GestaoOS.Tests
{
    [TestClass]
    public class RelatorioOrdemServicoTests
    {
        [TestMethod]
        public void StatusDescricao_RetornaNomeDoStatus()
        {
            var item = new RelatorioOrdemServico { Status = StatusOrdemServico.EmAndamento };

            Assert.AreEqual("EmAndamento", item.StatusDescricao);
        }
    }
}
