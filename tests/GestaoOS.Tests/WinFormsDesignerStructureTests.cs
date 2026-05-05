using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GestaoOS.Tests
{
    [TestClass]
    public class WinFormsDesignerStructureTests
    {
        private static readonly string[] Forms =
        {
            "MainForm",
            "ClienteForm",
            "ServicoForm",
            "OrdemServicoForm",
            "RelatorioOrdensForm"
        };

        [TestMethod]
        public void Forms_DeInterface_UsamEstruturaDoWindowsFormsDesigner()
        {
            var raiz = LocalizarRaizProjeto();
            var projetoPath = Path.Combine(raiz, "src", "GestaoOS.WinForms", "GestaoOS.WinForms.csproj");
            var projeto = XDocument.Load(projetoPath);
            var ns = projeto.Root.Name.Namespace;

            foreach (var form in Forms)
            {
                var mainPath = Path.Combine(raiz, "src", "GestaoOS.WinForms", form + ".cs");
                var designerPath = Path.Combine(raiz, "src", "GestaoOS.WinForms", form + ".Designer.cs");
                var resxPath = Path.Combine(raiz, "src", "GestaoOS.WinForms", form + ".resx");

                Assert.IsTrue(File.Exists(designerPath), form + " deve ter arquivo .Designer.cs.");
                Assert.IsTrue(File.Exists(resxPath), form + " deve ter arquivo .resx para o Designer.");

                var mainSource = File.ReadAllText(mainPath);
                var designerSource = File.ReadAllText(designerPath);
                Assert.IsTrue(mainSource.Contains("partial class " + form), form + " deve ser partial no arquivo principal.");
                Assert.IsTrue(mainSource.Contains("InitializeComponent();"), form + " deve chamar InitializeComponent no construtor.");
                Assert.IsTrue(designerSource.Contains("partial class " + form), form + " deve ser partial no Designer.");
                Assert.IsTrue(designerSource.Contains("private void InitializeComponent()"), form + " deve declarar InitializeComponent no Designer.");
                Assert.IsFalse(designerSource.Contains("BuildLayout("), form + " nao deve chamar helper de montagem no Designer.");
                Assert.IsFalse(designerSource.Contains("UiStyle."), form + " nao deve depender de helpers customizados no Designer.");
                Assert.IsFalse(designerSource.Contains("=>"), form + " deve usar eventos nomeados no Designer.");
                Assert.IsFalse(designerSource.Contains("Configure"), form + " nao deve usar metodos auxiliares no Designer.");

                if (form != "MainForm")
                {
                    Assert.IsTrue(mainSource.Contains("UiStyle.IsDesignMode(this)"), form + " deve proteger carregamentos de dados em modo Designer.");
                }

                AssertHasDependentCompile(projeto, ns, form + ".Designer.cs", form + ".cs");
                AssertHasDependentResource(projeto, ns, form + ".resx", form + ".cs");
            }
        }

        private static void AssertHasDependentCompile(XDocument projeto, XNamespace ns, string include, string dependentUpon)
        {
            var item = projeto
                .Descendants(ns + "Compile")
                .SingleOrDefault(x => string.Equals((string)x.Attribute("Include"), include, StringComparison.OrdinalIgnoreCase));

            Assert.IsNotNull(item, include + " deve estar incluido no csproj.");
            Assert.AreEqual(dependentUpon, (string)item.Element(ns + "DependentUpon"));
        }

        private static void AssertHasDependentResource(XDocument projeto, XNamespace ns, string include, string dependentUpon)
        {
            var item = projeto
                .Descendants(ns + "EmbeddedResource")
                .SingleOrDefault(x => string.Equals((string)x.Attribute("Include"), include, StringComparison.OrdinalIgnoreCase));

            Assert.IsNotNull(item, include + " deve estar incluido como EmbeddedResource no csproj.");
            Assert.AreEqual(dependentUpon, (string)item.Element(ns + "DependentUpon"));
        }

        private static string LocalizarRaizProjeto()
        {
            var diretorio = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            while (diretorio != null)
            {
                if (File.Exists(Path.Combine(diretorio.FullName, "GestaoOS.sln")))
                {
                    return diretorio.FullName;
                }

                diretorio = diretorio.Parent;
            }

            Assert.Fail("Raiz do projeto nao encontrada.");
            return null;
        }
    }
}
