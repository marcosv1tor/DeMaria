using System;
using System.Windows.Forms;
using GestaoOS.Application.Exceptions;
using GestaoOS.Infrastructure.Errors;
using Npgsql;

namespace GestaoOS.WinForms
{
    internal static class UiExceptionHandler
    {
        public static void Run(Action action)
        {
            try
            {
                action();
            }
            catch (ValidacaoException ex)
            {
                MessageBox.Show(ex.Message, "Validacao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (RegraNegocioException ex)
            {
                MessageBox.Show(ex.Message, "Regra de negocio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (ConcorrenciaException ex)
            {
                MessageBox.Show(ex.Message, "Concorrencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (PostgresException ex)
            {
                Bootstrapper.Logger.Error(ex, "Erro PostgreSQL");
                MessageBox.Show(PostgresErrorTranslator.ToFriendlyMessage(ex), "Banco de dados", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                Bootstrapper.Logger.Error(ex, "Erro inesperado na interface");
                MessageBox.Show("Nao foi possivel concluir a operacao. Consulte o log tecnico.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
