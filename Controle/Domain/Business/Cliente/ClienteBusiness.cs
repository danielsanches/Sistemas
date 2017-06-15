namespace Domain.Business.Cliente
{
    using Enum;
    using System;
    using System.Linq;

    public class ClienteBusiness
    {
        private ValidacoesCadastro _validar;

        public ClienteBusiness()
        {
            _validar = new ValidacoesCadastro();
        }

        public void ValidarCadastro(RequestClienteBusiness request)
        {
            if (string.IsNullOrWhiteSpace(request.Cliente.Nome))
                throw new InvalidOperationException("Não é possível cadastrar um cliente com o nome inválido");

            if (request.Cadastrar && request.Cliente.Status == EnumSituacao.Inativo.ToString())
                throw new InvalidOperationException("Não é possível cadastrar um cliente com status inativo.");

            if (request.Clientes.Any(x => x.Id != request.Cliente.Id && x.Nome.Trim().ToUpper().Equals(request.Cliente.Nome.ToUpper().Trim())))
                throw new InvalidOperationException("O nome informado já foi cadastrado. Favor informar outro nome.");

            if (!string.IsNullOrWhiteSpace(request.Cliente.Email) && !_validar.ValidarEmail(request.Cliente.Email))
                throw new InvalidOperationException("Não é possível cadastrar um cliente com email inválido.");

            if (!string.IsNullOrWhiteSpace(request.Cliente.Cpf) && !_validar.ValidarCpfCnpj(request.Cliente.Cpf))
                throw new InvalidOperationException("Não é possível cadastrar um cliente com o cpf inválido.");
        }
    }
}
