using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GestaoOS.Tests
{
    [TestClass]
    public class RelatorioRdlcTests
    {
        [TestMethod]
        public void OrdensServicoReport_AgrupaPorClienteEExibeTotalPorCliente()
        {
            var document = XDocument.Load(LocalizarRdlc());
            var ns = document.Root.Name.Namespace;

            var clienteGroup = document
                .Descendants(ns + "Group")
                .SingleOrDefault(x => (string)x.Attribute("Name") == "ClienteGroup");

            Assert.IsNotNull(clienteGroup, "O relatorio deve ter um grupo visual por cliente.");
            Assert.IsTrue(clienteGroup.Descendants(ns + "GroupExpression").Any(x => x.Value == "=Fields!ClienteId.Value"));

            var valores = document.Descendants(ns + "Value").Select(x => x.Value).ToList();
            Assert.IsTrue(valores.Any(x => x.Contains("Total por cliente")));
            Assert.IsTrue(valores.Any(x => x.Contains("Sum(Fields!ValorTotal.Value, \"ClienteGroup\")")));
            Assert.IsTrue(valores.Any(x => x.Contains("CountDistinct(Fields!OrdemServicoId.Value, \"ClienteGroup\")")));
            Assert.IsTrue(valores.Any(x => x == "=Fields!StatusDescricao.Value"));
            Assert.IsTrue(valores.Any(x => x.Contains("Sum(Fields!ValorTotal.Value, \"OrdensServicoDataSet\") + 0")));
        }

        private static string LocalizarRdlc()
        {
            var diretorio = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            while (diretorio != null)
            {
                var caminho = Path.Combine(diretorio.FullName, "src", "GestaoOS.WinForms", "Reports", "OrdensServicoReport.rdlc");
                if (File.Exists(caminho))
                {
                    return caminho;
                }

                diretorio = diretorio.Parent;
            }

            Assert.Fail("Arquivo OrdensServicoReport.rdlc nao encontrado.");
            return null;
        }
    }
}
