using System;
using System.Windows.Forms;

namespace GestaoOS.WinForms
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MessageBox.Show("Estrutura inicial criada. As telas serao implementadas nas proximas etapas.", "GestaoOS");
        }
    }
}
