using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace GestaoOS.WinForms
{
    public partial class MainForm
    {
        private IContainer components = null;

        private MenuStrip _menu;
        private ToolStripMenuItem _cadastrosMenuItem;
        private ToolStripMenuItem _clientesMenuItem;
        private ToolStripMenuItem _servicosMenuItem;
        private ToolStripMenuItem _ordensMenuItem;
        private ToolStripMenuItem _relatoriosMenuItem;
        private TableLayoutPanel _root;
        private Label _title;
        private GroupBox _modules;
        private TableLayoutPanel _modulesGrid;
        private Button _clientesButton;
        private Button _servicosButton;
        private Button _ordensButton;
        private Button _relatorioButton;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new Container();
            _menu = new MenuStrip();
            _menu.Name = "_menu";
            _cadastrosMenuItem = new ToolStripMenuItem();
            _cadastrosMenuItem.Name = "_cadastrosMenuItem";
            _clientesMenuItem = new ToolStripMenuItem();
            _clientesMenuItem.Name = "_clientesMenuItem";
            _servicosMenuItem = new ToolStripMenuItem();
            _servicosMenuItem.Name = "_servicosMenuItem";
            _ordensMenuItem = new ToolStripMenuItem();
            _ordensMenuItem.Name = "_ordensMenuItem";
            _relatoriosMenuItem = new ToolStripMenuItem();
            _relatoriosMenuItem.Name = "_relatoriosMenuItem";
            _root = new TableLayoutPanel();
            _root.Name = "_root";
            _title = new Label();
            _title.Name = "_title";
            _modules = new GroupBox();
            _modules.Name = "_modules";
            _modulesGrid = new TableLayoutPanel();
            _modulesGrid.Name = "_modulesGrid";
            _clientesButton = new Button();
            _clientesButton.Name = "_clientesButton";
            _servicosButton = new Button();
            _servicosButton.Name = "_servicosButton";
            _ordensButton = new Button();
            _ordensButton.Name = "_ordensButton";
            _relatorioButton = new Button();
            _relatorioButton.Name = "_relatorioButton";

            Text = "Gestao de Ordens de Servico";
            StartPosition = FormStartPosition.CenterScreen;
            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            BackColor = Color.FromArgb(245, 246, 248);
            Size = new Size(980, 620);
            MinimumSize = new Size(900, 540);
            Padding = new Padding(0);

            _clientesMenuItem.Text = "Clientes";
            _clientesMenuItem.Click += ClientesMenuItem_Click;
            _servicosMenuItem.Text = "Servicos";
            _servicosMenuItem.Click += ServicosMenuItem_Click;
            _cadastrosMenuItem.Text = "Cadastros";
            _cadastrosMenuItem.DropDownItems.AddRange(new ToolStripItem[] { _clientesMenuItem, _servicosMenuItem });

            _ordensMenuItem.Text = "Ordens de Servico";
            _ordensMenuItem.Click += OrdensMenuItem_Click;
            _relatoriosMenuItem.Text = "Relatorios";
            _relatoriosMenuItem.Click += RelatoriosMenuItem_Click;
            _menu.Items.AddRange(new ToolStripItem[] { _cadastrosMenuItem, _ordensMenuItem, _relatoriosMenuItem });
            MainMenuStrip = _menu;

            _root.Dock = DockStyle.Fill;
            _root.ColumnCount = 1;
            _root.RowCount = 2;
            _root.Padding = new Padding(24, 18, 24, 24);
            _root.RowStyles.Add(new RowStyle(SizeType.Absolute, 86));
            _root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            _title.Dock = DockStyle.Fill;
            _title.TextAlign = ContentAlignment.MiddleCenter;
            _title.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            _title.Text = "Gestao de Ordens de Servico";

            _modules.Text = "Modulos";
            _modules.Dock = DockStyle.Fill;
            _modules.BackColor = Color.White;
            _modules.Padding = new Padding(10, 8, 10, 10);

            _modulesGrid.Dock = DockStyle.Fill;
            _modulesGrid.ColumnCount = 2;
            _modulesGrid.RowCount = 2;
            _modulesGrid.Padding = new Padding(20);
            _modulesGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            _modulesGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            _modulesGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            _modulesGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            _clientesButton.Text = "Clientes";
            _clientesButton.Dock = DockStyle.Fill;
            _clientesButton.Margin = new Padding(16);
            _clientesButton.FlatStyle = FlatStyle.System;
            _clientesButton.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            _clientesButton.Click += ClientesMenuItem_Click;
            _servicosButton.Text = "Servicos";
            _servicosButton.Dock = DockStyle.Fill;
            _servicosButton.Margin = new Padding(16);
            _servicosButton.FlatStyle = FlatStyle.System;
            _servicosButton.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            _servicosButton.Click += ServicosMenuItem_Click;
            _ordensButton.Text = "Ordens de Servico";
            _ordensButton.Dock = DockStyle.Fill;
            _ordensButton.Margin = new Padding(16);
            _ordensButton.FlatStyle = FlatStyle.System;
            _ordensButton.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            _ordensButton.Click += OrdensMenuItem_Click;
            _relatorioButton.Text = "Relatorio Gerencial";
            _relatorioButton.Dock = DockStyle.Fill;
            _relatorioButton.Margin = new Padding(16);
            _relatorioButton.FlatStyle = FlatStyle.System;
            _relatorioButton.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            _relatorioButton.Click += RelatoriosMenuItem_Click;
            _modulesGrid.Controls.Add(_clientesButton, 0, 0);
            _modulesGrid.Controls.Add(_servicosButton, 1, 0);
            _modulesGrid.Controls.Add(_ordensButton, 0, 1);
            _modulesGrid.Controls.Add(_relatorioButton, 1, 1);
            _modules.Controls.Add(_modulesGrid);

            _root.Controls.Add(_title, 0, 0);
            _root.Controls.Add(_modules, 0, 1);
            Controls.Add(_root);
            Controls.Add(_menu);
        }
    }
}

