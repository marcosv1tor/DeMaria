using System;

namespace GestaoOS.Application.Exceptions
{
    public class ConcorrenciaException : Exception
    {
        public ConcorrenciaException(string message)
            : base(message)
        {
        }
    }
}
