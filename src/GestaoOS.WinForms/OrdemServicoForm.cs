using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using GestaoOS.Domain.DTOs;
using GestaoOS.Domain.Entities;
using GestaoOS.Domain.Enums;
using GestaoOS.Domain.Filters;

namespace GestaoOS.WinForms
{
    public partial class OrdemServicoForm : Form
    {
        private readonly BindingList<OrdemServicoItem> _itens = new BindingList<OrdemServicoItem>();
        private int _pagina = 1;
        private int _totalPaginas = 1;
        private int _idAtual;
        private int _versaoAtual = 1;

        public OrdemServicoForm()
        {
            InitializeComponent();
            if (UiStyle.IsDesignMode(this))
            {
                return;
            }
            _status.DataSource = Enum.GetValues(typeof(StatusOrdemServico));
            _itensGrid.DataSource = _itens;
            CarregarCombos();
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

        private void UsarPeriodo_CheckedChanged(object sender, EventArgs e)
        {
            AtualizarFiltroPeriodo();
        }

        private void Status_SelectedIndexChanged(object sender, EventArgs e)
        {
            AtualizarEdicaoItens();
        }

        private void UsarConclusao_CheckedChanged(object sender, EventArgs e)
        {
            _conclusao.Enabled = _usarConclusao.Checked;
        }

        private void PesquisarButton_Click(object sender, EventArgs e)
        {
            Pesquisar();
        }

        private void NovaButton_Click(object sender, EventArgs e)
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

        private void AdicionarItemButton_Click(object sender, EventArgs e)
        {
            AdicionarItem();
        }

        private void RemoverItemButton_Click(object sender, EventArgs e)
        {
            RemoverItem();
        }

        private void SalvarButton_Click(object sender, EventArgs e)
        {
            Salvar();
        }

        private void CarregarCombos()
        {
            UiExceptionHandler.Run(() =>
            {
                var clientes = Bootstrapper.ClienteService().Pesquisar(new ClienteFiltro { Ativo = true }, new Paginacao { Pagina = 1, TamanhoPagina = 200 }).Itens;
                var servicos = Bootstrapper.ServicoService().Pesquisar(new ServicoFiltro { Ativo = true }, new Paginacao { Pagina = 1, TamanhoPagina = 200 }).Itens;

                _cliente.DataSource = new BindingList<Cliente>(new System.Collections.Generic.List<Cliente>(clientes));
                _cliente.DisplayMember = "Nome";
                _cliente.ValueMember = "Id";

                _filtroCliente.DataSource = new BindingList<Cliente>(new System.Collections.Generic.List<Cliente>(clientes));
                _filtroCliente.DisplayMember = "Nome";
                _filtroCliente.ValueMember = "Id";
                _filtroCliente.SelectedIndex = -1;

                _servicoItem.DataSource = new BindingList<Servico>(new System.Collections.Generic.List<Servico>(servicos));
                _servicoItem.DisplayMember = "Nome";
                _servicoItem.ValueMember = "Id";

                _filtroStatus.Items.Clear();
                _filtroStatus.Items.Add("Todos");
                foreach (var status in Enum.GetValues(typeof(StatusOrdemServico)))
                {
                    _filtroStatus.Items.Add(status);
                }
                _filtroStatus.SelectedIndex = 0;
            });
        }

        private void Carregar()
        {
            UiExceptionHandler.Run(() =>
            {
                var filtro = new OrdemServicoFiltro
                {
                    DataInicio = _usarPeriodo.Checked ? _dataInicio.Value.Date : (DateTime?)null,
                    DataFim = _usarPeriodo.Checked ? _dataFim.Value.Date : (DateTime?)null,
                    ClienteId = _filtroCliente.SelectedValue is int ? (int?)_filtroCliente.SelectedValue : null,
                    Status = _filtroStatus.SelectedItem is StatusOrdemServico ? (StatusOrdemServico?)_filtroStatus.SelectedItem : null
                };

                var resultado = Bootstrapper.OrdemServicoService().Pesquisar(filtro, new Paginacao { Pagina = _pagina, TamanhoPagina = 20 });
                var totalPaginas = Math.Max(1, (int)Math.Ceiling(resultado.Total / 20m));
                if (_pagina > totalPaginas)
                {
                    _pagina = totalPaginas;
                    Carregar();
                    return;
                }

                _totalPaginas = totalPaginas;
                _grid.DataSource = new BindingList<OrdemServicoResumo>(new System.Collections.Generic.List<OrdemServicoResumo>(resultado.Itens));
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

        private void GridCellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            if (_grid.Columns[e.ColumnIndex].DataPropertyName != "DataConclusao")
            {
                return;
            }

            e.Value = OrdemServicoGridPresentation.FormatarConclusao(e.Value as DateTime?);
            e.FormattingApplied = true;
        }

        private void SelecionarAtual()
        {
            if (_grid.CurrentRow == null || _grid.CurrentRow.IsNewRow)
            {
                return;
            }

            var resumo = _grid.CurrentRow.DataBoundItem as OrdemServicoResumo;
            if (resumo == null || resumo.Id <= 0)
            {
                return;
            }

            UiExceptionHandler.Run(() =>
            {
                var ordem = Bootstrapper.OrdemServicoService().ObterComItens(resumo.Id);
                if (ordem == null)
                {
                    return;
                }

                _idAtual = ordem.Id;
                _versaoAtual = ordem.Versao;
                _cliente.SelectedValue = ordem.ClienteId;
                _abertura.Value = ordem.DataAbertura;
                _status.SelectedItem = ordem.Status;
                _usarConclusao.Checked = ordem.DataConclusao.HasValue;
                _conclusao.Value = ordem.DataConclusao ?? DateTime.Today;
                _observacao.Text = ordem.Observacao;
                _itens.Clear();
                foreach (var item in ordem.Itens)
                {
                    _itens.Add(item);
                }
                AtualizarEdicaoItens();
            });
        }

        private void AdicionarItem()
        {
            if (ItensBloqueados())
            {
                MessageBox.Show("Nao e possivel alterar itens de OS concluida ou cancelada.", "Itens da OS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!(_servicoItem.SelectedItem is Servico servico))
            {
                MessageBox.Show("Selecione um Servico para adicionar.", "Validacao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _servicoItem.Focus();
                return;
            }

            if (_quantidade.Value <= 0)
            {
                MessageBox.Show("Informe uma Qtd maior que zero.", "Validacao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _quantidade.Focus();
                return;
            }

            var item = new OrdemServicoItem
            {
                ServicoId = servico.Id,
                Quantidade = _quantidade.Value,
                ValorUnitario = servico.ValorBase,
                PercentualImpostoAplicado = servico.PercentualImposto
            };
            item.Recalcular();
            _itens.Add(item);
        }

        private void RemoverItem()
        {
            if (ItensBloqueados())
            {
                MessageBox.Show("Nao e possivel alterar itens de OS concluida ou cancelada.", "Itens da OS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (_itensGrid.CurrentRow != null && _itensGrid.CurrentRow.DataBoundItem is OrdemServicoItem item)
            {
                _itens.Remove(item);
            }
        }

        private void Salvar()
        {
            UiExceptionHandler.Run(() =>
            {
                if (!EntradaValida())
                {
                    return;
                }

                var momentoSalvar = DateTime.Now;
                var ordem = new OrdemServico
                {
                    Id = _idAtual,
                    ClienteId = Convert.ToInt32(_cliente.SelectedValue),
                    DataAbertura = OrdemServicoDateTime.ResolverDataAbertura(_abertura.Value, momentoSalvar, _idAtual == 0),
                    DataConclusao = _usarConclusao.Checked ? (DateTime?)_conclusao.Value : null,
                    Status = (StatusOrdemServico)_status.SelectedItem,
                    Observacao = _observacao.Text,
                    Versao = _versaoAtual
                };
                foreach (var item in _itens)
                {
                    ordem.Itens.Add(item);
                }

                Bootstrapper.OrdemServicoService().Salvar(ordem, Environment.UserName);
                Limpar();
                Carregar();
            });
        }

        private void Limpar()
        {
            _idAtual = 0;
            _versaoAtual = 1;
            if (_cliente.Items.Count > 0)
            {
                _cliente.SelectedIndex = 0;
            }
            _abertura.Value = DateTime.Today;
            _status.SelectedItem = StatusOrdemServico.Aberta;
            _usarConclusao.Checked = false;
            _conclusao.Value = DateTime.Today;
            _observacao.Clear();
            _itens.Clear();
            AtualizarEdicaoItens();
        }

        private bool ItensBloqueados()
        {
            if (!(_status.SelectedItem is StatusOrdemServico status))
            {
                return false;
            }

            return status == StatusOrdemServico.Concluida || status == StatusOrdemServico.Cancelada;
        }

        private void AtualizarEdicaoItens()
        {
            var habilitar = !ItensBloqueados();
            _servicoItem.Enabled = habilitar;
            _quantidade.Enabled = habilitar;
            if (_adicionarItemButton != null)
            {
                _adicionarItemButton.Enabled = habilitar;
            }

            if (_removerItemButton != null)
            {
                _removerItemButton.Enabled = habilitar;
            }
        }

        private void AtualizarFiltroPeriodo()
        {
            var habilitar = _usarPeriodo.Checked;
            _dataInicio.Enabled = habilitar;
            _dataFim.Enabled = habilitar;
        }

        private bool EntradaValida()
        {
            if (!(_cliente.SelectedValue is int clienteId) || clienteId <= 0)
            {
                MessageBox.Show("Selecione um Cliente.", "Validacao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _cliente.Focus();
                return false;
            }

            if (!(_status.SelectedItem is StatusOrdemServico))
            {
                MessageBox.Show("Selecione um Status.", "Validacao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _status.Focus();
                return false;
            }

            if (_usarConclusao.Checked && _conclusao.Value.Date < _abertura.Value.Date)
            {
                MessageBox.Show("A Conclusao nao pode ser anterior a Abertura.", "Validacao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _conclusao.Focus();
                return false;
            }

            if ((StatusOrdemServico)_status.SelectedItem == StatusOrdemServico.Concluida && _itens.Count == 0)
            {
                MessageBox.Show("Adicione ao menos um item antes de concluir a OS.", "Validacao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _servicoItem.Focus();
                return false;
            }

            return true;
        }
    }
}
