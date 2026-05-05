using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using GestaoOS.Domain.Entities;
using GestaoOS.Domain.Filters;

namespace GestaoOS.WinForms
{
    public partial class ServicoForm : Form
    {
        private int _idAtual;
        private int _pagina = 1;
        private int _totalPaginas = 1;

        public ServicoForm()
        {
            InitializeComponent();
            if (UiStyle.IsDesignMode(this))
            {
                return;
            }
            Carregar();
        }

        private void Grid_SelectionChanged(object sender, EventArgs e)
        {
            SelecionarAtual();
        }

        private void Grid_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
            e.Cancel = true;
        }

        private void PesquisarButton_Click(object sender, EventArgs e)
        {
            Pesquisar();
        }

        private void NovoButton_Click(object sender, EventArgs e)
        {
            Limpar();
        }

        private void AnteriorButton_Click(object sender, EventArgs e)
        {
            PaginaAnterior();
        }

        private void ProximaButton_Click(object sender, EventArgs e)
        {
            ProximaPagina();
        }

        private void SalvarButton_Click(object sender, EventArgs e)
        {
            Salvar();
        }

        private void Carregar()
        {
            UiExceptionHandler.Run(() =>
            {
                var filtro = new ServicoFiltro
                {
                    Nome = _filtroNome.Text,
                    Ativo = _filtroAtivo.SelectedIndex == 0 ? (bool?)null : _filtroAtivo.SelectedIndex == 1
                };
                var resultado = Bootstrapper.ServicoService().Pesquisar(filtro, new Paginacao { Pagina = _pagina, TamanhoPagina = 20 });
                var totalPaginas = Math.Max(1, (int)Math.Ceiling(resultado.Total / 20m));
                if (_pagina > totalPaginas)
                {
                    _pagina = totalPaginas;
                    Carregar();
                    return;
                }

                _totalPaginas = totalPaginas;
                _grid.DataSource = new BindingList<Servico>(new System.Collections.Generic.List<Servico>(resultado.Itens));
                _paginaLabel.Text = "Pagina " + _pagina + " de " + _totalPaginas + " | Registros: " + resultado.Total;
            });
        }

        private void Pesquisar()
        {
            _pagina = 1;
            Carregar();
        }

        private void PaginaAnterior()
        {
            if (_pagina <= 1)
            {
                return;
            }

            _pagina--;
            Carregar();
        }

        private void ProximaPagina()
        {
            if (_pagina >= _totalPaginas)
            {
                return;
            }

            _pagina++;
            Carregar();
        }

        private void Salvar()
        {
            UiExceptionHandler.Run(() =>
            {
                if (!EntradaValida())
                {
                    return;
                }

                Bootstrapper.ServicoService().Salvar(new Servico
                {
                    Id = _idAtual,
                    Nome = _nome.Text.Trim(),
                    ValorBase = _valorBase.Value,
                    PercentualImposto = _percentualImposto.Value,
                    Ativo = _ativo.Checked
                });
                Limpar();
                Carregar();
            });
        }

        private void SelecionarAtual()
        {
            if (_grid.CurrentRow == null || _grid.CurrentRow.IsNewRow)
            {
                return;
            }

            var servico = _grid.CurrentRow.DataBoundItem as Servico;
            if (servico == null)
            {
                return;
            }

            _idAtual = servico.Id;
            _nome.Text = servico.Nome;
            _valorBase.Value = servico.ValorBase;
            _percentualImposto.Value = servico.PercentualImposto;
            _ativo.Checked = servico.Ativo;
        }

        private void Limpar()
        {
            _idAtual = 0;
            _nome.Clear();
            _valorBase.Value = 0;
            _percentualImposto.Value = 0;
            _ativo.Checked = true;
        }

        private bool EntradaValida()
        {
            if (string.IsNullOrWhiteSpace(_nome.Text))
            {
                MessageBox.Show("Preencha o campo Nome.", "Validacao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _nome.Focus();
                return false;
            }

            if (_valorBase.Value <= 0)
            {
                MessageBox.Show("Informe um Valor base maior que zero.", "Validacao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _valorBase.Focus();
                return false;
            }

            if (_percentualImposto.Value < 0 || _percentualImposto.Value > 100)
            {
                MessageBox.Show("Informe um Imposto % entre 0 e 100.", "Validacao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _percentualImposto.Focus();
                return false;
            }

            return true;
        }
    }
}
