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

        public ServicoForm()
        {
            Text = "Servicos";
            Size = new Size(900, 600);
            StartPosition = FormStartPosition.CenterParent;
            BuildLayout();
            Carregar();
        }

        private void BuildLayout()
        {
            var root = new TableLayoutPanel { Dock = DockStyle.Fill, RowCount = 3, ColumnCount = 1 };
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 64));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 150));

            var filtros = new FlowLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(8) };
            filtros.Controls.Add(new Label { Text = "Nome", Width = 45, TextAlign = ContentAlignment.MiddleLeft });
            filtros.Controls.Add(_filtroNome);
            filtros.Controls.Add(new Label { Text = "Ativo", Width = 44, TextAlign = ContentAlignment.MiddleLeft });
            _filtroAtivo.DropDownStyle = ComboBoxStyle.DropDownList;
            _filtroAtivo.Items.AddRange(new object[] { "Todos", "Ativos", "Inativos" });
            _filtroAtivo.SelectedIndex = 0;
            filtros.Controls.Add(_filtroAtivo);
            filtros.Controls.Add(Button("Pesquisar", Carregar));
            filtros.Controls.Add(Button("Novo", Limpar));
            filtros.Controls.Add(Button("<", () => { if (_pagina > 1) { _pagina--; Carregar(); } }));
            filtros.Controls.Add(_paginaLabel);
            filtros.Controls.Add(Button(">", () => { _pagina++; Carregar(); }));

            _grid.Dock = DockStyle.Fill;
            _grid.AutoGenerateColumns = true;
            _grid.ReadOnly = true;
            _grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _grid.MultiSelect = false;
            _grid.SelectionChanged += (s, e) => SelecionarAtual();

            _valorBase.DecimalPlaces = 2;
            _valorBase.Maximum = 9999999;
            _valorBase.Minimum = 0;
            _percentualImposto.DecimalPlaces = 2;
            _percentualImposto.Maximum = 100;
            _percentualImposto.Minimum = 0;

            var editor = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(8), ColumnCount = 4, RowCount = 3 };
            editor.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100));
            editor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            editor.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120));
            editor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            AddField(editor, "Nome", _nome, 0, 0);
            AddField(editor, "Valor base", _valorBase, 2, 0);
            AddField(editor, "Imposto %", _percentualImposto, 0, 1);
            _ativo.Text = "Ativo";
            _ativo.Checked = true;
            editor.Controls.Add(_ativo, 2, 1);
            editor.Controls.Add(Button("Salvar", Salvar), 1, 2);

            root.Controls.Add(filtros, 0, 0);
            root.Controls.Add(_grid, 0, 1);
            root.Controls.Add(editor, 0, 2);
            Controls.Add(root);
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
                _grid.DataSource = new BindingList<Servico>(new System.Collections.Generic.List<Servico>(resultado.Itens));
                _paginaLabel.Text = "Pagina " + _pagina + " / Total " + resultado.Total;
            });
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
            if (_grid.CurrentRow == null || _grid.CurrentRow.DataBoundItem == null)
            {
                return;
            }

            var servico = (Servico)_grid.CurrentRow.DataBoundItem;
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

        private static Button Button(string text, Action action)
        {
            var button = new Button { Text = text, Width = 90, Height = 28, Margin = new Padding(4) };
            button.Click += (s, e) => action();
            return button;
        }

        private static void AddField(TableLayoutPanel panel, string label, Control control, int col, int row)
        {
            panel.Controls.Add(new Label { Text = label, TextAlign = ContentAlignment.MiddleLeft, Dock = DockStyle.Fill }, col, row);
            control.Dock = DockStyle.Fill;
            panel.Controls.Add(control, col + 1, row);
        }
    }
}
