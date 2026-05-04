using System;
using GestaoOS.WinForms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GestaoOS.Tests
{
    [TestClass]
    public class OrdemServicoGridPresentationTests
    {
        [TestMethod]
        public void FormatarConclusao_RetornaTracosQuandoConclusaoNaoFoiInformada()
        {
            var texto = OrdemServicoGridPresentation.FormatarConclusao(null);

            Assert.AreEqual("--", texto);
        }

        [TestMethod]
        public void FormatarConclusao_RetornaDataCurtaQuandoConclusaoFoiInformada()
        {
            var texto = OrdemServicoGridPresentation.FormatarConclusao(new DateTime(2026, 5, 4));

            Assert.AreEqual("04/05/2026", texto);
        }
    }
}
