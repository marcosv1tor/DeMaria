using GestaoOS.Application.Exceptions;
using GestaoOS.Application.Services;
using GestaoOS.Domain.Entities;
using GestaoOS.Domain.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GestaoOS.Tests
{
    [TestClass]
    public class ClienteServicoValidationTests
    {
        [TestMethod]
        public void ClienteService_Validar_RejeitaNomeObrigatorio()
        {
            var cliente = new Cliente
            {
                Documento = "123",
                Tipo = TipoCliente.Fisica
            };

            Assert.ThrowsException<ValidacaoException>(() => ClienteService.Validar(cliente));
        }

        [TestMethod]
        public void ServicoService_Validar_RejeitaValorBaseZero()
        {
            var servico = new Servico
            {
                Nome = "Servico",
                ValorBase = 0,
                PercentualImposto = 10
            };

            Assert.ThrowsException<ValidacaoException>(() => ServicoService.Validar(servico));
        }

        [TestMethod]
        public void ServicoService_Validar_RejeitaImpostoMaiorQueCem()
        {
            var servico = new Servico
            {
                Nome = "Servico",
                ValorBase = 10,
                PercentualImposto = 100.01m
            };

            Assert.ThrowsException<ValidacaoException>(() => ServicoService.Validar(servico));
        }
    }
}
