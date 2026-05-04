using GestaoOS.Application.Exceptions;
using Npgsql;

namespace GestaoOS.Infrastructure.Errors
{
    public static class PostgresErrorTranslator
    {
        public static string ToFriendlyMessage(PostgresException exception)
        {
            if (exception == null)
            {
                return "Erro inesperado ao acessar o banco de dados.";
            }

            switch (exception.SqlState)
            {
                case "23505":
                    return "Ja existe um registro com as mesmas informacoes unicas.";
                case "23503":
                    return "Nao e possivel remover ou alterar o registro porque existem vinculos no sistema.";
                case "23514":
                    return "Os dados informados violam uma regra de validacao do banco.";
                default:
                    return "Nao foi possivel concluir a operacao no banco de dados.";
            }
        }

        public static ValidacaoException ToValidationException(PostgresException exception)
        {
            return new ValidacaoException(ToFriendlyMessage(exception));
        }
    }
}
