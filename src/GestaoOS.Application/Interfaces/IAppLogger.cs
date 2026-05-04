using System;

namespace GestaoOS.Application.Interfaces
{
    public interface IAppLogger
    {
        void Info(string message);
        void Error(Exception exception, string message);
    }
}
