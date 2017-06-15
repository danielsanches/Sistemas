namespace Application.Services.UsuarioLogin
{
    using Domain.Business.Usuario;
    using Domain.Interfaces;
    using Domain.Model;
    using System;
    using System.Linq;
    using Infra.Reposiotry;

    public class UsuarioService
    {
        private IRepository<Usuario> _usuarioRepository;
        private Criptografia _cripto;

        public UsuarioService()
        {
            _usuarioRepository = new UsuarioRepository();
            _cripto = new Criptografia();
        }

        public ResponseUsuario Logar(RequestUsuario request)
        {
            try
            {
                var usuario = _usuarioRepository.ObterPor(x => x.NomeLogin.Trim().Equals(request.NomeLogin)).FirstOrDefault();
                UsuarioBusiness.ValidarLogin(new RequestUsuarioBusiness
                {
                    NomeLogin = request.NomeLogin,
                    Senha = request.Senha,
                    Usuario = usuario
                });

                return new ResponseUsuario { Sucesso = true, Usuario = usuario };
            }
            catch (Exception ex)
            {
                return new ResponseUsuario { Sucesso = false, Mensagem = ex.Message };
            }
        }

        public ResponseUsuario Cadastrar(RequestUsuario request)
        {
            try
            {
                UsuarioBusiness.ValidarCadastro(new RequestUsuarioBusiness
                {
                    Nome = request.Nome,
                    NomeLogin = request.NomeLogin,
                    Senha = request.Senha,
                    ConfirmaSenha = request.ConfirmaSenha,
                    Email = request.Email
                });

                var usuario = new Usuario
                {
                    NomeLogin = request.NomeLogin,
                    Nome = request.Nome.Trim(),
                    Senha = _cripto.Criptografar(request.Senha.Trim()),
                    Email = request.Email.Trim()
                };

                _usuarioRepository.Cadastrar(usuario);
                _usuarioRepository.SaveChanges();

                return new ResponseUsuario { Sucesso = true, Mensagem = "Usuário cadastrado com sucesso." };
            }
            catch (Exception ex)
            {
                return new ResponseUsuario { Sucesso = false, Mensagem = ex.Message };
            }
        }

    }
}
