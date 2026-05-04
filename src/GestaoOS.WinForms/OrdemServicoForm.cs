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
    public class OrdemServicoForm : Form
    {
        private readonly ComboBox _filtroCliente = new ComboBox();
        private readonly ComboBox _filtroStatus = new ComboBox();
        private readonly DateTimePicker _dataInicio = new DateTimePicker();
        private readonly DateTimePicker _dataFim = new DateTimePicker();
        private readonly CheckBox _usarPeriodo = new CheckBox();
        private readonly DataGridView _grid = new DataGridView();
        private readonly Label _paginaLabel = new Label();
        private readonly ComboBox _cliente = new ComboBox();
        private readonly ComboBox _status = new ComboBox();
        private readonly DateTimePicker _abertura = new DateTimePicker();
        private readonly DateTimePicker _conclusao = new DateTimePicker();
        private readonly CheckBox _usarConclusao = new CheckBox();
        private readonly TextBox _observacao = new TextBox();
        private readonly ComboBox _servicoItem = new ComboBox();
        private readonly NumericUpDown _quantidade = new NumericUpDown();
        private readonly DataGridView _itensGrid = new DataGridView();
        private readonly BindingList<OrdemServicoItem> _itens = new BindingList<OrdemServicoItem>();
        private Button _adicionarItemButton;
        private Button _removerItemButton;
        private int _pagina = 1;
        private int _totalPaginas = 1;
        private int _idAtual;
        private int _versaoAtual = 1;

        public OrdemServicoForm()
        {
            Text = "Ordens de Servico";
            StartPosition = FormStartPosition.CenterParent;
            UiStyle.ApplyForm(this, new Size(1280, 820), new Size(1120, 720));
            BuildLayout();
            CarregarCombos();
            Carregar();
        }

        private void BuildLayout()
        {
            var root = new TableLayoutPanel { Dock = DockStyle.Fill, RowCount = 3, ColumnCount = 1 };
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 130));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 44));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 56));

            var filtros = CriarFiltros();

            var gridGroup = UiStyle.GroupBox("Ordens cadastradas");
            UiStyle.ConfigureGrid(_grid);
            CriarColunasGrid();
            _grid.CellFormatting += GridCellFormatting;
            _grid.SelectionChanged += (s, e) => SelecionarAtual();
            gridGroup.Controls.Add(_grid);

            var editor = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 1 };
            editor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 42));
            editor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 58));
            editor.Controls.Add(CriarDadosOs(), 0, 0);
            editor.Controls.Add(CriarItens(), 1, 0);

            root.Controls.Add(filtros, 0, 0);
            root.Controls.Add(gridGroup, 0, 1);
            root.Controls.Add(editor, 0, 2);
            Controls.Add(root);
        }

        private Control CriarFiltros()
        {
            var group = UiStyle.GroupBox("Pesquisa");
            var filtros = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 10, RowCount = 2 };
            filtros.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 92));
            filtros.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 132));
            filtros.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 132));
            filtros.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 62));
            filtros.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            filtros.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 62));
            filtros.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150));
            filtros.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 108));
            filtros.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100));
            filtros.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 4));
            filtros.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
            filtros.RowStyles.Add(new RowStyle(SizeType.Absolute, 44));

            _usarPeriodo.Text = "Periodo";
            _usarPeriodo.Dock = DockStyle.Fill;
            _usarPeriodo.CheckedChanged += (s, e) => AtualizarFiltroPeriodo();
            _dataInicio.Format = DateTimePickerFormat.Short;
            _dataFim.Format = DateTimePickerFormat.Short;
            _filtroCliente.DropDownStyle = ComboBoxStyle.DropDownList;
            _filtroStatus.DropDownStyle = ComboBoxStyle.DropDownList;

            filtros.Controls.Add(UiStyle.Fill(_usarPeriodo), 0, 0);
            filtros.Controls.Add(UiStyle.Fill(_dataInicio), 1, 0);
            filtros.Controls.Add(UiStyle.Fill(_dataFim), 2, 0);
            filtros.Controls.Add(UiStyle.Label("Cliente"), 3, 0);
            filtros.Controls.Add(UiStyle.Fill(_filtroCliente), 4, 0);
            filtros.Controls.Add(UiStyle.Label("Status"), 5, 0);
            filtros.Controls.Add(UiStyle.Fill(_filtroStatus), 6, 0);
            filtros.Controls.Add(UiStyle.Button("Pesquisar", Pesquisar), 7, 0);
            filtros.Controls.Add(UiStyle.Button("Nova", Limpar), 8, 0);

            var pager = CriarPaginacao();
            filtros.Controls.Add(pager, 0, 1);
            filtros.SetColumnSpan(pager, 10);
            group.Controls.Add(filtros);
            AtualizarFiltroPeriodo();
            return group;
        }

        private Control CriarPaginacao()
        {
            var pager = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 3, RowCount = 1 };
            pager.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            pager.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 96));
            pager.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 96));
            _paginaLabel.Dock = DockStyle.Fill;
            _paginaLabel.TextAlign = ContentAlignment.MiddleRight;
            pager.Controls.Add(_paginaLabel, 0, 0);
            pager.Controls.Add(UiStyle.PagerButton("Anterior", PaginaAnterior), 1, 0);
            pager.Controls.Add(UiStyle.PagerButton("Proxima", ProximaPagina), 2, 0);
            return pager;
        }

        private void CriarColunasGrid()
        {
            _grid.Columns.Clear();
            _grid.Columns.Add(UiStyle.TextColumn("Id", "OS", 55, null, DataGridViewContentAlignment.MiddleRight));
            _grid.Columns.Add(UiStyle.TextColumn("ClienteNome", "Cliente", 200));
            _grid.Columns.Add(UiStyle.TextColumn("DataAbertura", "Abertura", 120, "g", DataGridViewContentAlignment.MiddleLeft));
            _grid.Columns.Add(UiStyle.TextColumn("DataConclusao", "Conclusao", 110, null, DataGridViewContentAlignment.MiddleLeft));
            _grid.Columns.Add(UiStyle.TextColumn("Status", "Status", 95));
            _grid.Columns.Add(UiStyle.TextColumn("ValorTotal", "Total", 95, "C2", DataGridViewContentAlignment.MiddleRight));
            _grid.Columns.Add(UiStyle.TextColumn("Versao", "Versao", 65, null, DataGridViewContentAlignment.MiddleRight));
        }

        private Control CriarDadosOs()
        {
            var group = UiStyle.GroupBox("Dados da ordem");
            var panel = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 7 };
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 34));
            panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 34));
            panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 34));
            panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 34));
            panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 34));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 42));

            _cliente.DropDownStyle = ComboBoxStyle.DropDownList;
            _status.DropDownStyle = ComboBoxStyle.DropDownList;
            _status.DataSource = Enum.GetValues(typeof(StatusOrdemServico));
            _status.SelectedIndexChanged += (s, e) => AtualizarEdicaoItens();
            _abertura.Format = DateTimePickerFormat.Short;
            _conclusao.Format = DateTimePickerFormat.Short;
            _usarConclusao.Text = "Informar conclusao";
            _usarConclusao.Dock = DockStyle.Fill;
            _usarConclusao.CheckedChanged += (s, e) => _conclusao.Enabled = _usarConclusao.Checked;
            _conclusao.Enabled = _usarConclusao.Checked;
            _observacao.Multiline = true;
            _observacao.ScrollBars = ScrollBars.Vertical;
            _observacao.MaxLength = 2000;

            UiStyle.AddField(panel, "Cliente", _cliente, 0);
            UiStyle.AddField(panel, "Abertura", _abertura, 1);
            UiStyle.AddField(panel, "Status", _status, 2);
            panel.Controls.Add(UiStyle.Fill(_usarConclusao), 1, 3);
            UiStyle.AddField(panel, "Conclusao", _conclusao, 4);
            UiStyle.AddField(panel, "Observacao", _observacao, 5);

            var actions = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.RightToLeft, WrapContents = false };
            actions.Controls.Add(UiStyle.Button("Salvar OS", Salvar, 110));
            panel.Controls.Add(actions, 0, 6);
            panel.SetColumnSpan(actions, 2);
            group.Controls.Add(panel);
            return group;
        }

        private Control CriarItens()
        {
            var group = UiStyle.GroupBox("Itens da ordem");
            var panel = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 2 };
            panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 42));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            var topo = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 6, RowCount = 1 };
            topo.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 64));
            topo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            topo.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 42));
            topo.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110));
            topo.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 104));
            topo.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 104));
            _servicoItem.DropDownStyle = ComboBoxStyle.DropDownList;
            _quantidade.DecimalPlaces = 2;
            _quantidade.Minimum = 1;
            _quantidade.Maximum = 999;
            _quantidade.Increment = 1;
            _quantidade.ThousandsSeparator = true;
            _quantidade.Value = 1;
            topo.Controls.Add(UiStyle.Label("Servico"), 0, 0);
            topo.Controls.Add(UiStyle.Fill(_servicoItem), 1, 0);
            topo.Controls.Add(UiStyle.Label("Qtd"), 2, 0);
            topo.Controls.Add(UiStyle.Fill(_quantidade), 3, 0);
            _adicionarItemButton = UiStyle.Button("Adicionar", AdicionarItem);
            _removerItemButton = UiStyle.Button("Remover", RemoverItem);
            topo.Controls.Add(_adicionarItemButton, 4, 0);
            topo.Controls.Add(_removerItemButton, 5, 0);

            UiStyle.ConfigureGrid(_itensGrid);
            CriarColunasItensGrid();
            _itensGrid.DataSource = _itens;
            panel.Controls.Add(topo, 0, 0);
            panel.Controls.Add(_itensGrid, 0, 1);
            group.Controls.Add(panel);
            return group;
        }

        private void CriarColunasItensGrid()
        {
            _itensGrid.Columns.Clear();
            _itensGrid.Columns.Add(UiStyle.TextColumn("ServicoId", "Servico", 85, null, DataGridViewContentAlignment.MiddleRight));
            _itensGrid.Columns.Add(UiStyle.TextColumn("Quantidade", "Qtd", 80, "N2", DataGridViewContentAlignment.MiddleRight));
            _itensGrid.Columns.Add(UiStyle.TextColumn("ValorUnitario", "Valor unitario", 110, "C2", DataGridViewContentAlignment.MiddleRight));
            _itensGrid.Columns.Add(UiStyle.TextColumn("PercentualImpostoAplicado", "Imposto %", 95, "N2", DataGridViewContentAlignment.MiddleRight));
            _itensGrid.Columns.Add(UiStyle.TextColumn("ValorTotalItem", "Total", 110, "C2", DataGridViewContentAlignment.MiddleRight));
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
