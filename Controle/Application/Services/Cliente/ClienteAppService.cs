namespace Application.Services.Cliente
{
    using System;
    using Domain.Business.Cliente;
    using Domain.Interfaces;
    using Domain.Model;
    using System.Linq;
    using Domain.Enum;
    using System.Collections.Generic;
    using Infra.Reposiotry;

    public class ClienteAppService
    {
        public ClienteBusiness _clienteBusiness;
        public IRepository<Cliente> _clienteRepository;

        public ClienteAppService()
        {
            _clienteBusiness = new ClienteBusiness();
            _clienteRepository = new ClienteRepository();
        }

        public ResponseCliente Alterar(RequestCliente request)
        {
            try
            {
                var cliente = _clienteRepository.Obter(request.Id);

                cliente.Nome = request.Cliente.Nome.Trim().ToUpper();
                cliente.FoneFixo = string.IsNullOrWhiteSpace(request.Cliente.FoneFixo) ? "" : request.Cliente.FoneFixo.Replace("-", "");
                cliente.FoneMovel1 = string.IsNullOrWhiteSpace(request.Cliente.FoneMovel1) ? "" : request.Cliente.FoneMovel1.Replace("-", "");
                cliente.FoneMovel2 = string.IsNullOrWhiteSpace(request.Cliente.FoneMovel2) ? "" : request.Cliente.FoneMovel2.Replace("-", "");
                cliente.Email = request.Cliente.Email;
                cliente.DataNascimento = request.Cliente.DataNascimento;
                cliente.Cpf = request.Cliente.Cpf;
                cliente.Status = request.Cliente.Status;
                cliente.Vendas = null;

                ValidarCamposCliente(cliente);

                _clienteRepository.Alterar(request.Cliente);
                _clienteRepository.SaveChanges();

                return new ResponseCliente { Sucesso = true, Mensagem = "Cliente alterado com sucesso." };
            }
            catch (Exception ex)
            {
                return new ResponseCliente { Sucesso = false, Mensagem = ex.Message };
            }
        }

        public ResponseCliente Cadastrar(RequestCliente request)
        {
            try
            {
                ValidarCamposCliente(request.Cliente, true);

                request.Cliente.FoneFixo = string.IsNullOrWhiteSpace(request.Cliente.FoneFixo) ? "" : request.Cliente.FoneFixo.Replace("-", "");
                request.Cliente.FoneMovel1 = string.IsNullOrWhiteSpace(request.Cliente.FoneMovel1) ? "" : request.Cliente.FoneMovel1.Replace("-", "");
                request.Cliente.FoneMovel2 = string.IsNullOrWhiteSpace(request.Cliente.FoneMovel2) ? "" : request.Cliente.FoneMovel2.Replace("-", "");
                request.Cliente.Nome = request.Cliente.Nome.ToUpper();
                _clienteRepository.Cadastrar(request.Cliente);
                _clienteRepository.SaveChanges();

                return new ResponseCliente { Sucesso = true, Mensagem = "Cliente cadastrado com sucesso." };
            }
            catch (Exception ex)
            {
                return new ResponseCliente { Sucesso = false, Mensagem = ex.Message };
            }
        }

        public ResponseCliente Obter(int id)
        {
            try
            {
                var cliente = _clienteRepository.Obter(id);
                if (cliente == null)
                    throw new Exception("Cliente não encontrado.");


                return new ResponseCliente { Sucesso = true, Cliente = cliente };
            }
            catch (Exception ex)
            {
                return new ResponseCliente { Sucesso = false, Mensagem = ex.Message };
            }
        }

        public ResponseCliente ConsultarAtivos()
        {
            try
            {
                var lista = _clienteRepository.ObterPor(x => x.Status.Equals(EnumSituacao.Ativo.ToString())).ToList();

                return new ResponseCliente { Sucesso = true, ListaCliente = lista };
            }
            catch (Exception ex)
            {
                return new ResponseCliente { Sucesso = false, Mensagem = ex.Message };
            }
        }

        public ResponseCliente ObterLista(RequestCliente request)
        {
            try
            {
                var lista = FiltrarLista(request).Where(x => x.Status.ToUpper().Equals(request.Cliente.Status.ToUpper())).ToList();

                return new ResponseCliente { Sucesso = true, ListaCliente = lista };
            }
            catch (Exception ex)
            {
                return new ResponseCliente { Sucesso = false, Mensagem = ex.Message };
            }
        }

        public ResponseCliente ObterQuantidades(RequestCliente request)
        {
            try
            {
                var lista = FiltrarLista(request).ToList();

                var qtdAtivo = lista.Count(x => x.Status.Equals(EnumSituacao.Ativo.ToString()));
                var qtdInativo = lista.Count(x => x.Status.Equals(EnumSituacao.Inativo.ToString()));

                return new ResponseCliente { Sucesso = true, QtdAtivo = qtdAtivo, QtdInativo = qtdInativo };
            }
            catch (Exception ex)
            {
                return new ResponseCliente { Sucesso = false, Mensagem = ex.Message };
            }
        }

        private List<Cliente> FiltrarLista(RequestCliente request)
        {
            var cliente = request.Cliente;
            var temDescricao = !string.IsNullOrWhiteSpace(cliente.Nome);
            return _clienteRepository.ObterPor(x => temDescricao
                                                ? x.Nome.Trim().ToUpper().Contains(cliente.Nome.Trim().ToUpper())
                                                : x.Nome != string.Empty
                                              && cliente.DataNascimento.HasValue
                                              ? x.DataNascimento.Value.Month == cliente.DataNascimento.Value.Month
                                              : x.DataNascimento.HasValue).ToList();
        }

        private void ValidarCamposCliente(Cliente cliente, bool cadastro = false)
        {
            _clienteBusiness.ValidarCadastro(new RequestClienteBusiness
            {
                Cadastrar = cadastro,
                Cliente = cliente,
                Clientes = _clienteRepository.ObterTodos().ToList()
            });
        }
    }
}