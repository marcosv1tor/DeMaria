using System;
using System.Drawing;
using System.Windows.Forms;

namespace GestaoOS.WinForms
{
    public class MainForm : Form
    {
        public MainForm()
        {
            Text = "Gestao de Ordens de Servico";
            StartPosition = FormStartPosition.CenterScreen;
            UiStyle.ApplyForm(this, new Size(980, 620), new Size(900, 540));
            Padding = new Padding(0);

            var menu = new MenuStrip();
            var cadastros = new ToolStripMenuItem("Cadastros");
            cadastros.DropDownItems.Add("Clientes", null, (s, e) => Abrir(new ClienteForm()));
            cadastros.DropDownItems.Add("Servicos", null, (s, e) => Abrir(new ServicoForm()));
            menu.Items.Add(cadastros);
            menu.Items.Add("Ordens de Servico", null, (s, e) => Abrir(new OrdemServicoForm()));
            menu.Items.Add("Relatorios", null, (s, e) => Abrir(new RelatorioOrdensForm()));
            MainMenuStrip = menu;

            var root = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
                Padding = new Padding(24, 18, 24, 24)
            };
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 86));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            var title = new Label
            {
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                Text = "Gestao de Ordens de Servico"
            };

            var modules = UiStyle.GroupBox("Modulos");
            var grid = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 2, Padding = new Padding(20) };
            grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            grid.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            grid.RowStyles.Add(new RowStyle(SizeType.Percent, 50));

            grid.Controls.Add(CriarBotao("Clientes", () => Abrir(new ClienteForm())), 0, 0);
            grid.Controls.Add(CriarBotao("Servicos", () => Abrir(new ServicoForm())), 1, 0);
            grid.Controls.Add(CriarBotao("Ordens de Servico", () => Abrir(new OrdemServicoForm())), 0, 1);
            grid.Controls.Add(CriarBotao("Relatorio Gerencial", () => Abrir(new RelatorioOrdensForm())), 1, 1);
            modules.Controls.Add(grid);

            root.Controls.Add(title, 0, 0);
            root.Controls.Add(modules, 0, 1);
            Controls.Add(root);
            Controls.Add(menu);
        }

        private static Button CriarBotao(string text, Action action)
        {
            var button = new Button
            {
                Text = text,
                Dock = DockStyle.Fill,
                Margin = new Padding(16),
                FlatStyle = FlatStyle.System,
                Font = new Font("Segoe UI", 11, FontStyle.Regular)
            };
            button.Click += (s, e) => action();
            return button;
        }

        private static void Abrir(Form form)
        {
            form.ShowDialog();
        }
    }
}
