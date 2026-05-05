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
    public partial class RelatorioOrdensForm : Form
    {
        private BindingList<RelatorioOrdemServico> _dados = new BindingList<RelatorioOrdemServico>();

        public RelatorioOrdensForm()
        {
            InitializeComponent();
            ConectarEventos();
            if (UiStyle.IsDesignMode(this))
            {
                return;
            }
            CarregarCombos();
        }

        private void ConectarEventos()
        {
            _usarPeriodo.CheckedChanged += UsarPeriodo_CheckedChanged;
            _gerarButton.Click += GerarButton_Click;
            _pdfButton.Click += PdfButton_Click;
        }

        private void UsarPeriodo_CheckedChanged(object sender, EventArgs e)
        {
            AtualizarPeriodo();
        }

        private void GerarButton_Click(object sender, EventArgs e)
        {
            Gerar();
        }

        private void PdfButton_Click(object sender, EventArgs e)
        {
            ExportarPdf();
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
                _usarPeriodo.Checked = false;
                AtualizarPeriodo();
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

                var filtro = RelatorioFiltroFactory.Criar(_inicio.Value, _fim.Value, _usarPeriodo.Checked, _cliente.SelectedValue, _status.SelectedItem);

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

                    string mimeType;
                    string encoding;
                    string fileNameExtension;
                    string[] streams;
                    Microsoft.Reporting.WinForms.Warning[] warnings;

                    var bytes = _viewer.LocalReport.Render(
                        "PDF", 
                        null, 
                        out mimeType, 
                        out encoding, 
                        out fileNameExtension, 
                        out streams, 
                        out warnings);

                    File.WriteAllBytes(dialog.FileName, bytes);
                }
            });
        }

        private bool EntradaValida()
        {
            if (_usarPeriodo.Checked && _inicio.Value.Date > _fim.Value.Date)
            {
                MessageBox.Show("A data Inicio nao pode ser maior que a data Fim.", "Validacao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _inicio.Focus();
                return false;
            }

            return true;
        }

        private void AtualizarPeriodo()
        {
            _inicio.Enabled = _usarPeriodo.Checked;
            _fim.Enabled = _usarPeriodo.Checked;
        }
    }
}
