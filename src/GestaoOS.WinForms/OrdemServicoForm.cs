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
        private int _pagina = 1;
        private int _idAtual;
        private int _versaoAtual = 1;

        public OrdemServicoForm()
        {
            Text = "Ordens de Servico";
            Size = new Size(1180, 760);
            StartPosition = FormStartPosition.CenterParent;
            BuildLayout();
            CarregarCombos();
            Carregar();
        }

        private void BuildLayout()
        {
            var root = new TableLayoutPanel { Dock = DockStyle.Fill, RowCount = 3, ColumnCount = 1 };
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 70));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 45));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 55));

            var filtros = new FlowLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(8) };
            _usarPeriodo.Text = "Periodo";
            filtros.Controls.Add(_usarPeriodo);
            _dataInicio.Width = 120;
            _dataFim.Width = 120;
            filtros.Controls.Add(_dataInicio);
            filtros.Controls.Add(_dataFim);
            filtros.Controls.Add(new Label { Text = "Cliente", Width = 48, TextAlign = ContentAlignment.MiddleLeft });
            _filtroCliente.Width = 180;
            filtros.Controls.Add(_filtroCliente);
            filtros.Controls.Add(new Label { Text = "Status", Width = 45, TextAlign = ContentAlignment.MiddleLeft });
            _filtroStatus.Width = 140;
            filtros.Controls.Add(_filtroStatus);
            filtros.Controls.Add(Button("Pesquisar", Carregar));
            filtros.Controls.Add(Button("Nova", Limpar));
            filtros.Controls.Add(Button("<", () => { if (_pagina > 1) { _pagina--; Carregar(); } }));
            filtros.Controls.Add(_paginaLabel);
            filtros.Controls.Add(Button(">", () => { _pagina++; Carregar(); }));

            _grid.Dock = DockStyle.Fill;
            _grid.AutoGenerateColumns = true;
            _grid.ReadOnly = true;
            _grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _grid.MultiSelect = false;
            _grid.SelectionChanged += (s, e) => SelecionarAtual();

            var editor = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(8), ColumnCount = 2, RowCount = 1 };
            editor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45));
            editor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55));
            editor.Controls.Add(CriarDadosOs(), 0, 0);
            editor.Controls.Add(CriarItens(), 1, 0);

            root.Controls.Add(filtros, 0, 0);
            root.Controls.Add(_grid, 0, 1);
            root.Controls.Add(editor, 0, 2);
            Controls.Add(root);
        }

        private Control CriarDadosOs()
        {
            var panel = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 7 };
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            _cliente.DropDownStyle = ComboBoxStyle.DropDownList;
            _status.DropDownStyle = ComboBoxStyle.DropDownList;
            _status.DataSource = Enum.GetValues(typeof(StatusOrdemServico));
            _abertura.Format = DateTimePickerFormat.Short;
            _conclusao.Format = DateTimePickerFormat.Short;
            _usarConclusao.Text = "Informar conclusao";
            _observacao.Multiline = true;
            _observacao.Height = 80;

            AddField(panel, "Cliente", _cliente, 0);
            AddField(panel, "Abertura", _abertura, 1);
            AddField(panel, "Status", _status, 2);
            panel.Controls.Add(_usarConclusao, 1, 3);
            AddField(panel, "Conclusao", _conclusao, 4);
            AddField(panel, "Observacao", _observacao, 5);
            panel.Controls.Add(Button("Salvar OS", Salvar), 1, 6);
            return panel;
        }

        private Control CriarItens()
        {
            var panel = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 2 };
            panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 44));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            var topo = new FlowLayoutPanel { Dock = DockStyle.Fill };
            _servicoItem.DropDownStyle = ComboBoxStyle.DropDownList;
            _servicoItem.Width = 220;
            _quantidade.DecimalPlaces = 2;
            _quantidade.Minimum = 1;
            _quantidade.Maximum = 999;
            _quantidade.Value = 1;
            topo.Controls.Add(new Label { Text = "Servico", Width = 55, TextAlign = ContentAlignment.MiddleLeft });
            topo.Controls.Add(_servicoItem);
            topo.Controls.Add(new Label { Text = "Qtd", Width = 35, TextAlign = ContentAlignment.MiddleLeft });
            topo.Controls.Add(_quantidade);
            topo.Controls.Add(Button("Adicionar", AdicionarItem));
            topo.Controls.Add(Button("Remover", RemoverItem));

            _itensGrid.Dock = DockStyle.Fill;
            _itensGrid.AutoGenerateColumns = true;
            _itensGrid.DataSource = _itens;
            panel.Controls.Add(topo, 0, 0);
            panel.Controls.Add(_itensGrid, 0, 1);
            return panel;
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
                _grid.DataSource = new BindingList<OrdemServicoResumo>(new System.Collections.Generic.List<OrdemServicoResumo>(resultado.Itens));
                _paginaLabel.Text = "Pagina " + _pagina + " / Total " + resultado.Total;
            });
        }

        private void SelecionarAtual()
        {
            if (_grid.CurrentRow == null || _grid.CurrentRow.DataBoundItem == null)
            {
                return;
            }

            var resumo = (OrdemServicoResumo)_grid.CurrentRow.DataBoundItem;
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
            });
        }

        private void AdicionarItem()
        {
            if (!(_servicoItem.SelectedItem is Servico servico))
            {
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
            if (_itensGrid.CurrentRow != null && _itensGrid.CurrentRow.DataBoundItem is OrdemServicoItem item)
            {
                _itens.Remove(item);
            }
        }

        private void Salvar()
        {
            UiExceptionHandler.Run(() =>
            {
                var ordem = new OrdemServico
                {
                    Id = _idAtual,
                    ClienteId = Convert.ToInt32(_cliente.SelectedValue),
                    DataAbertura = _abertura.Value,
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
        }

        private static Button Button(string text, Action action)
        {
            var button = new Button { Text = text, Width = 90, Height = 28, Margin = new Padding(4) };
            button.Click += (s, e) => action();
            return button;
        }

        private static void AddField(TableLayoutPanel panel, string label, Control control, int row)
        {
            panel.Controls.Add(new Label { Text = label, Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft }, 0, row);
            control.Dock = DockStyle.Fill;
            panel.Controls.Add(control, 1, row);
        }
    }
}
