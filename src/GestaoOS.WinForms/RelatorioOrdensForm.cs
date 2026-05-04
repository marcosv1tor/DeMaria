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
            Size = new Size(1180, 760);
            StartPosition = FormStartPosition.CenterParent;
            BuildLayout();
            CarregarCombos();
        }

        private void BuildLayout()
        {
            var root = new TableLayoutPanel { Dock = DockStyle.Fill, RowCount = 3, ColumnCount = 1 };
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 72));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 36));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            var filtros = new FlowLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(8) };
            filtros.Controls.Add(new Label { Text = "Inicio", Width = 45, TextAlign = ContentAlignment.MiddleLeft });
            filtros.Controls.Add(_inicio);
            filtros.Controls.Add(new Label { Text = "Fim", Width = 35, TextAlign = ContentAlignment.MiddleLeft });
            filtros.Controls.Add(_fim);
            filtros.Controls.Add(new Label { Text = "Cliente", Width = 48, TextAlign = ContentAlignment.MiddleLeft });
            _cliente.Width = 200;
            filtros.Controls.Add(_cliente);
            filtros.Controls.Add(new Label { Text = "Status", Width = 45, TextAlign = ContentAlignment.MiddleLeft });
            _status.Width = 140;
            filtros.Controls.Add(_status);
            filtros.Controls.Add(Button("Gerar", Gerar));
            filtros.Controls.Add(Button("PDF", ExportarPdf));

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

        private static Button Button(string text, Action action)
        {
            var button = new Button { Text = text, Width = 90, Height = 28, Margin = new Padding(4) };
            button.Click += (s, e) => action();
            return button;
        }
    }
}
