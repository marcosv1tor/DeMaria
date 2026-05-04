using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using GestaoOS.Domain.Entities;
using GestaoOS.Domain.Filters;

namespace GestaoOS.WinForms
{
    public class ServicoForm : Form
    {
        private readonly TextBox _filtroNome = new TextBox();
        private readonly ComboBox _filtroAtivo = new ComboBox();
        private readonly DataGridView _grid = new DataGridView();
        private readonly TextBox _nome = new TextBox();
        private readonly NumericUpDown _valorBase = new NumericUpDown();
        private readonly NumericUpDown _percentualImposto = new NumericUpDown();
        private readonly CheckBox _ativo = new CheckBox();
        private readonly Label _paginaLabel = new Label();
        private int _idAtual;
        private int _pagina = 1;
        private int _totalPaginas = 1;

        public ServicoForm()
        {
            Text = "Servicos";
            StartPosition = FormStartPosition.CenterParent;
            UiStyle.ApplyForm(this, new Size(980, 660), new Size(880, 580));
            BuildLayout();
            Carregar();
        }

        private void BuildLayout()
        {
            var root = new TableLayoutPanel { Dock = DockStyle.Fill, RowCount = 3, ColumnCount = 1 };
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 105));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 185));

            var filtros = CriarFiltros();
            UiStyle.ConfigureGrid(_grid);
            CriarColunasGrid();
            _grid.SelectionChanged += (s, e) => SelecionarAtual();

            _valorBase.DecimalPlaces = 2;
            _valorBase.Maximum = 9999999;
            _valorBase.Minimum = 0;
            _percentualImposto.DecimalPlaces = 2;
            _percentualImposto.Maximum = 100;
            _percentualImposto.Minimum = 0;

            var editorGroup = UiStyle.GroupBox("Dados do servico");
            var editor = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 4, RowCount = 3 };
            editor.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100));
            editor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            editor.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120));
            editor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            editor.RowStyles.Add(new RowStyle(SizeType.Absolute, 34));
            editor.RowStyles.Add(new RowStyle(SizeType.Absolute, 34));
            editor.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            UiStyle.AddField(editor, "Nome", _nome, 0, 0);
            UiStyle.AddField(editor, "Valor base", _valorBase, 2, 0);
            UiStyle.AddField(editor, "Imposto %", _percentualImposto, 0, 1);
            _ativo.Text = "Ativo";
            _ativo.Checked = true;
            _ativo.Dock = DockStyle.Fill;
            editor.Controls.Add(_ativo, 2, 1);
            var actions = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.RightToLeft, WrapContents = false };
            actions.Controls.Add(UiStyle.Button("Salvar", Salvar));
            editor.Controls.Add(actions, 0, 2);
            editor.SetColumnSpan(actions, 4);
            editorGroup.Controls.Add(editor);

            root.Controls.Add(filtros, 0, 0);
            root.Controls.Add(_grid, 0, 1);
            root.Controls.Add(editorGroup, 0, 2);
            Controls.Add(root);
        }

        private Control CriarFiltros()
        {
            var group = UiStyle.GroupBox("Pesquisa");
            var layout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 6, RowCount = 2 };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 55));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 108));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 36));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            _filtroAtivo.DropDownStyle = ComboBoxStyle.DropDownList;
            _filtroAtivo.Items.AddRange(new object[] { "Todos", "Ativos", "Inativos" });
            _filtroAtivo.SelectedIndex = 0;

            UiStyle.AddField(layout, "Nome", _filtroNome, 0, 0);
            UiStyle.AddField(layout, "Ativo", _filtroAtivo, 2, 0);
            layout.Controls.Add(UiStyle.Button("Pesquisar", Pesquisar), 4, 0);
            layout.Controls.Add(UiStyle.Button("Novo", Limpar), 5, 0);

            var pager = CriarPaginacao();
            layout.Controls.Add(pager, 0, 1);
            layout.SetColumnSpan(pager, 6);
            group.Controls.Add(layout);
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
            pager.Controls.Add(UiStyle.Button("Anterior", PaginaAnterior), 1, 0);
            pager.Controls.Add(UiStyle.Button("Proxima", ProximaPagina), 2, 0);
            return pager;
        }

        private void CriarColunasGrid()
        {
            _grid.Columns.Clear();
            _grid.Columns.Add(UiStyle.TextColumn("Id", "Id", 60, null, DataGridViewContentAlignment.MiddleRight));
            _grid.Columns.Add(UiStyle.TextColumn("Nome", "Nome", 220));
            _grid.Columns.Add(UiStyle.TextColumn("ValorBase", "Valor base", 110, "C2", DataGridViewContentAlignment.MiddleRight));
            _grid.Columns.Add(UiStyle.TextColumn("PercentualImposto", "Imposto %", 95, "N2", DataGridViewContentAlignment.MiddleRight));
            _grid.Columns.Add(new DataGridViewCheckBoxColumn { DataPropertyName = "Ativo", HeaderText = "Ativo", FillWeight = 70, MinimumWidth = 60 });
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
                Bootstrapper.ServicoService().Salvar(new Servico
                {
                    Id = _idAtual,
                    Nome = _nome.Text,
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

    }
}
