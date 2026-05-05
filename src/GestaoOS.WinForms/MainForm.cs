using System;
using System.Windows.Forms;

namespace GestaoOS.WinForms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
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
