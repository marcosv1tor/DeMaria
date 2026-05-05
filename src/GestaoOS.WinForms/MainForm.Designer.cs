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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this._menu = new System.Windows.Forms.MenuStrip();
            this._cadastrosMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._clientesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._servicosMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._ordensMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._relatoriosMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._root = new System.Windows.Forms.TableLayoutPanel();
            this._title = new System.Windows.Forms.Label();
            this._modules = new System.Windows.Forms.GroupBox();
            this._modulesGrid = new System.Windows.Forms.TableLayoutPanel();
            this._clientesButton = new System.Windows.Forms.Button();
            this._servicosButton = new System.Windows.Forms.Button();
            this._ordensButton = new System.Windows.Forms.Button();
            this._relatorioButton = new System.Windows.Forms.Button();
            this._menu.SuspendLayout();
            this._root.SuspendLayout();
            this._modules.SuspendLayout();
            this._modulesGrid.SuspendLayout();
            this.SuspendLayout();
            // 
            // _menu
            // 
            this._menu.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this._menu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this._menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._cadastrosMenuItem,
            this._ordensMenuItem,
            this._relatoriosMenuItem});
            this._menu.Location = new System.Drawing.Point(0, 0);
            this._menu.Name = "_menu";
            this._menu.Size = new System.Drawing.Size(1211, 33);
            this._menu.TabIndex = 1;
            // 
            // _cadastrosMenuItem
            // 
            this._cadastrosMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._clientesMenuItem,
            this._servicosMenuItem});
            this._cadastrosMenuItem.Name = "_cadastrosMenuItem";
            this._cadastrosMenuItem.Size = new System.Drawing.Size(107, 29);
            this._cadastrosMenuItem.Text = "Cadastros";
            // 
            // _clientesMenuItem
            // 
            this._clientesMenuItem.Name = "_clientesMenuItem";
            this._clientesMenuItem.Size = new System.Drawing.Size(179, 34);
            this._clientesMenuItem.Text = "Clientes";
            // 
            // _servicosMenuItem
            // 
            this._servicosMenuItem.Name = "_servicosMenuItem";
            this._servicosMenuItem.Size = new System.Drawing.Size(179, 34);
            this._servicosMenuItem.Text = "Servicos";
            // 
            // _ordensMenuItem
            // 
            this._ordensMenuItem.Name = "_ordensMenuItem";
            this._ordensMenuItem.Size = new System.Drawing.Size(173, 29);
            this._ordensMenuItem.Text = "Ordens de Servico";
            // 
            // _relatoriosMenuItem
            // 
            this._relatoriosMenuItem.Name = "_relatoriosMenuItem";
            this._relatoriosMenuItem.Size = new System.Drawing.Size(106, 29);
            this._relatoriosMenuItem.Text = "Relatorios";
            // 
            // _root
            // 
            this._root.ColumnCount = 1;
            this._root.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this._root.Controls.Add(this._title, 0, 0);
            this._root.Controls.Add(this._modules, 0, 1);
            this._root.Dock = System.Windows.Forms.DockStyle.Fill;
            this._root.Location = new System.Drawing.Point(0, 33);
            this._root.Name = "_root";
            this._root.Padding = new System.Windows.Forms.Padding(24, 18, 24, 24);
            this._root.RowCount = 2;
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 86F));
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._root.Size = new System.Drawing.Size(1211, 656);
            this._root.TabIndex = 0;
            // 
            // _title
            // 
            this._title.Dock = System.Windows.Forms.DockStyle.Fill;
            this._title.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this._title.Location = new System.Drawing.Point(27, 18);
            this._title.Name = "_title";
            this._title.Size = new System.Drawing.Size(1157, 86);
            this._title.TabIndex = 0;
            this._title.Text = "Gestao de Ordens de Servico";
            this._title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _modules
            // 
            this._modules.BackColor = System.Drawing.Color.White;
            this._modules.Controls.Add(this._modulesGrid);
            this._modules.Dock = System.Windows.Forms.DockStyle.Fill;
            this._modules.Location = new System.Drawing.Point(27, 107);
            this._modules.Name = "_modules";
            this._modules.Padding = new System.Windows.Forms.Padding(10, 8, 10, 10);
            this._modules.Size = new System.Drawing.Size(1157, 522);
            this._modules.TabIndex = 1;
            this._modules.TabStop = false;
            this._modules.Text = "Modulos";
            // 
            // _modulesGrid
            // 
            this._modulesGrid.ColumnCount = 2;
            this._modulesGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._modulesGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._modulesGrid.Controls.Add(this._clientesButton, 0, 0);
            this._modulesGrid.Controls.Add(this._servicosButton, 1, 0);
            this._modulesGrid.Controls.Add(this._ordensButton, 0, 1);
            this._modulesGrid.Controls.Add(this._relatorioButton, 1, 1);
            this._modulesGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._modulesGrid.Location = new System.Drawing.Point(10, 32);
            this._modulesGrid.Name = "_modulesGrid";
            this._modulesGrid.Padding = new System.Windows.Forms.Padding(20);
            this._modulesGrid.RowCount = 2;
            this._modulesGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._modulesGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._modulesGrid.Size = new System.Drawing.Size(1137, 480);
            this._modulesGrid.TabIndex = 0;
            // 
            // _clientesButton
            // 
            this._clientesButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this._clientesButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this._clientesButton.Font = new System.Drawing.Font("Segoe UI", 11F);
            this._clientesButton.Location = new System.Drawing.Point(36, 36);
            this._clientesButton.Margin = new System.Windows.Forms.Padding(16);
            this._clientesButton.Name = "_clientesButton";
            this._clientesButton.Size = new System.Drawing.Size(516, 188);
            this._clientesButton.TabIndex = 0;
            this._clientesButton.Text = "Clientes";
            // 
            // _servicosButton
            // 
            this._servicosButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this._servicosButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this._servicosButton.Font = new System.Drawing.Font("Segoe UI", 11F);
            this._servicosButton.Location = new System.Drawing.Point(584, 36);
            this._servicosButton.Margin = new System.Windows.Forms.Padding(16);
            this._servicosButton.Name = "_servicosButton";
            this._servicosButton.Size = new System.Drawing.Size(517, 188);
            this._servicosButton.TabIndex = 1;
            this._servicosButton.Text = "Servicos";
            // 
            // _ordensButton
            // 
            this._ordensButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this._ordensButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this._ordensButton.Font = new System.Drawing.Font("Segoe UI", 11F);
            this._ordensButton.Location = new System.Drawing.Point(36, 256);
            this._ordensButton.Margin = new System.Windows.Forms.Padding(16);
            this._ordensButton.Name = "_ordensButton";
            this._ordensButton.Size = new System.Drawing.Size(516, 188);
            this._ordensButton.TabIndex = 2;
            this._ordensButton.Text = "Ordens de Servico";
            // 
            // _relatorioButton
            // 
            this._relatorioButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this._relatorioButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this._relatorioButton.Font = new System.Drawing.Font("Segoe UI", 11F);
            this._relatorioButton.Location = new System.Drawing.Point(584, 256);
            this._relatorioButton.Margin = new System.Windows.Forms.Padding(16);
            this._relatorioButton.Name = "_relatorioButton";
            this._relatorioButton.Size = new System.Drawing.Size(517, 188);
            this._relatorioButton.TabIndex = 3;
            this._relatorioButton.Text = "Relatorio Gerencial";
            // 
            // MainForm
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1211, 689);
            this.Controls.Add(this._root);
            this.Controls.Add(this._menu);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this._menu;
            this.MinimumSize = new System.Drawing.Size(900, 540);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestao de Ordens de Servico";
            this._menu.ResumeLayout(false);
            this._menu.PerformLayout();
            this._root.ResumeLayout(false);
            this._modules.ResumeLayout(false);
            this._modulesGrid.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}

