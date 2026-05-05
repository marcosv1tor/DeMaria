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

        private static readonly string[] EventBindings =
        {
            "_clientesButton.Click += ClientesMenuItem_Click;",
            "_clientesMenuItem.Click += ClientesMenuItem_Click;",
            "_servicosButton.Click += ServicosMenuItem_Click;",
            "_ordensButton.Click += OrdensMenuItem_Click;",
            "_relatorioButton.Click += RelatoriosMenuItem_Click;",
            "_pesquisarButton.Click += PesquisarButton_Click;",
            "_novoButton.Click += NovoButton_Click;",
            "_salvarButton.Click += SalvarButton_Click;",
            "_grid.SelectionChanged += Grid_SelectionChanged;",
            "_gerarButton.Click += GerarButton_Click;",
            "_pdfButton.Click += PdfButton_Click;"
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
                Assert.IsTrue(mainSource.Contains("ConectarEventos();"), form + " deve conectar eventos fora do Designer.");
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

        [TestMethod]
        public void Eventos_DaInterface_FicamForaDosArquivosDesigner()
        {
            var raiz = LocalizarRaizProjeto();
            var fontesPrincipais = Forms
                .Select(form => File.ReadAllText(Path.Combine(raiz, "src", "GestaoOS.WinForms", form + ".cs")))
                .ToList();
            var fontesDesigner = Forms
                .Select(form => File.ReadAllText(Path.Combine(raiz, "src", "GestaoOS.WinForms", form + ".Designer.cs")))
                .ToList();

            foreach (var binding in EventBindings)
            {
                Assert.IsTrue(fontesPrincipais.Any(source => source.Contains(binding)), binding + " deve estar no arquivo principal.");
                Assert.IsFalse(fontesDesigner.Any(source => source.Contains(binding)), binding + " nao deve ficar no Designer.");
            }
        }

        [TestMethod]
        public void FiltroAtivo_DeveIniciarEmTodosENaoFiltrarQuandoSemSelecao()
        {
            var raiz = LocalizarRaizProjeto();
            var clienteSource = File.ReadAllText(Path.Combine(raiz, "src", "GestaoOS.WinForms", "ClienteForm.cs"));
            var servicoSource = File.ReadAllText(Path.Combine(raiz, "src", "GestaoOS.WinForms", "ServicoForm.cs"));

            Assert.IsTrue(clienteSource.Contains("_filtroAtivo.SelectedIndex = 0;"), "ClienteForm deve iniciar filtro ativo com 'Todos'.");
            Assert.IsTrue(servicoSource.Contains("_filtroAtivo.SelectedIndex = 0;"), "ServicoForm deve iniciar filtro ativo com 'Todos'.");

            Assert.IsTrue(clienteSource.Contains("private static bool? ResolverFiltroAtivo(int selectedIndex)"), "ClienteForm deve resolver filtro ativo de forma defensiva.");
            Assert.IsTrue(servicoSource.Contains("private static bool? ResolverFiltroAtivo(int selectedIndex)"), "ServicoForm deve resolver filtro ativo de forma defensiva.");
            Assert.IsTrue(clienteSource.Contains("return null;"), "ClienteForm deve tratar sem selecao como sem filtro.");
            Assert.IsTrue(servicoSource.Contains("return null;"), "ServicoForm deve tratar sem selecao como sem filtro.");
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
