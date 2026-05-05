using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace GestaoOS.WinForms
{
    public partial class ServicoForm
    {
        private IContainer components = null;
        private TableLayoutPanel _root;
        private GroupBox _filtrosGroup;
        private TableLayoutPanel _filtrosLayout;
        private TableLayoutPanel _pagerLayout;
        private GroupBox _editorGroup;
        private TableLayoutPanel _editorLayout;
        private FlowLayoutPanel _actionsPanel;
        private Label _filtroNomeLabel;
        private Label _filtroAtivoLabel;
        private Label _nomeLabel;
        private Label _valorBaseLabel;
        private Label _percentualImpostoLabel;
        private Button _pesquisarButton;
        private Button _novoButton;
        private Button _anteriorButton;
        private Button _proximaButton;
        private Button _salvarButton;
        private TextBox _filtroNome;
        private ComboBox _filtroAtivo;
        private DataGridView _grid;
        private TextBox _nome;
        private NumericUpDown _valorBase;
        private NumericUpDown _percentualImposto;
        private CheckBox _ativo;
        private Label _paginaLabel;
        private DataGridViewTextBoxColumn _idColumn;
        private DataGridViewTextBoxColumn _nomeColumn;
        private DataGridViewTextBoxColumn _valorBaseColumn;
        private DataGridViewTextBoxColumn _percentualImpostoColumn;
        private DataGridViewCheckBoxColumn _ativoColumn;

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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServicoForm));
            this._root = new System.Windows.Forms.TableLayoutPanel();
            this._filtrosGroup = new System.Windows.Forms.GroupBox();
            this._filtrosLayout = new System.Windows.Forms.TableLayoutPanel();
            this._filtroNomeLabel = new System.Windows.Forms.Label();
            this._filtroNome = new System.Windows.Forms.TextBox();
            this._filtroAtivoLabel = new System.Windows.Forms.Label();
            this._filtroAtivo = new System.Windows.Forms.ComboBox();
            this._pesquisarButton = new System.Windows.Forms.Button();
            this._novoButton = new System.Windows.Forms.Button();
            this._pagerLayout = new System.Windows.Forms.TableLayoutPanel();
            this._paginaLabel = new System.Windows.Forms.Label();
            this._anteriorButton = new System.Windows.Forms.Button();
            this._proximaButton = new System.Windows.Forms.Button();
            this._grid = new System.Windows.Forms.DataGridView();
            this._editorGroup = new System.Windows.Forms.GroupBox();
            this._editorLayout = new System.Windows.Forms.TableLayoutPanel();
            this._nomeLabel = new System.Windows.Forms.Label();
            this._nome = new System.Windows.Forms.TextBox();
            this._valorBaseLabel = new System.Windows.Forms.Label();
            this._valorBase = new System.Windows.Forms.NumericUpDown();
            this._percentualImpostoLabel = new System.Windows.Forms.Label();
            this._percentualImposto = new System.Windows.Forms.NumericUpDown();
            this._ativo = new System.Windows.Forms.CheckBox();
            this._actionsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this._salvarButton = new System.Windows.Forms.Button();
            this._root.SuspendLayout();
            this._filtrosGroup.SuspendLayout();
            this._filtrosLayout.SuspendLayout();
            this._pagerLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._grid)).BeginInit();
            this._editorGroup.SuspendLayout();
            this._editorLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._valorBase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._percentualImposto)).BeginInit();
            this._actionsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // _root
            // 
            this._root.ColumnCount = 1;
            this._root.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this._root.Controls.Add(this._filtrosGroup, 0, 0);
            this._root.Controls.Add(this._grid, 0, 1);
            this._root.Controls.Add(this._editorGroup, 0, 2);
            this._root.Dock = System.Windows.Forms.DockStyle.Fill;
            this._root.Location = new System.Drawing.Point(10, 10);
            this._root.Name = "_root";
            this._root.RowCount = 3;
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 122F));
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 185F));
            this._root.Size = new System.Drawing.Size(938, 584);
            this._root.TabIndex = 0;
            // 
            // _filtrosGroup
            // 
            this._filtrosGroup.BackColor = System.Drawing.Color.White;
            this._filtrosGroup.Controls.Add(this._filtrosLayout);
            this._filtrosGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this._filtrosGroup.Location = new System.Drawing.Point(3, 3);
            this._filtrosGroup.Name = "_filtrosGroup";
            this._filtrosGroup.Padding = new System.Windows.Forms.Padding(10, 8, 10, 10);
            this._filtrosGroup.Size = new System.Drawing.Size(932, 116);
            this._filtrosGroup.TabIndex = 0;
            this._filtrosGroup.TabStop = false;
            this._filtrosGroup.Text = "Pesquisa";
            // 
            // _filtrosLayout
            // 
            this._filtrosLayout.ColumnCount = 6;
            this._filtrosLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this._filtrosLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._filtrosLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this._filtrosLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this._filtrosLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 108F));
            this._filtrosLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this._filtrosLayout.Controls.Add(this._filtroNomeLabel, 0, 0);
            this._filtrosLayout.Controls.Add(this._filtroNome, 1, 0);
            this._filtrosLayout.Controls.Add(this._filtroAtivoLabel, 2, 0);
            this._filtrosLayout.Controls.Add(this._filtroAtivo, 3, 0);
            this._filtrosLayout.Controls.Add(this._pesquisarButton, 4, 0);
            this._filtrosLayout.Controls.Add(this._novoButton, 5, 0);
            this._filtrosLayout.Controls.Add(this._pagerLayout, 0, 1);
            this._filtrosLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this._filtrosLayout.Location = new System.Drawing.Point(10, 32);
            this._filtrosLayout.Name = "_filtrosLayout";
            this._filtrosLayout.RowCount = 2;
            this._filtrosLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this._filtrosLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this._filtrosLayout.Size = new System.Drawing.Size(912, 74);
            this._filtrosLayout.TabIndex = 0;
            // 
            // _filtroNomeLabel
            // 
            this._filtroNomeLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._filtroNomeLabel.Location = new System.Drawing.Point(3, 3);
            this._filtroNomeLabel.Margin = new System.Windows.Forms.Padding(3, 3, 6, 3);
            this._filtroNomeLabel.Name = "_filtroNomeLabel";
            this._filtroNomeLabel.Size = new System.Drawing.Size(51, 32);
            this._filtroNomeLabel.TabIndex = 0;
            this._filtroNomeLabel.Text = "Nome";
            this._filtroNomeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _filtroNome
            // 
            this._filtroNome.Dock = System.Windows.Forms.DockStyle.Fill;
            this._filtroNome.Location = new System.Drawing.Point(63, 3);
            this._filtroNome.Margin = new System.Windows.Forms.Padding(3, 3, 8, 3);
            this._filtroNome.MaxLength = 150;
            this._filtroNome.Name = "_filtroNome";
            this._filtroNome.Size = new System.Drawing.Size(428, 31);
            this._filtroNome.TabIndex = 1;
            // 
            // _filtroAtivoLabel
            // 
            this._filtroAtivoLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._filtroAtivoLabel.Location = new System.Drawing.Point(502, 3);
            this._filtroAtivoLabel.Margin = new System.Windows.Forms.Padding(3, 3, 6, 3);
            this._filtroAtivoLabel.Name = "_filtroAtivoLabel";
            this._filtroAtivoLabel.Size = new System.Drawing.Size(46, 32);
            this._filtroAtivoLabel.TabIndex = 2;
            this._filtroAtivoLabel.Text = "Ativo";
            this._filtroAtivoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _filtroAtivo
            // 
            this._filtroAtivo.Dock = System.Windows.Forms.DockStyle.Fill;
            this._filtroAtivo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._filtroAtivo.Items.AddRange(new object[] {
            "Todos",
            "Ativos",
            "Inativos"});
            this._filtroAtivo.Location = new System.Drawing.Point(557, 3);
            this._filtroAtivo.Margin = new System.Windows.Forms.Padding(3, 3, 8, 3);
            this._filtroAtivo.Name = "_filtroAtivo";
            this._filtroAtivo.Size = new System.Drawing.Size(139, 33);
            this._filtroAtivo.TabIndex = 3;
            // 
            // _pesquisarButton
            // 
            this._pesquisarButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this._pesquisarButton.Location = new System.Drawing.Point(708, 4);
            this._pesquisarButton.Margin = new System.Windows.Forms.Padding(4);
            this._pesquisarButton.Name = "_pesquisarButton";
            this._pesquisarButton.Size = new System.Drawing.Size(96, 30);
            this._pesquisarButton.TabIndex = 4;
            this._pesquisarButton.Text = "Pesquisar";
            this._pesquisarButton.UseVisualStyleBackColor = true;
            // 
            // _novoButton
            // 
            this._novoButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this._novoButton.Location = new System.Drawing.Point(816, 4);
            this._novoButton.Margin = new System.Windows.Forms.Padding(4);
            this._novoButton.Name = "_novoButton";
            this._novoButton.Size = new System.Drawing.Size(92, 30);
            this._novoButton.TabIndex = 5;
            this._novoButton.Text = "Novo";
            this._novoButton.UseVisualStyleBackColor = true;
            // 
            // _pagerLayout
            // 
            this._pagerLayout.ColumnCount = 3;
            this._filtrosLayout.SetColumnSpan(this._pagerLayout, 6);
            this._pagerLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._pagerLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 96F));
            this._pagerLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 96F));
            this._pagerLayout.Controls.Add(this._paginaLabel, 0, 0);
            this._pagerLayout.Controls.Add(this._anteriorButton, 1, 0);
            this._pagerLayout.Controls.Add(this._proximaButton, 2, 0);
            this._pagerLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this._pagerLayout.Location = new System.Drawing.Point(3, 41);
            this._pagerLayout.Name = "_pagerLayout";
            this._pagerLayout.RowCount = 1;
            this._pagerLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this._pagerLayout.Size = new System.Drawing.Size(906, 38);
            this._pagerLayout.TabIndex = 6;
            // 
            // _paginaLabel
            // 
            this._paginaLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._paginaLabel.Location = new System.Drawing.Point(3, 0);
            this._paginaLabel.Name = "_paginaLabel";
            this._paginaLabel.Size = new System.Drawing.Size(708, 38);
            this._paginaLabel.TabIndex = 0;
            this._paginaLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _anteriorButton
            // 
            this._anteriorButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this._anteriorButton.Location = new System.Drawing.Point(718, 2);
            this._anteriorButton.Margin = new System.Windows.Forms.Padding(4, 2, 4, 8);
            this._anteriorButton.Name = "_anteriorButton";
            this._anteriorButton.Size = new System.Drawing.Size(88, 28);
            this._anteriorButton.TabIndex = 1;
            this._anteriorButton.Text = "Anterior";
            this._anteriorButton.UseVisualStyleBackColor = true;
            // 
            // _proximaButton
            // 
            this._proximaButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this._proximaButton.Location = new System.Drawing.Point(814, 2);
            this._proximaButton.Margin = new System.Windows.Forms.Padding(4, 2, 4, 8);
            this._proximaButton.Name = "_proximaButton";
            this._proximaButton.Size = new System.Drawing.Size(88, 28);
            this._proximaButton.TabIndex = 2;
            this._proximaButton.Text = "Proxima";
            this._proximaButton.UseVisualStyleBackColor = true;
            // 
            // _grid
            // 
            this._grid.AllowUserToAddRows = false;
            this._grid.AllowUserToDeleteRows = false;
            this._grid.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._grid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this._grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._grid.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(241)))), ((int)(((byte)(245)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this._grid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this._grid.ColumnHeadersHeight = 32;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this._grid.DefaultCellStyle = dataGridViewCellStyle3;
            this._grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._grid.EnableHeadersVisualStyles = false;
            this._grid.Location = new System.Drawing.Point(3, 125);
            this._grid.MultiSelect = false;
            this._grid.Name = "_grid";
            this._grid.ReadOnly = true;
            this._grid.RowHeadersVisible = false;
            this._grid.RowHeadersWidth = 62;
            this._grid.RowTemplate.Height = 28;
            this._grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._grid.Size = new System.Drawing.Size(932, 271);
            this._grid.TabIndex = 1;
            // 
            // _editorGroup
            // 
            this._editorGroup.BackColor = System.Drawing.Color.White;
            this._editorGroup.Controls.Add(this._editorLayout);
            this._editorGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this._editorGroup.Location = new System.Drawing.Point(3, 402);
            this._editorGroup.Name = "_editorGroup";
            this._editorGroup.Padding = new System.Windows.Forms.Padding(10, 8, 10, 10);
            this._editorGroup.Size = new System.Drawing.Size(932, 179);
            this._editorGroup.TabIndex = 2;
            this._editorGroup.TabStop = false;
            this._editorGroup.Text = "Dados do servico";
            // 
            // _editorLayout
            // 
            this._editorLayout.ColumnCount = 4;
            this._editorLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this._editorLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._editorLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this._editorLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._editorLayout.Controls.Add(this._nomeLabel, 0, 0);
            this._editorLayout.Controls.Add(this._nome, 1, 0);
            this._editorLayout.Controls.Add(this._valorBaseLabel, 2, 0);
            this._editorLayout.Controls.Add(this._valorBase, 3, 0);
            this._editorLayout.Controls.Add(this._percentualImpostoLabel, 0, 1);
            this._editorLayout.Controls.Add(this._percentualImposto, 1, 1);
            this._editorLayout.Controls.Add(this._ativo, 2, 1);
            this._editorLayout.Controls.Add(this._actionsPanel, 0, 2);
            this._editorLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this._editorLayout.Location = new System.Drawing.Point(10, 32);
            this._editorLayout.Name = "_editorLayout";
            this._editorLayout.RowCount = 3;
            this._editorLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this._editorLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this._editorLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._editorLayout.Size = new System.Drawing.Size(912, 137);
            this._editorLayout.TabIndex = 0;
            // 
            // _nomeLabel
            // 
            this._nomeLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._nomeLabel.Location = new System.Drawing.Point(3, 3);
            this._nomeLabel.Margin = new System.Windows.Forms.Padding(3, 3, 6, 3);
            this._nomeLabel.Name = "_nomeLabel";
            this._nomeLabel.Size = new System.Drawing.Size(91, 28);
            this._nomeLabel.TabIndex = 0;
            this._nomeLabel.Text = "Nome";
            this._nomeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _nome
            // 
            this._nome.Dock = System.Windows.Forms.DockStyle.Fill;
            this._nome.Location = new System.Drawing.Point(103, 3);
            this._nome.Margin = new System.Windows.Forms.Padding(3, 3, 8, 3);
            this._nome.MaxLength = 150;
            this._nome.Name = "_nome";
            this._nome.Size = new System.Drawing.Size(335, 31);
            this._nome.TabIndex = 1;
            // 
            // _valorBaseLabel
            // 
            this._valorBaseLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._valorBaseLabel.Location = new System.Drawing.Point(449, 3);
            this._valorBaseLabel.Margin = new System.Windows.Forms.Padding(3, 3, 6, 3);
            this._valorBaseLabel.Name = "_valorBaseLabel";
            this._valorBaseLabel.Size = new System.Drawing.Size(111, 28);
            this._valorBaseLabel.TabIndex = 2;
            this._valorBaseLabel.Text = "Valor base";
            this._valorBaseLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _valorBase
            // 
            this._valorBase.DecimalPlaces = 2;
            this._valorBase.Dock = System.Windows.Forms.DockStyle.Fill;
            this._valorBase.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this._valorBase.Location = new System.Drawing.Point(569, 3);
            this._valorBase.Margin = new System.Windows.Forms.Padding(3, 3, 8, 3);
            this._valorBase.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this._valorBase.Name = "_valorBase";
            this._valorBase.Size = new System.Drawing.Size(335, 31);
            this._valorBase.TabIndex = 3;
            this._valorBase.ThousandsSeparator = true;
            // 
            // _percentualImpostoLabel
            // 
            this._percentualImpostoLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._percentualImpostoLabel.Location = new System.Drawing.Point(3, 37);
            this._percentualImpostoLabel.Margin = new System.Windows.Forms.Padding(3, 3, 6, 3);
            this._percentualImpostoLabel.Name = "_percentualImpostoLabel";
            this._percentualImpostoLabel.Size = new System.Drawing.Size(91, 28);
            this._percentualImpostoLabel.TabIndex = 4;
            this._percentualImpostoLabel.Text = "Imposto %";
            this._percentualImpostoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _percentualImposto
            // 
            this._percentualImposto.DecimalPlaces = 2;
            this._percentualImposto.Dock = System.Windows.Forms.DockStyle.Fill;
            this._percentualImposto.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this._percentualImposto.Location = new System.Drawing.Point(103, 37);
            this._percentualImposto.Margin = new System.Windows.Forms.Padding(3, 3, 8, 3);
            this._percentualImposto.Name = "_percentualImposto";
            this._percentualImposto.Size = new System.Drawing.Size(335, 31);
            this._percentualImposto.TabIndex = 5;
            // 
            // _ativo
            // 
            this._ativo.Checked = true;
            this._ativo.CheckState = System.Windows.Forms.CheckState.Checked;
            this._ativo.Dock = System.Windows.Forms.DockStyle.Fill;
            this._ativo.Location = new System.Drawing.Point(449, 37);
            this._ativo.Name = "_ativo";
            this._ativo.Size = new System.Drawing.Size(114, 28);
            this._ativo.TabIndex = 6;
            this._ativo.Text = "Ativo";
            // 
            // _actionsPanel
            // 
            this._editorLayout.SetColumnSpan(this._actionsPanel, 4);
            this._actionsPanel.Controls.Add(this._salvarButton);
            this._actionsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._actionsPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._actionsPanel.Location = new System.Drawing.Point(3, 71);
            this._actionsPanel.Name = "_actionsPanel";
            this._actionsPanel.Size = new System.Drawing.Size(906, 63);
            this._actionsPanel.TabIndex = 7;
            this._actionsPanel.WrapContents = false;
            // 
            // _salvarButton
            // 
            this._salvarButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this._salvarButton.Location = new System.Drawing.Point(806, 4);
            this._salvarButton.Margin = new System.Windows.Forms.Padding(4);
            this._salvarButton.Name = "_salvarButton";
            this._salvarButton.Size = new System.Drawing.Size(96, 30);
            this._salvarButton.TabIndex = 0;
            this._salvarButton.Text = "Salvar";
            this._salvarButton.UseVisualStyleBackColor = true;
            // 
            // ServicoForm
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(958, 604);
            this.Controls.Add(this._root);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(880, 580);
            this.Name = "ServicoForm";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Servicos";
            this._root.ResumeLayout(false);
            this._filtrosGroup.ResumeLayout(false);
            this._filtrosLayout.ResumeLayout(false);
            this._filtrosLayout.PerformLayout();
            this._pagerLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._grid)).EndInit();
            this._editorGroup.ResumeLayout(false);
            this._editorLayout.ResumeLayout(false);
            this._editorLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._valorBase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._percentualImposto)).EndInit();
            this._actionsPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}
