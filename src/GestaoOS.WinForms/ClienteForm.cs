using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using GestaoOS.Domain.Entities;
using GestaoOS.Domain.Enums;
using GestaoOS.Domain.Filters;

namespace GestaoOS.WinForms
{
    public partial class ClienteForm : Form
    {
        private BindingList<Cliente> _clientes = new BindingList<Cliente>();
        private int _pagina = 1;
        private int _totalPaginas = 1;
        private int _idAtual;

        public ClienteForm()
        {
            InitializeComponent();
            if (UiStyle.IsDesignMode(this))
            {
                return;
            }
            _tipo.DataSource = Enum.GetValues(typeof(TipoCliente));
            AtualizarMascaraTelefone(string.Empty);
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

        private void FiltroDocumento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void Email_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void Tipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            AtualizarMascaraDocumento();
        }

        private void Telefone_Leave(object sender, EventArgs e)
        {
            AtualizarMascaraTelefone(UiStyle.DigitsOnly(_telefone.Text));
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

        private void ExcluirButton_Click(object sender, EventArgs e)
        {
            Excluir();
        }

        private void SalvarButton_Click(object sender, EventArgs e)
        {
            Salvar();
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
