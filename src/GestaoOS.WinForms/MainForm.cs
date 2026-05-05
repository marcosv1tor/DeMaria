using System;
using System.Windows.Forms;

namespace GestaoOS.WinForms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            ConectarEventos();
        }

        private void ConectarEventos()
        {
            _clientesMenuItem.Click += ClientesMenuItem_Click;
            _servicosMenuItem.Click += ServicosMenuItem_Click;
            _ordensMenuItem.Click += OrdensMenuItem_Click;
            _relatoriosMenuItem.Click += RelatoriosMenuItem_Click;
            _clientesButton.Click += ClientesMenuItem_Click;
            _servicosButton.Click += ServicosMenuItem_Click;
            _ordensButton.Click += OrdensMenuItem_Click;
            _relatorioButton.Click += RelatoriosMenuItem_Click;
        }

        private void ClientesMenuItem_Click(object sender, EventArgs e)
        {
            Abrir(new ClienteForm());
        }

        private void ServicosMenuItem_Click(object sender, EventArgs e)
        {
            Abrir(new ServicoForm());
        }

        private void OrdensMenuItem_Click(object sender, EventArgs e)
        {
            Abrir(new OrdemServicoForm());
        }

        private void RelatoriosMenuItem_Click(object sender, EventArgs e)
        {
            Abrir(new RelatorioOrdensForm());
        }

        private static void Abrir(Form form)
        {
            form.ShowDialog();
        }
    }
}
