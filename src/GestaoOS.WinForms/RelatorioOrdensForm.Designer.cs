using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace GestaoOS.WinForms
{
    public partial class RelatorioOrdensForm
    {
        private IContainer components = null;
        private TableLayoutPanel _root;
        private GroupBox _filtrosGroup;
        private TableLayoutPanel _filtrosLayout;
        private Label _inicioLabel;
        private Label _fimLabel;
        private Label _clienteLabel;
        private Label _statusLabel;
        private Button _gerarButton;
        private Button _pdfButton;
        private ComboBox _cliente;
        private ComboBox _status;
        private DateTimePicker _inicio;
        private DateTimePicker _fim;
        private CheckBox _usarPeriodo;
        private ReportViewer _viewer;
        private Label _resumo;

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
            _root = new TableLayoutPanel();
            _filtrosGroup = new GroupBox();
            _filtrosLayout = new TableLayoutPanel();
            _inicioLabel = new Label();
            _fimLabel = new Label();
            _clienteLabel = new Label();
            _statusLabel = new Label();
            _gerarButton = new Button();
            _pdfButton = new Button();
            _cliente = new ComboBox();
            _status = new ComboBox();
            _inicio = new DateTimePicker();
            _fim = new DateTimePicker();
            _usarPeriodo = new CheckBox();
            _viewer = new ReportViewer();
            _resumo = new Label();
            SuspendLayout();

            Text = "Relatorio Gerencial";
            StartPosition = FormStartPosition.CenterParent;
            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            BackColor = Color.FromArgb(245, 246, 248);
            Size = new Size(1180, 760);
            MinimumSize = new Size(1020, 680);
            Padding = new Padding(10);

            _root.Name = "_root";
            _root.Dock = DockStyle.Fill;
            _root.ColumnCount = 1;
            _root.RowCount = 3;
            _root.RowStyles.Add(new RowStyle(SizeType.Absolute, 92));
            _root.RowStyles.Add(new RowStyle(SizeType.Absolute, 36));
            _root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            _filtrosGroup.Name = "_filtrosGroup";
            _filtrosGroup.Text = "Filtros do relatorio";
            _filtrosGroup.Dock = DockStyle.Fill;
            _filtrosGroup.BackColor = Color.White;
            _filtrosGroup.Padding = new Padding(10, 8, 10, 10);

            _filtrosLayout.Name = "_filtrosLayout";
            _filtrosLayout.Dock = DockStyle.Fill;
            _filtrosLayout.ColumnCount = 11;
            _filtrosLayout.RowCount = 1;
            _filtrosLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 82));
            _filtrosLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 55));
            _filtrosLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 132));
            _filtrosLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 45));
            _filtrosLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 132));
            _filtrosLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 65));
            _filtrosLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            _filtrosLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 62));
            _filtrosLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150));
            _filtrosLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 96));
            _filtrosLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 96));

            _usarPeriodo.Name = "_usarPeriodo";
            _usarPeriodo.Text = "Periodo";
            _usarPeriodo.Dock = DockStyle.Fill;
            _usarPeriodo.CheckedChanged += UsarPeriodo_CheckedChanged;
            _inicio.Name = "_inicio";
            _inicio.Dock = DockStyle.Fill;
            _inicio.Margin = new Padding(3, 3, 8, 3);
            _inicio.Format = DateTimePickerFormat.Short;
            _fim.Name = "_fim";
            _fim.Dock = DockStyle.Fill;
            _fim.Margin = new Padding(3, 3, 8, 3);
            _fim.Format = DateTimePickerFormat.Short;
            _cliente.Name = "_cliente";
            _cliente.Dock = DockStyle.Fill;
            _cliente.Margin = new Padding(3, 3, 8, 3);
            _cliente.DropDownStyle = ComboBoxStyle.DropDownList;
            _status.Name = "_status";
            _status.Dock = DockStyle.Fill;
            _status.Margin = new Padding(3, 3, 8, 3);
            _status.DropDownStyle = ComboBoxStyle.DropDownList;

            _inicioLabel.Name = "_inicioLabel";
            _inicioLabel.Text = "Inicio";
            _inicioLabel.Dock = DockStyle.Fill;
            _inicioLabel.TextAlign = ContentAlignment.MiddleLeft;
            _inicioLabel.Margin = new Padding(3, 3, 6, 3);
            _fimLabel.Name = "_fimLabel";
            _fimLabel.Text = "Fim";
            _fimLabel.Dock = DockStyle.Fill;
            _fimLabel.TextAlign = ContentAlignment.MiddleLeft;
            _fimLabel.Margin = new Padding(3, 3, 6, 3);
            _clienteLabel.Name = "_clienteLabel";
            _clienteLabel.Text = "Cliente";
            _clienteLabel.Dock = DockStyle.Fill;
            _clienteLabel.TextAlign = ContentAlignment.MiddleLeft;
            _clienteLabel.Margin = new Padding(3, 3, 6, 3);
            _statusLabel.Name = "_statusLabel";
            _statusLabel.Text = "Status";
            _statusLabel.Dock = DockStyle.Fill;
            _statusLabel.TextAlign = ContentAlignment.MiddleLeft;
            _statusLabel.Margin = new Padding(3, 3, 6, 3);
            _gerarButton.Name = "_gerarButton";
            _gerarButton.Text = "Gerar";
            _gerarButton.Width = 96;
            _gerarButton.Height = 30;
            _gerarButton.Margin = new Padding(4);
            _gerarButton.FlatStyle = FlatStyle.System;
            _gerarButton.UseVisualStyleBackColor = true;
            _gerarButton.Click += GerarButton_Click;
            _pdfButton.Name = "_pdfButton";
            _pdfButton.Text = "PDF";
            _pdfButton.Width = 96;
            _pdfButton.Height = 30;
            _pdfButton.Margin = new Padding(4);
            _pdfButton.FlatStyle = FlatStyle.System;
            _pdfButton.UseVisualStyleBackColor = true;
            _pdfButton.Click += PdfButton_Click;

            _filtrosLayout.Controls.Add(_usarPeriodo, 0, 0);
            _filtrosLayout.Controls.Add(_inicioLabel, 1, 0);
            _filtrosLayout.Controls.Add(_inicio, 2, 0);
            _filtrosLayout.Controls.Add(_fimLabel, 3, 0);
            _filtrosLayout.Controls.Add(_fim, 4, 0);
            _filtrosLayout.Controls.Add(_clienteLabel, 5, 0);
            _filtrosLayout.Controls.Add(_cliente, 6, 0);
            _filtrosLayout.Controls.Add(_statusLabel, 7, 0);
            _filtrosLayout.Controls.Add(_status, 8, 0);
            _filtrosLayout.Controls.Add(_gerarButton, 9, 0);
            _filtrosLayout.Controls.Add(_pdfButton, 10, 0);
            _filtrosGroup.Controls.Add(_filtrosLayout);

            _resumo.Name = "_resumo";
            _resumo.Dock = DockStyle.Fill;
            _resumo.TextAlign = ContentAlignment.MiddleLeft;
            _resumo.Padding = new Padding(8, 0, 0, 0);

            _viewer.Name = "_viewer";
            _viewer.Dock = DockStyle.Fill;
            _viewer.ProcessingMode = ProcessingMode.Local;

            _root.Controls.Add(_filtrosGroup, 0, 0);
            _root.Controls.Add(_resumo, 0, 1);
            _root.Controls.Add(_viewer, 0, 2);
            Controls.Add(_root);
            ResumeLayout(false);
        }
    }
}
