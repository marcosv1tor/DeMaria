using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using GestaoOS.Domain.DTOs;
using GestaoOS.Domain.Entities;
using GestaoOS.Domain.Enums;
using GestaoOS.Domain.Filters;
using Microsoft.Reporting.WinForms;

namespace GestaoOS.WinForms
{
    public class RelatorioOrdensForm : Form
    {
        private readonly ComboBox _cliente = new ComboBox();
        private readonly ComboBox _status = new ComboBox();
        private readonly DateTimePicker _inicio = new DateTimePicker();
        private readonly DateTimePicker _fim = new DateTimePicker();
        private readonly ReportViewer _viewer = new ReportViewer();
        private readonly Label _resumo = new Label();
        private BindingList<RelatorioOrdemServico> _dados = new BindingList<RelatorioOrdemServico>();

        public RelatorioOrdensForm()
        {
            Text = "Relatorio Gerencial";
            StartPosition = FormStartPosition.CenterParent;
            UiStyle.ApplyForm(this, new Size(1180, 760), new Size(1020, 680));
            BuildLayout();
            CarregarCombos();
        }

        private void BuildLayout()
        {
            var root = new TableLayoutPanel { Dock = DockStyle.Fill, RowCount = 3, ColumnCount = 1 };
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 92));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 36));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            var filtros = CriarFiltros();

            _resumo.Dock = DockStyle.Fill;
            _resumo.TextAlign = ContentAlignment.MiddleLeft;
            _resumo.Padding = new Padding(8, 0, 0, 0);

            _viewer.Dock = DockStyle.Fill;
            _viewer.ProcessingMode = ProcessingMode.Local;

            root.Controls.Add(filtros, 0, 0);
            root.Controls.Add(_resumo, 0, 1);
            root.Controls.Add(_viewer, 0, 2);
            Controls.Add(root);
        }

        private Control CriarFiltros()
        {
            var group = UiStyle.GroupBox("Filtros do relatorio");
            var filtros = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 10, RowCount = 1 };
            filtros.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 55));
            filtros.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 132));
            filtros.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 45));
            filtros.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 132));
            filtros.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 65));
            filtros.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            filtros.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 62));
            filtros.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150));
            filtros.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 96));
            filtros.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 96));

            _inicio.Format = DateTimePickerFormat.Short;
            _fim.Format = DateTimePickerFormat.Short;
            _cliente.DropDownStyle = ComboBoxStyle.DropDownList;
            _status.DropDownStyle = ComboBoxStyle.DropDownList;

            filtros.Controls.Add(UiStyle.Label("Inicio"), 0, 0);
            filtros.Controls.Add(UiStyle.Fill(_inicio), 1, 0);
            filtros.Controls.Add(UiStyle.Label("Fim"), 2, 0);
            filtros.Controls.Add(UiStyle.Fill(_fim), 3, 0);
            filtros.Controls.Add(UiStyle.Label("Cliente"), 4, 0);
            filtros.Controls.Add(UiStyle.Fill(_cliente), 5, 0);
            filtros.Controls.Add(UiStyle.Label("Status"), 6, 0);
            filtros.Controls.Add(UiStyle.Fill(_status), 7, 0);
            filtros.Controls.Add(UiStyle.Button("Gerar", Gerar), 8, 0);
            filtros.Controls.Add(UiStyle.Button("PDF", ExportarPdf), 9, 0);
            group.Controls.Add(filtros);
            return group;
        }

        private void CarregarCombos()
        {
            UiExceptionHandler.Run(() =>
            {
                var clientes = Bootstrapper.ClienteService().Pesquisar(new ClienteFiltro { Ativo = true }, new Paginacao { Pagina = 1, TamanhoPagina = 200 }).Itens;
                _cliente.DataSource = new BindingList<Cliente>(new System.Collections.Generic.List<Cliente>(clientes));
                _cliente.DisplayMember = "Nome";
                _cliente.ValueMember = "Id";
                _cliente.SelectedIndex = -1;

                _status.Items.Add("Todos");
                foreach (var status in Enum.GetValues(typeof(StatusOrdemServico)))
                {
                    _status.Items.Add(status);
                }
                _status.SelectedIndex = 0;
                _inicio.Value = DateTime.Today.AddMonths(-1);
                _fim.Value = DateTime.Today;
            });
        }

        private void Gerar()
        {
            UiExceptionHandler.Run(() =>
            {
                if (!EntradaValida())
                {
                    return;
                }

                var filtro = new OrdemServicoFiltro
                {
                    DataInicio = _inicio.Value.Date,
                    DataFim = _fim.Value.Date,
                    ClienteId = _cliente.SelectedValue is int ? (int?)_cliente.SelectedValue : null,
                    Status = _status.SelectedItem is StatusOrdemServico ? (StatusOrdemServico?)_status.SelectedItem : null
                };

                _dados = new BindingList<RelatorioOrdemServico>(new System.Collections.Generic.List<RelatorioOrdemServico>(Bootstrapper.RelatorioService().GerarOrdensServico(filtro)));
                _viewer.LocalReport.ReportPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reports", "OrdensServicoReport.rdlc");
                _viewer.LocalReport.DataSources.Clear();
                _viewer.LocalReport.DataSources.Add(new ReportDataSource("OrdensServicoDataSet", _dados));
                _viewer.RefreshReport();

                var totalGeral = _dados.Sum(x => x.ValorTotal);
                var totalImpostos = _dados.Sum(x => x.TotalImpostos);
                var totalOs = _dados.Select(x => x.OrdemServicoId).Distinct().Count();
                _resumo.Text = string.Format("Quantidade OS: {0}    Total geral: {1:C}    Total impostos: {2:C}", totalOs, totalGeral, totalImpostos);
            });
        }

        private void ExportarPdf()
        {
            UiExceptionHandler.Run(() =>
            {
                if (_dados.Count == 0)
                {
                    MessageBox.Show("Gere o relatorio antes de exportar.", "Relatorio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using (var dialog = new SaveFileDialog())
                {
                    dialog.Filter = "PDF (*.pdf)|*.pdf";
                    dialog.FileName = "relatorio-ordens-servico.pdf";
                    if (dialog.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }

                    var bytes = _viewer.LocalReport.Render("PDF");
                    File.WriteAllBytes(dialog.FileName, bytes);
                }
            });
        }

        private bool EntradaValida()
        {
            if (_inicio.Value.Date > _fim.Value.Date)
            {
                MessageBox.Show("A data Inicio nao pode ser maior que a data Fim.", "Validacao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _inicio.Focus();
                return false;
            }

            return true;
        }

    }
}
