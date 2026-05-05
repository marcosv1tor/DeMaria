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
            ConectarEventos();
            ConfigurarColunasGrid();
            if (UiStyle.IsDesignMode(this))
            {
                return;
            }
            _filtroAtivo.SelectedIndex = 0;
            Carregar();
        }

        private void ConectarEventos()
        {
            _grid.SelectionChanged += Grid_SelectionChanged;
            _grid.DataError += Grid_DataError;
            _pesquisarButton.Click += PesquisarButton_Click;
            _novoButton.Click += NovoButton_Click;
            _anteriorButton.Click += AnteriorButton_Click;
            _proximaButton.Click += ProximaButton_Click;
            _salvarButton.Click += SalvarButton_Click;
        }

        private void ConfigurarColunasGrid()
        {
            _grid.Columns.Clear();
            _idColumn = _idColumn ?? new DataGridViewTextBoxColumn();
            _nomeColumn = _nomeColumn ?? new DataGridViewTextBoxColumn();
            _valorBaseColumn = _valorBaseColumn ?? new DataGridViewTextBoxColumn();
            _percentualImpostoColumn = _percentualImpostoColumn ?? new DataGridViewTextBoxColumn();
            _ativoColumn = _ativoColumn ?? new DataGridViewCheckBoxColumn();
            ConfigurarColunaTexto(_idColumn, "Id", "Id", 60, null, DataGridViewContentAlignment.MiddleRight);
            ConfigurarColunaTexto(_nomeColumn, "Nome", "Nome", 220, null, DataGridViewContentAlignment.MiddleLeft);
            ConfigurarColunaTexto(_valorBaseColumn, "ValorBase", "Valor base", 110, "C2", DataGridViewContentAlignment.MiddleRight);
            ConfigurarColunaTexto(_percentualImpostoColumn, "PercentualImposto", "Imposto %", 95, "N2", DataGridViewContentAlignment.MiddleRight);
            _ativoColumn.DataPropertyName = "Ativo";
            _ativoColumn.HeaderText = "Ativo";
            _ativoColumn.FillWeight = 70;
            _ativoColumn.MinimumWidth = 60;
            _grid.Columns.AddRange(new DataGridViewColumn[] { _idColumn, _nomeColumn, _valorBaseColumn, _percentualImpostoColumn, _ativoColumn });
        }

        private static void ConfigurarColunaTexto(DataGridViewTextBoxColumn column, string propertyName, string headerText, int fillWeight, string format, DataGridViewContentAlignment alignment)
        {
            column.DataPropertyName = propertyName;
            column.HeaderText = headerText;
            column.FillWeight = fillWeight;
            column.MinimumWidth = Math.Min(70, Math.Max(45, fillWeight));
            column.DefaultCellStyle.Alignment = alignment;
            column.DefaultCellStyle.Format = format;
            column.DefaultCellStyle.NullValue = string.Empty;
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
                    Ativo = ResolverFiltroAtivo(_filtroAtivo.SelectedIndex)
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

        private static bool? ResolverFiltroAtivo(int selectedIndex)
        {
            if (selectedIndex == 1)
            {
                return true;
            }

            if (selectedIndex == 2)
            {
                return false;
            }

            return null;
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
