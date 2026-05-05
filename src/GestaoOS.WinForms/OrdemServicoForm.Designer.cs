using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace GestaoOS.WinForms
{
    public partial class OrdemServicoForm
    {
        private IContainer components = null;
        private TableLayoutPanel _root;
        private GroupBox _filtrosGroup;
        private TableLayoutPanel _filtrosLayout;
        private TableLayoutPanel _pagerLayout;
        private GroupBox _gridGroup;
        private TableLayoutPanel _editorRoot;
        private GroupBox _dadosGroup;
        private TableLayoutPanel _dadosPanel;
        private FlowLayoutPanel _salvarPanel;
        private GroupBox _itensGroup;
        private TableLayoutPanel _itensPanel;
        private TableLayoutPanel _itensTopo;
        private Label _clienteFiltroLabel;
        private Label _statusFiltroLabel;
        private Label _clienteLabel;
        private Label _aberturaLabel;
        private Label _statusLabel;
        private Label _conclusaoLabel;
        private Label _observacaoLabel;
        private Label _servicoLabel;
        private Label _quantidadeLabel;
        private Button _pesquisarButton;
        private Button _novaButton;
        private Button _anteriorButton;
        private Button _proximaButton;
        private Button _salvarButton;
        private ComboBox _filtroCliente;
        private ComboBox _filtroStatus;
        private DateTimePicker _dataInicio;
        private DateTimePicker _dataFim;
        private CheckBox _usarPeriodo;
        private DataGridView _grid;
        private Label _paginaLabel;
        private ComboBox _cliente;
        private ComboBox _status;
        private DateTimePicker _abertura;
        private DateTimePicker _conclusao;
        private CheckBox _usarConclusao;
        private TextBox _observacao;
        private ComboBox _servicoItem;
        private NumericUpDown _quantidade;
        private DataGridView _itensGrid;
        private Button _adicionarItemButton;
        private Button _removerItemButton;
        private DataGridViewTextBoxColumn _osColumn;
        private DataGridViewTextBoxColumn _clienteNomeColumn;
        private DataGridViewTextBoxColumn _aberturaColumn;
        private DataGridViewTextBoxColumn _conclusaoColumn;
        private DataGridViewTextBoxColumn _statusColumn;
        private DataGridViewTextBoxColumn _totalColumn;
        private DataGridViewTextBoxColumn _versaoColumn;
        private DataGridViewTextBoxColumn _itemServicoColumn;
        private DataGridViewTextBoxColumn _itemQuantidadeColumn;
        private DataGridViewTextBoxColumn _itemValorUnitarioColumn;
        private DataGridViewTextBoxColumn _itemImpostoColumn;
        private DataGridViewTextBoxColumn _itemTotalColumn;

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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OrdemServicoForm));
            this._root = new System.Windows.Forms.TableLayoutPanel();
            this._filtrosGroup = new System.Windows.Forms.GroupBox();
            this._filtrosLayout = new System.Windows.Forms.TableLayoutPanel();
            this._usarPeriodo = new System.Windows.Forms.CheckBox();
            this._dataInicio = new System.Windows.Forms.DateTimePicker();
            this._dataFim = new System.Windows.Forms.DateTimePicker();
            this._clienteFiltroLabel = new System.Windows.Forms.Label();
            this._filtroCliente = new System.Windows.Forms.ComboBox();
            this._statusFiltroLabel = new System.Windows.Forms.Label();
            this._filtroStatus = new System.Windows.Forms.ComboBox();
            this._pesquisarButton = new System.Windows.Forms.Button();
            this._novaButton = new System.Windows.Forms.Button();
            this._pagerLayout = new System.Windows.Forms.TableLayoutPanel();
            this._paginaLabel = new System.Windows.Forms.Label();
            this._anteriorButton = new System.Windows.Forms.Button();
            this._proximaButton = new System.Windows.Forms.Button();
            this._gridGroup = new System.Windows.Forms.GroupBox();
            this._grid = new System.Windows.Forms.DataGridView();
            this._editorRoot = new System.Windows.Forms.TableLayoutPanel();
            this._dadosGroup = new System.Windows.Forms.GroupBox();
            this._dadosPanel = new System.Windows.Forms.TableLayoutPanel();
            this._clienteLabel = new System.Windows.Forms.Label();
            this._cliente = new System.Windows.Forms.ComboBox();
            this._aberturaLabel = new System.Windows.Forms.Label();
            this._abertura = new System.Windows.Forms.DateTimePicker();
            this._statusLabel = new System.Windows.Forms.Label();
            this._status = new System.Windows.Forms.ComboBox();
            this._usarConclusao = new System.Windows.Forms.CheckBox();
            this._conclusaoLabel = new System.Windows.Forms.Label();
            this._conclusao = new System.Windows.Forms.DateTimePicker();
            this._observacaoLabel = new System.Windows.Forms.Label();
            this._observacao = new System.Windows.Forms.TextBox();
            this._salvarPanel = new System.Windows.Forms.FlowLayoutPanel();
            this._salvarButton = new System.Windows.Forms.Button();
            this._itensGroup = new System.Windows.Forms.GroupBox();
            this._itensPanel = new System.Windows.Forms.TableLayoutPanel();
            this._itensTopo = new System.Windows.Forms.TableLayoutPanel();
            this._servicoLabel = new System.Windows.Forms.Label();
            this._servicoItem = new System.Windows.Forms.ComboBox();
            this._quantidadeLabel = new System.Windows.Forms.Label();
            this._quantidade = new System.Windows.Forms.NumericUpDown();
            this._adicionarItemButton = new System.Windows.Forms.Button();
            this._removerItemButton = new System.Windows.Forms.Button();
            this._itensGrid = new System.Windows.Forms.DataGridView();
            this._root.SuspendLayout();
            this._filtrosGroup.SuspendLayout();
            this._filtrosLayout.SuspendLayout();
            this._pagerLayout.SuspendLayout();
            this._gridGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._grid)).BeginInit();
            this._editorRoot.SuspendLayout();
            this._dadosGroup.SuspendLayout();
            this._dadosPanel.SuspendLayout();
            this._salvarPanel.SuspendLayout();
            this._itensGroup.SuspendLayout();
            this._itensPanel.SuspendLayout();
            this._itensTopo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._quantidade)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._itensGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // _root
            // 
            this._root.ColumnCount = 1;
            this._root.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this._root.Controls.Add(this._filtrosGroup, 0, 0);
            this._root.Controls.Add(this._gridGroup, 0, 1);
            this._root.Controls.Add(this._editorRoot, 0, 2);
            this._root.Dock = System.Windows.Forms.DockStyle.Fill;
            this._root.Location = new System.Drawing.Point(10, 10);
            this._root.Name = "_root";
            this._root.RowCount = 3;
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 44F));
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 56F));
            this._root.Size = new System.Drawing.Size(1238, 744);
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
            this._filtrosGroup.Size = new System.Drawing.Size(1232, 124);
            this._filtrosGroup.TabIndex = 0;
            this._filtrosGroup.TabStop = false;
            this._filtrosGroup.Text = "Pesquisa";
            // 
            // _filtrosLayout
            // 
            this._filtrosLayout.ColumnCount = 10;
            this._filtrosLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 92F));
            this._filtrosLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 132F));
            this._filtrosLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 132F));
            this._filtrosLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 62F));
            this._filtrosLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._filtrosLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 62F));
            this._filtrosLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this._filtrosLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 108F));
            this._filtrosLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this._filtrosLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 4F));
            this._filtrosLayout.Controls.Add(this._usarPeriodo, 0, 0);
            this._filtrosLayout.Controls.Add(this._dataInicio, 1, 0);
            this._filtrosLayout.Controls.Add(this._dataFim, 2, 0);
            this._filtrosLayout.Controls.Add(this._clienteFiltroLabel, 3, 0);
            this._filtrosLayout.Controls.Add(this._filtroCliente, 4, 0);
            this._filtrosLayout.Controls.Add(this._statusFiltroLabel, 5, 0);
            this._filtrosLayout.Controls.Add(this._filtroStatus, 6, 0);
            this._filtrosLayout.Controls.Add(this._pesquisarButton, 7, 0);
            this._filtrosLayout.Controls.Add(this._novaButton, 8, 0);
            this._filtrosLayout.Controls.Add(this._pagerLayout, 0, 1);
            this._filtrosLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this._filtrosLayout.Location = new System.Drawing.Point(10, 32);
            this._filtrosLayout.Name = "_filtrosLayout";
            this._filtrosLayout.RowCount = 2;
            this._filtrosLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this._filtrosLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this._filtrosLayout.Size = new System.Drawing.Size(1212, 82);
            this._filtrosLayout.TabIndex = 0;
            // 
            // _usarPeriodo
            // 
            this._usarPeriodo.Dock = System.Windows.Forms.DockStyle.Fill;
            this._usarPeriodo.Location = new System.Drawing.Point(3, 3);
            this._usarPeriodo.Name = "_usarPeriodo";
            this._usarPeriodo.Size = new System.Drawing.Size(86, 34);
            this._usarPeriodo.TabIndex = 0;
            this._usarPeriodo.Text = "Periodo";
            // 
            // _dataInicio
            // 
            this._dataInicio.Dock = System.Windows.Forms.DockStyle.Fill;
            this._dataInicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this._dataInicio.Location = new System.Drawing.Point(95, 3);
            this._dataInicio.Margin = new System.Windows.Forms.Padding(3, 3, 8, 3);
            this._dataInicio.Name = "_dataInicio";
            this._dataInicio.Size = new System.Drawing.Size(121, 31);
            this._dataInicio.TabIndex = 1;
            // 
            // _dataFim
            // 
            this._dataFim.Dock = System.Windows.Forms.DockStyle.Fill;
            this._dataFim.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this._dataFim.Location = new System.Drawing.Point(227, 3);
            this._dataFim.Margin = new System.Windows.Forms.Padding(3, 3, 8, 3);
            this._dataFim.Name = "_dataFim";
            this._dataFim.Size = new System.Drawing.Size(121, 31);
            this._dataFim.TabIndex = 2;
            // 
            // _clienteFiltroLabel
            // 
            this._clienteFiltroLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._clienteFiltroLabel.Location = new System.Drawing.Point(359, 3);
            this._clienteFiltroLabel.Margin = new System.Windows.Forms.Padding(3, 3, 6, 3);
            this._clienteFiltroLabel.Name = "_clienteFiltroLabel";
            this._clienteFiltroLabel.Size = new System.Drawing.Size(53, 34);
            this._clienteFiltroLabel.TabIndex = 3;
            this._clienteFiltroLabel.Text = "Cliente";
            this._clienteFiltroLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _filtroCliente
            // 
            this._filtroCliente.Dock = System.Windows.Forms.DockStyle.Fill;
            this._filtroCliente.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._filtroCliente.Location = new System.Drawing.Point(421, 3);
            this._filtroCliente.Margin = new System.Windows.Forms.Padding(3, 3, 8, 3);
            this._filtroCliente.Name = "_filtroCliente";
            this._filtroCliente.Size = new System.Drawing.Size(359, 33);
            this._filtroCliente.TabIndex = 4;
            // 
            // _statusFiltroLabel
            // 
            this._statusFiltroLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._statusFiltroLabel.Location = new System.Drawing.Point(791, 3);
            this._statusFiltroLabel.Margin = new System.Windows.Forms.Padding(3, 3, 6, 3);
            this._statusFiltroLabel.Name = "_statusFiltroLabel";
            this._statusFiltroLabel.Size = new System.Drawing.Size(53, 34);
            this._statusFiltroLabel.TabIndex = 5;
            this._statusFiltroLabel.Text = "Status";
            this._statusFiltroLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _filtroStatus
            // 
            this._filtroStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this._filtroStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._filtroStatus.Location = new System.Drawing.Point(853, 3);
            this._filtroStatus.Margin = new System.Windows.Forms.Padding(3, 3, 8, 3);
            this._filtroStatus.Name = "_filtroStatus";
            this._filtroStatus.Size = new System.Drawing.Size(139, 33);
            this._filtroStatus.TabIndex = 6;
            // 
            // _pesquisarButton
            // 
            this._pesquisarButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this._pesquisarButton.Location = new System.Drawing.Point(1004, 4);
            this._pesquisarButton.Margin = new System.Windows.Forms.Padding(4);
            this._pesquisarButton.Name = "_pesquisarButton";
            this._pesquisarButton.Size = new System.Drawing.Size(96, 30);
            this._pesquisarButton.TabIndex = 7;
            this._pesquisarButton.Text = "Pesquisar";
            this._pesquisarButton.UseVisualStyleBackColor = true;
            // 
            // _novaButton
            // 
            this._novaButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this._novaButton.Location = new System.Drawing.Point(1112, 4);
            this._novaButton.Margin = new System.Windows.Forms.Padding(4);
            this._novaButton.Name = "_novaButton";
            this._novaButton.Size = new System.Drawing.Size(92, 30);
            this._novaButton.TabIndex = 8;
            this._novaButton.Text = "Nova";
            this._novaButton.UseVisualStyleBackColor = true;
            // 
            // _pagerLayout
            // 
            this._pagerLayout.ColumnCount = 3;
            this._filtrosLayout.SetColumnSpan(this._pagerLayout, 10);
            this._pagerLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._pagerLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 96F));
            this._pagerLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 96F));
            this._pagerLayout.Controls.Add(this._paginaLabel, 0, 0);
            this._pagerLayout.Controls.Add(this._anteriorButton, 1, 0);
            this._pagerLayout.Controls.Add(this._proximaButton, 2, 0);
            this._pagerLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this._pagerLayout.Location = new System.Drawing.Point(3, 43);
            this._pagerLayout.Name = "_pagerLayout";
            this._pagerLayout.RowCount = 1;
            this._pagerLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this._pagerLayout.Size = new System.Drawing.Size(1206, 38);
            this._pagerLayout.TabIndex = 9;
            // 
            // _paginaLabel
            // 
            this._paginaLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._paginaLabel.Location = new System.Drawing.Point(3, 0);
            this._paginaLabel.Name = "_paginaLabel";
            this._paginaLabel.Size = new System.Drawing.Size(1008, 38);
            this._paginaLabel.TabIndex = 0;
            this._paginaLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _anteriorButton
            // 
            this._anteriorButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this._anteriorButton.Location = new System.Drawing.Point(1018, 2);
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
            this._proximaButton.Location = new System.Drawing.Point(1114, 2);
            this._proximaButton.Margin = new System.Windows.Forms.Padding(4, 2, 4, 8);
            this._proximaButton.Name = "_proximaButton";
            this._proximaButton.Size = new System.Drawing.Size(88, 28);
            this._proximaButton.TabIndex = 2;
            this._proximaButton.Text = "Proxima";
            this._proximaButton.UseVisualStyleBackColor = true;
            // 
            // _gridGroup
            // 
            this._gridGroup.BackColor = System.Drawing.Color.White;
            this._gridGroup.Controls.Add(this._grid);
            this._gridGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this._gridGroup.Location = new System.Drawing.Point(3, 133);
            this._gridGroup.Name = "_gridGroup";
            this._gridGroup.Padding = new System.Windows.Forms.Padding(10, 8, 10, 10);
            this._gridGroup.Size = new System.Drawing.Size(1232, 264);
            this._gridGroup.TabIndex = 1;
            this._gridGroup.TabStop = false;
            this._gridGroup.Text = "Ordens cadastradas";
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
            this._grid.Location = new System.Drawing.Point(10, 32);
            this._grid.MultiSelect = false;
            this._grid.Name = "_grid";
            this._grid.ReadOnly = true;
            this._grid.RowHeadersVisible = false;
            this._grid.RowHeadersWidth = 62;
            this._grid.RowTemplate.Height = 28;
            this._grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._grid.Size = new System.Drawing.Size(1212, 222);
            this._grid.TabIndex = 0;
            // 
            // _editorRoot
            // 
            this._editorRoot.ColumnCount = 2;
            this._editorRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42F));
            this._editorRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 58F));
            this._editorRoot.Controls.Add(this._dadosGroup, 0, 0);
            this._editorRoot.Controls.Add(this._itensGroup, 1, 0);
            this._editorRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this._editorRoot.Location = new System.Drawing.Point(3, 403);
            this._editorRoot.Name = "_editorRoot";
            this._editorRoot.RowCount = 1;
            this._editorRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this._editorRoot.Size = new System.Drawing.Size(1232, 338);
            this._editorRoot.TabIndex = 2;
            // 
            // _dadosGroup
            // 
            this._dadosGroup.BackColor = System.Drawing.Color.White;
            this._dadosGroup.Controls.Add(this._dadosPanel);
            this._dadosGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this._dadosGroup.Location = new System.Drawing.Point(3, 3);
            this._dadosGroup.Name = "_dadosGroup";
            this._dadosGroup.Padding = new System.Windows.Forms.Padding(10, 8, 10, 10);
            this._dadosGroup.Size = new System.Drawing.Size(511, 332);
            this._dadosGroup.TabIndex = 0;
            this._dadosGroup.TabStop = false;
            this._dadosGroup.Text = "Dados da ordem";
            // 
            // _dadosPanel
            // 
            this._dadosPanel.ColumnCount = 2;
            this._dadosPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this._dadosPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._dadosPanel.Controls.Add(this._clienteLabel, 0, 0);
            this._dadosPanel.Controls.Add(this._cliente, 1, 0);
            this._dadosPanel.Controls.Add(this._aberturaLabel, 0, 1);
            this._dadosPanel.Controls.Add(this._abertura, 1, 1);
            this._dadosPanel.Controls.Add(this._statusLabel, 0, 2);
            this._dadosPanel.Controls.Add(this._status, 1, 2);
            this._dadosPanel.Controls.Add(this._usarConclusao, 1, 3);
            this._dadosPanel.Controls.Add(this._conclusaoLabel, 0, 4);
            this._dadosPanel.Controls.Add(this._conclusao, 1, 4);
            this._dadosPanel.Controls.Add(this._observacaoLabel, 0, 5);
            this._dadosPanel.Controls.Add(this._observacao, 1, 5);
            this._dadosPanel.Controls.Add(this._salvarPanel, 0, 6);
            this._dadosPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._dadosPanel.Location = new System.Drawing.Point(10, 32);
            this._dadosPanel.Name = "_dadosPanel";
            this._dadosPanel.RowCount = 7;
            this._dadosPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this._dadosPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this._dadosPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this._dadosPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this._dadosPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this._dadosPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._dadosPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this._dadosPanel.Size = new System.Drawing.Size(491, 290);
            this._dadosPanel.TabIndex = 0;
            // 
            // _clienteLabel
            // 
            this._clienteLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._clienteLabel.Location = new System.Drawing.Point(3, 3);
            this._clienteLabel.Margin = new System.Windows.Forms.Padding(3, 3, 6, 3);
            this._clienteLabel.Name = "_clienteLabel";
            this._clienteLabel.Size = new System.Drawing.Size(91, 28);
            this._clienteLabel.TabIndex = 0;
            this._clienteLabel.Text = "Cliente";
            this._clienteLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _cliente
            // 
            this._cliente.Dock = System.Windows.Forms.DockStyle.Fill;
            this._cliente.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cliente.Location = new System.Drawing.Point(103, 3);
            this._cliente.Margin = new System.Windows.Forms.Padding(3, 3, 8, 3);
            this._cliente.Name = "_cliente";
            this._cliente.Size = new System.Drawing.Size(380, 33);
            this._cliente.TabIndex = 1;
            // 
            // _aberturaLabel
            // 
            this._aberturaLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._aberturaLabel.Location = new System.Drawing.Point(3, 37);
            this._aberturaLabel.Margin = new System.Windows.Forms.Padding(3, 3, 6, 3);
            this._aberturaLabel.Name = "_aberturaLabel";
            this._aberturaLabel.Size = new System.Drawing.Size(91, 28);
            this._aberturaLabel.TabIndex = 2;
            this._aberturaLabel.Text = "Abertura";
            this._aberturaLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _abertura
            // 
            this._abertura.Dock = System.Windows.Forms.DockStyle.Fill;
            this._abertura.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this._abertura.Location = new System.Drawing.Point(103, 37);
            this._abertura.Margin = new System.Windows.Forms.Padding(3, 3, 8, 3);
            this._abertura.Name = "_abertura";
            this._abertura.Size = new System.Drawing.Size(380, 31);
            this._abertura.TabIndex = 3;
            // 
            // _statusLabel
            // 
            this._statusLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._statusLabel.Location = new System.Drawing.Point(3, 71);
            this._statusLabel.Margin = new System.Windows.Forms.Padding(3, 3, 6, 3);
            this._statusLabel.Name = "_statusLabel";
            this._statusLabel.Size = new System.Drawing.Size(91, 28);
            this._statusLabel.TabIndex = 4;
            this._statusLabel.Text = "Status";
            this._statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _status
            // 
            this._status.Dock = System.Windows.Forms.DockStyle.Fill;
            this._status.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._status.Location = new System.Drawing.Point(103, 71);
            this._status.Margin = new System.Windows.Forms.Padding(3, 3, 8, 3);
            this._status.Name = "_status";
            this._status.Size = new System.Drawing.Size(380, 33);
            this._status.TabIndex = 5;
            // 
            // _usarConclusao
            // 
            this._usarConclusao.Dock = System.Windows.Forms.DockStyle.Fill;
            this._usarConclusao.Location = new System.Drawing.Point(103, 105);
            this._usarConclusao.Name = "_usarConclusao";
            this._usarConclusao.Size = new System.Drawing.Size(385, 28);
            this._usarConclusao.TabIndex = 6;
            this._usarConclusao.Text = "Informar conclusao";
            // 
            // _conclusaoLabel
            // 
            this._conclusaoLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._conclusaoLabel.Location = new System.Drawing.Point(3, 139);
            this._conclusaoLabel.Margin = new System.Windows.Forms.Padding(3, 3, 6, 3);
            this._conclusaoLabel.Name = "_conclusaoLabel";
            this._conclusaoLabel.Size = new System.Drawing.Size(91, 28);
            this._conclusaoLabel.TabIndex = 7;
            this._conclusaoLabel.Text = "Conclusao";
            this._conclusaoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _conclusao
            // 
            this._conclusao.Dock = System.Windows.Forms.DockStyle.Fill;
            this._conclusao.Enabled = false;
            this._conclusao.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this._conclusao.Location = new System.Drawing.Point(103, 139);
            this._conclusao.Margin = new System.Windows.Forms.Padding(3, 3, 8, 3);
            this._conclusao.Name = "_conclusao";
            this._conclusao.Size = new System.Drawing.Size(380, 31);
            this._conclusao.TabIndex = 8;
            // 
            // _observacaoLabel
            // 
            this._observacaoLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._observacaoLabel.Location = new System.Drawing.Point(3, 173);
            this._observacaoLabel.Margin = new System.Windows.Forms.Padding(3, 3, 6, 3);
            this._observacaoLabel.Name = "_observacaoLabel";
            this._observacaoLabel.Size = new System.Drawing.Size(91, 72);
            this._observacaoLabel.TabIndex = 9;
            this._observacaoLabel.Text = "Observacao";
            this._observacaoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _observacao
            // 
            this._observacao.Dock = System.Windows.Forms.DockStyle.Fill;
            this._observacao.Location = new System.Drawing.Point(103, 173);
            this._observacao.Margin = new System.Windows.Forms.Padding(3, 3, 8, 3);
            this._observacao.MaxLength = 2000;
            this._observacao.Multiline = true;
            this._observacao.Name = "_observacao";
            this._observacao.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._observacao.Size = new System.Drawing.Size(380, 72);
            this._observacao.TabIndex = 10;
            // 
            // _salvarPanel
            // 
            this._dadosPanel.SetColumnSpan(this._salvarPanel, 2);
            this._salvarPanel.Controls.Add(this._salvarButton);
            this._salvarPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._salvarPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._salvarPanel.Location = new System.Drawing.Point(3, 251);
            this._salvarPanel.Name = "_salvarPanel";
            this._salvarPanel.Size = new System.Drawing.Size(485, 36);
            this._salvarPanel.TabIndex = 11;
            this._salvarPanel.WrapContents = false;
            // 
            // _salvarButton
            // 
            this._salvarButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this._salvarButton.Location = new System.Drawing.Point(371, 4);
            this._salvarButton.Margin = new System.Windows.Forms.Padding(4);
            this._salvarButton.Name = "_salvarButton";
            this._salvarButton.Size = new System.Drawing.Size(110, 30);
            this._salvarButton.TabIndex = 0;
            this._salvarButton.Text = "Salvar OS";
            this._salvarButton.UseVisualStyleBackColor = true;
            // 
            // _itensGroup
            // 
            this._itensGroup.BackColor = System.Drawing.Color.White;
            this._itensGroup.Controls.Add(this._itensPanel);
            this._itensGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this._itensGroup.Location = new System.Drawing.Point(520, 3);
            this._itensGroup.Name = "_itensGroup";
            this._itensGroup.Padding = new System.Windows.Forms.Padding(10, 8, 10, 10);
            this._itensGroup.Size = new System.Drawing.Size(709, 332);
            this._itensGroup.TabIndex = 1;
            this._itensGroup.TabStop = false;
            this._itensGroup.Text = "Itens da ordem";
            // 
            // _itensPanel
            // 
            this._itensPanel.ColumnCount = 1;
            this._itensPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this._itensPanel.Controls.Add(this._itensTopo, 0, 0);
            this._itensPanel.Controls.Add(this._itensGrid, 0, 1);
            this._itensPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._itensPanel.Location = new System.Drawing.Point(10, 32);
            this._itensPanel.Name = "_itensPanel";
            this._itensPanel.RowCount = 2;
            this._itensPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this._itensPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._itensPanel.Size = new System.Drawing.Size(689, 290);
            this._itensPanel.TabIndex = 0;
            // 
            // _itensTopo
            // 
            this._itensTopo.ColumnCount = 6;
            this._itensTopo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this._itensTopo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._itensTopo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this._itensTopo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this._itensTopo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 104F));
            this._itensTopo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 104F));
            this._itensTopo.Controls.Add(this._servicoLabel, 0, 0);
            this._itensTopo.Controls.Add(this._servicoItem, 1, 0);
            this._itensTopo.Controls.Add(this._quantidadeLabel, 2, 0);
            this._itensTopo.Controls.Add(this._quantidade, 3, 0);
            this._itensTopo.Controls.Add(this._adicionarItemButton, 4, 0);
            this._itensTopo.Controls.Add(this._removerItemButton, 5, 0);
            this._itensTopo.Dock = System.Windows.Forms.DockStyle.Fill;
            this._itensTopo.Location = new System.Drawing.Point(3, 3);
            this._itensTopo.Name = "_itensTopo";
            this._itensTopo.RowCount = 1;
            this._itensTopo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this._itensTopo.Size = new System.Drawing.Size(683, 36);
            this._itensTopo.TabIndex = 0;
            // 
            // _servicoLabel
            // 
            this._servicoLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._servicoLabel.Location = new System.Drawing.Point(3, 3);
            this._servicoLabel.Margin = new System.Windows.Forms.Padding(3, 3, 6, 3);
            this._servicoLabel.Name = "_servicoLabel";
            this._servicoLabel.Size = new System.Drawing.Size(55, 30);
            this._servicoLabel.TabIndex = 0;
            this._servicoLabel.Text = "Servico";
            this._servicoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _servicoItem
            // 
            this._servicoItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this._servicoItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._servicoItem.Location = new System.Drawing.Point(67, 3);
            this._servicoItem.Margin = new System.Windows.Forms.Padding(3, 3, 8, 3);
            this._servicoItem.Name = "_servicoItem";
            this._servicoItem.Size = new System.Drawing.Size(248, 33);
            this._servicoItem.TabIndex = 1;
            // 
            // _quantidadeLabel
            // 
            this._quantidadeLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._quantidadeLabel.Location = new System.Drawing.Point(326, 3);
            this._quantidadeLabel.Margin = new System.Windows.Forms.Padding(3, 3, 6, 3);
            this._quantidadeLabel.Name = "_quantidadeLabel";
            this._quantidadeLabel.Size = new System.Drawing.Size(33, 30);
            this._quantidadeLabel.TabIndex = 2;
            this._quantidadeLabel.Text = "Qtd";
            this._quantidadeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _quantidade
            // 
            this._quantidade.DecimalPlaces = 2;
            this._quantidade.Dock = System.Windows.Forms.DockStyle.Fill;
            this._quantidade.Location = new System.Drawing.Point(368, 3);
            this._quantidade.Margin = new System.Windows.Forms.Padding(3, 3, 8, 3);
            this._quantidade.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this._quantidade.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._quantidade.Name = "_quantidade";
            this._quantidade.Size = new System.Drawing.Size(99, 31);
            this._quantidade.TabIndex = 3;
            this._quantidade.ThousandsSeparator = true;
            this._quantidade.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // _adicionarItemButton
            // 
            this._adicionarItemButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this._adicionarItemButton.Location = new System.Drawing.Point(479, 4);
            this._adicionarItemButton.Margin = new System.Windows.Forms.Padding(4);
            this._adicionarItemButton.Name = "_adicionarItemButton";
            this._adicionarItemButton.Size = new System.Drawing.Size(96, 28);
            this._adicionarItemButton.TabIndex = 4;
            this._adicionarItemButton.Text = "Adicionar";
            this._adicionarItemButton.UseVisualStyleBackColor = true;
            // 
            // _removerItemButton
            // 
            this._removerItemButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this._removerItemButton.Location = new System.Drawing.Point(583, 4);
            this._removerItemButton.Margin = new System.Windows.Forms.Padding(4);
            this._removerItemButton.Name = "_removerItemButton";
            this._removerItemButton.Size = new System.Drawing.Size(96, 28);
            this._removerItemButton.TabIndex = 5;
            this._removerItemButton.Text = "Remover";
            this._removerItemButton.UseVisualStyleBackColor = true;
            // 
            // _itensGrid
            // 
            this._itensGrid.AllowUserToAddRows = false;
            this._itensGrid.AllowUserToDeleteRows = false;
            this._itensGrid.AllowUserToResizeRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._itensGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this._itensGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._itensGrid.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(241)))), ((int)(((byte)(245)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this._itensGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this._itensGrid.ColumnHeadersHeight = 32;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this._itensGrid.DefaultCellStyle = dataGridViewCellStyle6;
            this._itensGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._itensGrid.EnableHeadersVisualStyles = false;
            this._itensGrid.Location = new System.Drawing.Point(3, 45);
            this._itensGrid.MultiSelect = false;
            this._itensGrid.Name = "_itensGrid";
            this._itensGrid.ReadOnly = true;
            this._itensGrid.RowHeadersVisible = false;
            this._itensGrid.RowHeadersWidth = 62;
            this._itensGrid.RowTemplate.Height = 28;
            this._itensGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._itensGrid.Size = new System.Drawing.Size(683, 242);
            this._itensGrid.TabIndex = 1;
            // 
            // OrdemServicoForm
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1258, 764);
            this.Controls.Add(this._root);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1120, 720);
            this.Name = "OrdemServicoForm";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Ordens de Servico";
            this._root.ResumeLayout(false);
            this._filtrosGroup.ResumeLayout(false);
            this._filtrosLayout.ResumeLayout(false);
            this._pagerLayout.ResumeLayout(false);
            this._gridGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._grid)).EndInit();
            this._editorRoot.ResumeLayout(false);
            this._dadosGroup.ResumeLayout(false);
            this._dadosPanel.ResumeLayout(false);
            this._dadosPanel.PerformLayout();
            this._salvarPanel.ResumeLayout(false);
            this._itensGroup.ResumeLayout(false);
            this._itensPanel.ResumeLayout(false);
            this._itensTopo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._quantidade)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._itensGrid)).EndInit();
            this.ResumeLayout(false);

        }

    }
}
