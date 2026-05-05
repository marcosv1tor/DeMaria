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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RelatorioOrdensForm));
            this._root = new System.Windows.Forms.TableLayoutPanel();
            this._filtrosGroup = new System.Windows.Forms.GroupBox();
            this._filtrosLayout = new System.Windows.Forms.TableLayoutPanel();
            this._usarPeriodo = new System.Windows.Forms.CheckBox();
            this._inicioLabel = new System.Windows.Forms.Label();
            this._inicio = new System.Windows.Forms.DateTimePicker();
            this._fimLabel = new System.Windows.Forms.Label();
            this._fim = new System.Windows.Forms.DateTimePicker();
            this._clienteLabel = new System.Windows.Forms.Label();
            this._cliente = new System.Windows.Forms.ComboBox();
            this._statusLabel = new System.Windows.Forms.Label();
            this._status = new System.Windows.Forms.ComboBox();
            this._gerarButton = new System.Windows.Forms.Button();
            this._pdfButton = new System.Windows.Forms.Button();
            this._resumo = new System.Windows.Forms.Label();
            this._viewer = new Microsoft.Reporting.WinForms.ReportViewer();
            this._root.SuspendLayout();
            this._filtrosGroup.SuspendLayout();
            this._filtrosLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // _root
            // 
            this._root.ColumnCount = 1;
            this._root.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this._root.Controls.Add(this._filtrosGroup, 0, 0);
            this._root.Controls.Add(this._resumo, 0, 1);
            this._root.Controls.Add(this._viewer, 0, 2);
            this._root.Dock = System.Windows.Forms.DockStyle.Fill;
            this._root.Location = new System.Drawing.Point(10, 10);
            this._root.Name = "_root";
            this._root.RowCount = 3;
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 92F));
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._root.Size = new System.Drawing.Size(1138, 684);
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
            this._filtrosGroup.Size = new System.Drawing.Size(1132, 86);
            this._filtrosGroup.TabIndex = 0;
            this._filtrosGroup.TabStop = false;
            this._filtrosGroup.Text = "Filtros do relatorio";
            // 
            // _filtrosLayout
            // 
            this._filtrosLayout.ColumnCount = 11;
            this._filtrosLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 82F));
            this._filtrosLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this._filtrosLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 132F));
            this._filtrosLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this._filtrosLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 132F));
            this._filtrosLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this._filtrosLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._filtrosLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 62F));
            this._filtrosLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this._filtrosLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 96F));
            this._filtrosLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 96F));
            this._filtrosLayout.Controls.Add(this._usarPeriodo, 0, 0);
            this._filtrosLayout.Controls.Add(this._inicioLabel, 1, 0);
            this._filtrosLayout.Controls.Add(this._inicio, 2, 0);
            this._filtrosLayout.Controls.Add(this._fimLabel, 3, 0);
            this._filtrosLayout.Controls.Add(this._fim, 4, 0);
            this._filtrosLayout.Controls.Add(this._clienteLabel, 5, 0);
            this._filtrosLayout.Controls.Add(this._cliente, 6, 0);
            this._filtrosLayout.Controls.Add(this._statusLabel, 7, 0);
            this._filtrosLayout.Controls.Add(this._status, 8, 0);
            this._filtrosLayout.Controls.Add(this._gerarButton, 9, 0);
            this._filtrosLayout.Controls.Add(this._pdfButton, 10, 0);
            this._filtrosLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this._filtrosLayout.Location = new System.Drawing.Point(10, 32);
            this._filtrosLayout.Name = "_filtrosLayout";
            this._filtrosLayout.RowCount = 1;
            this._filtrosLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this._filtrosLayout.Size = new System.Drawing.Size(1112, 44);
            this._filtrosLayout.TabIndex = 0;
            // 
            // _usarPeriodo
            // 
            this._usarPeriodo.Dock = System.Windows.Forms.DockStyle.Fill;
            this._usarPeriodo.Location = new System.Drawing.Point(3, 3);
            this._usarPeriodo.Name = "_usarPeriodo";
            this._usarPeriodo.Size = new System.Drawing.Size(76, 38);
            this._usarPeriodo.TabIndex = 0;
            this._usarPeriodo.Text = "Periodo";
            // 
            // _inicioLabel
            // 
            this._inicioLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._inicioLabel.Location = new System.Drawing.Point(85, 3);
            this._inicioLabel.Margin = new System.Windows.Forms.Padding(3, 3, 6, 3);
            this._inicioLabel.Name = "_inicioLabel";
            this._inicioLabel.Size = new System.Drawing.Size(46, 38);
            this._inicioLabel.TabIndex = 1;
            this._inicioLabel.Text = "Inicio";
            this._inicioLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _inicio
            // 
            this._inicio.Dock = System.Windows.Forms.DockStyle.Fill;
            this._inicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this._inicio.Location = new System.Drawing.Point(140, 3);
            this._inicio.Margin = new System.Windows.Forms.Padding(3, 3, 8, 3);
            this._inicio.Name = "_inicio";
            this._inicio.Size = new System.Drawing.Size(121, 31);
            this._inicio.TabIndex = 2;
            // 
            // _fimLabel
            // 
            this._fimLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._fimLabel.Location = new System.Drawing.Point(272, 3);
            this._fimLabel.Margin = new System.Windows.Forms.Padding(3, 3, 6, 3);
            this._fimLabel.Name = "_fimLabel";
            this._fimLabel.Size = new System.Drawing.Size(36, 38);
            this._fimLabel.TabIndex = 3;
            this._fimLabel.Text = "Fim";
            this._fimLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _fim
            // 
            this._fim.Dock = System.Windows.Forms.DockStyle.Fill;
            this._fim.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this._fim.Location = new System.Drawing.Point(317, 3);
            this._fim.Margin = new System.Windows.Forms.Padding(3, 3, 8, 3);
            this._fim.Name = "_fim";
            this._fim.Size = new System.Drawing.Size(121, 31);
            this._fim.TabIndex = 4;
            // 
            // _clienteLabel
            // 
            this._clienteLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._clienteLabel.Location = new System.Drawing.Point(449, 3);
            this._clienteLabel.Margin = new System.Windows.Forms.Padding(3, 3, 6, 3);
            this._clienteLabel.Name = "_clienteLabel";
            this._clienteLabel.Size = new System.Drawing.Size(56, 38);
            this._clienteLabel.TabIndex = 5;
            this._clienteLabel.Text = "Cliente";
            this._clienteLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _cliente
            // 
            this._cliente.Dock = System.Windows.Forms.DockStyle.Fill;
            this._cliente.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cliente.Location = new System.Drawing.Point(514, 3);
            this._cliente.Margin = new System.Windows.Forms.Padding(3, 3, 8, 3);
            this._cliente.Name = "_cliente";
            this._cliente.Size = new System.Drawing.Size(186, 33);
            this._cliente.TabIndex = 6;
            // 
            // _statusLabel
            // 
            this._statusLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._statusLabel.Location = new System.Drawing.Point(711, 3);
            this._statusLabel.Margin = new System.Windows.Forms.Padding(3, 3, 6, 3);
            this._statusLabel.Name = "_statusLabel";
            this._statusLabel.Size = new System.Drawing.Size(53, 38);
            this._statusLabel.TabIndex = 7;
            this._statusLabel.Text = "Status";
            this._statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _status
            // 
            this._status.Dock = System.Windows.Forms.DockStyle.Fill;
            this._status.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._status.Location = new System.Drawing.Point(773, 3);
            this._status.Margin = new System.Windows.Forms.Padding(3, 3, 8, 3);
            this._status.Name = "_status";
            this._status.Size = new System.Drawing.Size(139, 33);
            this._status.TabIndex = 8;
            // 
            // _gerarButton
            // 
            this._gerarButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this._gerarButton.Location = new System.Drawing.Point(924, 4);
            this._gerarButton.Margin = new System.Windows.Forms.Padding(4);
            this._gerarButton.Name = "_gerarButton";
            this._gerarButton.Size = new System.Drawing.Size(88, 30);
            this._gerarButton.TabIndex = 9;
            this._gerarButton.Text = "Gerar";
            this._gerarButton.UseVisualStyleBackColor = true;
            // 
            // _pdfButton
            // 
            this._pdfButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this._pdfButton.Location = new System.Drawing.Point(1020, 4);
            this._pdfButton.Margin = new System.Windows.Forms.Padding(4);
            this._pdfButton.Name = "_pdfButton";
            this._pdfButton.Size = new System.Drawing.Size(88, 30);
            this._pdfButton.TabIndex = 10;
            this._pdfButton.Text = "PDF";
            this._pdfButton.UseVisualStyleBackColor = true;
            // 
            // _resumo
            // 
            this._resumo.Dock = System.Windows.Forms.DockStyle.Fill;
            this._resumo.Location = new System.Drawing.Point(3, 92);
            this._resumo.Name = "_resumo";
            this._resumo.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this._resumo.Size = new System.Drawing.Size(1132, 36);
            this._resumo.TabIndex = 1;
            this._resumo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _viewer
            // 
            this._viewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this._viewer.Location = new System.Drawing.Point(3, 131);
            this._viewer.Name = "_viewer";
            this._viewer.ServerReport.BearerToken = null;
            this._viewer.Size = new System.Drawing.Size(1132, 550);
            this._viewer.TabIndex = 2;
            // 
            // RelatorioOrdensForm
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1158, 704);
            this.Controls.Add(this._root);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1020, 680);
            this.Name = "RelatorioOrdensForm";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Relatorio Gerencial";
            this._root.ResumeLayout(false);
            this._filtrosGroup.ResumeLayout(false);
            this._filtrosLayout.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}
