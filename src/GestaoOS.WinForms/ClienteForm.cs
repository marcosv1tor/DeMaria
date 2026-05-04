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
        private readonly MaskedTextBox _documento = new MaskedTextBox();
        private readonly ComboBox _tipo = new ComboBox();
        private readonly TextBox _email = new TextBox();
        private readonly MaskedTextBox _telefone = new MaskedTextBox();
        private readonly CheckBox _ativo = new CheckBox();
        private readonly Label _paginaLabel = new Label();
        private BindingList<Cliente> _clientes = new BindingList<Cliente>();
        private int _pagina = 1;
        private int _totalPaginas = 1;
        private int _idAtual;

        public ClienteForm()
        {
            Text = "Clientes";
            StartPosition = FormStartPosition.CenterParent;
            UiStyle.ApplyForm(this, new Size(1120, 720), new Size(980, 640));
            BuildLayout();
            Carregar();
        }

        private void BuildLayout()
        {
            var root = new TableLayoutPanel { Dock = DockStyle.Fill, RowCount = 3, ColumnCount = 1 };
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 122));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 220));

            var filtros = CriarFiltros();
            UiStyle.ConfigureGrid(_grid);
            CriarColunasGrid();
            _grid.SelectionChanged += (s, e) => SelecionarAtual();

            var editorGroup = UiStyle.GroupBox("Dados do cliente");
            var editor = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 4, RowCount = 4 };
            editor.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90));
            editor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            editor.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90));
            editor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            editor.RowStyles.Add(new RowStyle(SizeType.Absolute, 34));
            editor.RowStyles.Add(new RowStyle(SizeType.Absolute, 34));
            editor.RowStyles.Add(new RowStyle(SizeType.Absolute, 34));
            editor.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            _filtroNome.MaxLength = 150;
            _nome.MaxLength = 150;
            _email.MaxLength = 150;
            UiStyle.AddField(editor, "Nome", _nome, 0, 0);
            UiStyle.AddField(editor, "Documento", _documento, 2, 0);
            _tipo.DropDownStyle = ComboBoxStyle.DropDownList;
            _tipo.DataSource = Enum.GetValues(typeof(TipoCliente));
            _tipo.SelectedIndexChanged += (s, e) => AtualizarMascaraDocumento();
            _documento.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            _telefone.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            _telefone.Leave += (s, e) => AtualizarMascaraTelefone(UiStyle.DigitsOnly(_telefone.Text));
            UiStyle.ConfigureNoSpaces(_email);
            AtualizarMascaraTelefone(string.Empty);
            UiStyle.AddField(editor, "Tipo", _tipo, 0, 1);
            UiStyle.AddField(editor, "E-mail", _email, 2, 1);
            UiStyle.AddField(editor, "Telefone", _telefone, 0, 2);
            _ativo.Text = "Ativo";
            _ativo.Checked = true;
            _ativo.Dock = DockStyle.Fill;
            editor.Controls.Add(_ativo, 2, 2);
            var actions = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.RightToLeft, WrapContents = false };
            actions.Controls.Add(UiStyle.Button("Excluir", Excluir));
            actions.Controls.Add(UiStyle.Button("Salvar", Salvar));
            editor.Controls.Add(actions, 0, 3);
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
            var layout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 8, RowCount = 2 };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 32));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 95));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 28));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 55));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 108));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 38));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 44));

            _filtroAtivo.DropDownStyle = ComboBoxStyle.DropDownList;
            _filtroAtivo.Items.AddRange(new object[] { "Todos", "Ativos", "Inativos" });
            _filtroAtivo.SelectedIndex = 0;
            UiStyle.ConfigureDigitsOnly(_filtroDocumento, 14);

            UiStyle.AddField(layout, "Nome", _filtroNome, 0, 0);
            UiStyle.AddField(layout, "Documento", _filtroDocumento, 2, 0);
            UiStyle.AddField(layout, "Ativo", _filtroAtivo, 4, 0);
            layout.Controls.Add(UiStyle.Button("Pesquisar", Pesquisar), 6, 0);
            layout.Controls.Add(UiStyle.Button("Novo", Limpar), 7, 0);

            var pager = CriarPaginacao();
            layout.Controls.Add(pager, 0, 1);
            layout.SetColumnSpan(pager, 8);
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
            pager.Controls.Add(UiStyle.PagerButton("Anterior", PaginaAnterior), 1, 0);
            pager.Controls.Add(UiStyle.PagerButton("Proxima", ProximaPagina), 2, 0);
            return pager;
        }

        private void CriarColunasGrid()
        {
            _grid.Columns.Clear();
            _grid.Columns.Add(UiStyle.TextColumn("Id", "Id", 55, null, DataGridViewContentAlignment.MiddleRight));
            _grid.Columns.Add(UiStyle.TextColumn("Nome", "Nome", 180));
            _grid.Columns.Add(UiStyle.TextColumn("Documento", "Documento", 130));
            _grid.Columns.Add(UiStyle.TextColumn("Tipo", "Tipo", 90));
            _grid.Columns.Add(UiStyle.TextColumn("Email", "E-mail", 180));
            _grid.Columns.Add(UiStyle.TextColumn("Telefone", "Telefone", 120));
            _grid.Columns.Add(new DataGridViewCheckBoxColumn { DataPropertyName = "Ativo", HeaderText = "Ativo", FillWeight = 70, MinimumWidth = 60 });
        }

        private void Carregar()
        {
            UiExceptionHandler.Run(() =>
            {
                var filtro = new ClienteFiltro
                {
                    Nome = _filtroNome.Text,
                    Documento = UiStyle.DigitsOnly(_filtroDocumento.Text),
                    Ativo = _filtroAtivo.SelectedIndex == 0 ? (bool?)null : _filtroAtivo.SelectedIndex == 1
                };
                var resultado = Bootstrapper.ClienteService().Pesquisar(filtro, new Paginacao { Pagina = _pagina, TamanhoPagina = 20 });
                var totalPaginas = Math.Max(1, (int)Math.Ceiling(resultado.Total / 20m));
                if (_pagina > totalPaginas)
                {
                    _pagina = totalPaginas;
                    Carregar();
                    return;
                }

                _totalPaginas = totalPaginas;
                _clientes = new BindingList<Cliente>(new System.Collections.Generic.List<Cliente>(resultado.Itens));
                _grid.DataSource = _clientes;
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

                var cliente = new Cliente
                {
                    Id = _idAtual,
                    Nome = _nome.Text.Trim(),
                    Documento = UiStyle.DigitsOnly(_documento.Text),
                    Tipo = (TipoCliente)_tipo.SelectedItem,
                    Email = _email.Text.Trim(),
                    Telefone = UiStyle.DigitsOnly(_telefone.Text),
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
            if (_grid.CurrentRow == null || _grid.CurrentRow.IsNewRow)
            {
                return;
            }

            var cliente = _grid.CurrentRow.DataBoundItem as Cliente;
            if (cliente == null)
            {
                return;
            }

            _idAtual = cliente.Id;
            _nome.Text = cliente.Nome;
            _tipo.SelectedItem = cliente.Tipo;
            AtualizarMascaraDocumento();
            _documento.Text = UiStyle.DigitsOnly(cliente.Documento);
            _email.Text = cliente.Email;
            AtualizarMascaraTelefone(UiStyle.DigitsOnly(cliente.Telefone));
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
            AtualizarMascaraDocumento();
            AtualizarMascaraTelefone(string.Empty);
        }

        private void AtualizarMascaraDocumento()
        {
            var digitos = UiStyle.DigitsOnly(_documento.Text);
            _documento.Mask = _tipo.SelectedItem is TipoCliente tipo && tipo == TipoCliente.Juridica
                ? "00.000.000/0000-00"
                : "000.000.000-00";
            _documento.Text = digitos;
        }

        private void AtualizarMascaraTelefone(string digitos)
        {
            _telefone.Mask = digitos.Length == 0 || digitos.Length > 10 ? "(00) 00000-0000" : "(00) 0000-0000";
            _telefone.Text = digitos;
        }

        private bool EntradaValida()
        {
            if (string.IsNullOrWhiteSpace(_nome.Text))
            {
                MessageBox.Show("Preencha o campo Nome.", "Validacao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _nome.Focus();
                return false;
            }

            var documento = UiStyle.DigitsOnly(_documento.Text);
            if (!(_tipo.SelectedItem is TipoCliente tipo))
            {
                MessageBox.Show("Selecione o campo Tipo.", "Validacao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _tipo.Focus();
                return false;
            }

            var esperado = tipo == TipoCliente.Juridica ? 14 : 11;
            if (documento.Length != esperado)
            {
                MessageBox.Show("Informe um documento com " + esperado + " digitos.", "Validacao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _documento.Focus();
                return false;
            }

            var telefone = UiStyle.DigitsOnly(_telefone.Text);
            if (telefone.Length > 0 && telefone.Length != 10 && telefone.Length != 11)
            {
                MessageBox.Show("Informe um telefone com 10 ou 11 digitos.", "Validacao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _telefone.Focus();
                return false;
            }

            var email = _email.Text.Trim();
            if (email.Length > 0 && (email.IndexOf("@", StringComparison.Ordinal) <= 0 || email.LastIndexOf(".", StringComparison.Ordinal) < email.IndexOf("@", StringComparison.Ordinal)))
            {
                MessageBox.Show("Informe um e-mail valido.", "Validacao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _email.Focus();
                return false;
            }

            return true;
        }

    }
}
