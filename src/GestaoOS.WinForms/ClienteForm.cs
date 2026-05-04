using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using GestaoOS.Domain.Entities;
using GestaoOS.Domain.Enums;
using GestaoOS.Domain.Filters;

namespace GestaoOS.WinForms
{
    public class ClienteForm : Form
    {
        private readonly TextBox _filtroNome = new TextBox();
        private readonly TextBox _filtroDocumento = new TextBox();
        private readonly ComboBox _filtroAtivo = new ComboBox();
        private readonly DataGridView _grid = new DataGridView();
        private readonly TextBox _nome = new TextBox();
        private readonly TextBox _documento = new TextBox();
        private readonly ComboBox _tipo = new ComboBox();
        private readonly TextBox _email = new TextBox();
        private readonly TextBox _telefone = new TextBox();
        private readonly CheckBox _ativo = new CheckBox();
        private readonly Label _paginaLabel = new Label();
        private BindingList<Cliente> _clientes = new BindingList<Cliente>();
        private int _pagina = 1;
        private int _idAtual;

        public ClienteForm()
        {
            Text = "Clientes";
            Size = new Size(1050, 660);
            StartPosition = FormStartPosition.CenterParent;
            BuildLayout();
            Carregar();
        }

        private void BuildLayout()
        {
            var root = new TableLayoutPanel { Dock = DockStyle.Fill, RowCount = 3, ColumnCount = 1 };
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 64));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 190));

            var filtros = new FlowLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(8) };
            filtros.Controls.Add(new Label { Text = "Nome", Width = 45, TextAlign = ContentAlignment.MiddleLeft });
            filtros.Controls.Add(_filtroNome);
            filtros.Controls.Add(new Label { Text = "Documento", Width = 76, TextAlign = ContentAlignment.MiddleLeft });
            filtros.Controls.Add(_filtroDocumento);
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

            var editor = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(8), ColumnCount = 4, RowCount = 4 };
            editor.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90));
            editor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            editor.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90));
            editor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            AddField(editor, "Nome", _nome, 0, 0);
            AddField(editor, "Documento", _documento, 2, 0);
            _tipo.DropDownStyle = ComboBoxStyle.DropDownList;
            _tipo.DataSource = Enum.GetValues(typeof(TipoCliente));
            AddField(editor, "Tipo", _tipo, 0, 1);
            AddField(editor, "E-mail", _email, 2, 1);
            AddField(editor, "Telefone", _telefone, 0, 2);
            _ativo.Text = "Ativo";
            _ativo.Checked = true;
            editor.Controls.Add(_ativo, 2, 2);
            editor.Controls.Add(Button("Salvar", Salvar), 1, 3);
            editor.Controls.Add(Button("Excluir", Excluir), 3, 3);

            root.Controls.Add(filtros, 0, 0);
            root.Controls.Add(_grid, 0, 1);
            root.Controls.Add(editor, 0, 2);
            Controls.Add(root);
        }

        private void Carregar()
        {
            UiExceptionHandler.Run(() =>
            {
                var filtro = new ClienteFiltro
                {
                    Nome = _filtroNome.Text,
                    Documento = _filtroDocumento.Text,
                    Ativo = _filtroAtivo.SelectedIndex == 0 ? (bool?)null : _filtroAtivo.SelectedIndex == 1
                };
                var resultado = Bootstrapper.ClienteService().Pesquisar(filtro, new Paginacao { Pagina = _pagina, TamanhoPagina = 20 });
                _clientes = new BindingList<Cliente>(new System.Collections.Generic.List<Cliente>(resultado.Itens));
                _grid.DataSource = _clientes;
                _paginaLabel.Text = "Pagina " + _pagina + " / Total " + resultado.Total;
            });
        }

        private void Salvar()
        {
            UiExceptionHandler.Run(() =>
            {
                var cliente = new Cliente
                {
                    Id = _idAtual,
                    Nome = _nome.Text,
                    Documento = _documento.Text,
                    Tipo = (TipoCliente)_tipo.SelectedItem,
                    Email = _email.Text,
                    Telefone = _telefone.Text,
                    Ativo = _ativo.Checked
                };
                Bootstrapper.ClienteService().Salvar(cliente);
                Limpar();
                Carregar();
            });
        }

        private void Excluir()
        {
            if (_idAtual == 0)
            {
                return;
            }

            if (MessageBox.Show("Excluir cliente selecionado?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            UiExceptionHandler.Run(() =>
            {
                Bootstrapper.ClienteService().Excluir(_idAtual);
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

            var cliente = (Cliente)_grid.CurrentRow.DataBoundItem;
            _idAtual = cliente.Id;
            _nome.Text = cliente.Nome;
            _documento.Text = cliente.Documento;
            _tipo.SelectedItem = cliente.Tipo;
            _email.Text = cliente.Email;
            _telefone.Text = cliente.Telefone;
            _ativo.Checked = cliente.Ativo;
        }

        private void Limpar()
        {
            _idAtual = 0;
            _nome.Clear();
            _documento.Clear();
            _email.Clear();
            _telefone.Clear();
            _ativo.Checked = true;
            _tipo.SelectedIndex = 0;
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
