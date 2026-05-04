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
            Size = new Size(980, 620);
            MinimumSize = new Size(900, 540);

            var menu = new MenuStrip();
            var cadastros = new ToolStripMenuItem("Cadastros");
            cadastros.DropDownItems.Add("Clientes", null, (s, e) => Abrir(new ClienteForm()));
            cadastros.DropDownItems.Add("Servicos", null, (s, e) => Abrir(new ServicoForm()));
            menu.Items.Add(cadastros);
            menu.Items.Add("Ordens de Servico", null, (s, e) => Abrir(new OrdemServicoForm()));
            menu.Items.Add("Relatorios", null, (s, e) => Abrir(new RelatorioOrdensForm()));
            MainMenuStrip = menu;
            Controls.Add(menu);

            var title = new Label
            {
                Dock = DockStyle.Top,
                Height = 80,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font(FontFamily.GenericSansSerif, 18, FontStyle.Bold),
                Text = "Gestao de Ordens de Servico"
            };

            var panel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(30),
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false
            };

            panel.Controls.Add(CriarBotao("Clientes", () => Abrir(new ClienteForm())));
            panel.Controls.Add(CriarBotao("Servicos", () => Abrir(new ServicoForm())));
            panel.Controls.Add(CriarBotao("Ordens de Servico", () => Abrir(new OrdemServicoForm())));
            panel.Controls.Add(CriarBotao("Relatorio Gerencial", () => Abrir(new RelatorioOrdensForm())));

            Controls.Add(panel);
            Controls.Add(title);
        }

        private static Button CriarBotao(string text, Action action)
        {
            var button = new Button
            {
                Text = text,
                Width = 260,
                Height = 42,
                Margin = new Padding(0, 0, 0, 12)
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
