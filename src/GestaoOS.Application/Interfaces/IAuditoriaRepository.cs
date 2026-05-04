using GestaoOS.Domain.Entities;

namespace GestaoOS.Application.Interfaces
{
    public interface IAuditoriaRepository
    {
        void Registrar(Auditoria auditoria);
    }
}
