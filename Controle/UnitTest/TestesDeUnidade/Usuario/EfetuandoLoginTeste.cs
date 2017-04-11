namespace UnitTest.TestesDeUnidade.Usuario
{
    using System;
    using Domain.Model;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Domain.Business.Usuario;

    [TestClass]
    public class EfetuandoLoginTeste
    {
        private RequestUsuarioBusiness _usuario;
        private UsuarioBusiness _usuarioBusiness;
        private Criptografia _criptografia;

        [TestInitialize]
        public void Initializer()
        {
            _usuarioBusiness = new UsuarioBusiness();
            _criptografia = new Criptografia();
            var cript = _criptografia.Criptografar("senha123");
            var usuario = new Usuario { Nome = "admin", Senha = cript };
            _usuario = new RequestUsuarioBusiness
            {
                Nome = "admin",
                Senha = "senha123",
                Usuario = usuario
            };
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void NaoConsigoLogarSemNomeDeUsuarioTeste()
        {
            _usuario.Nome = "";
            _usuarioBusiness.ValidarLogin(_usuario);
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void NaoConsigoLogarSemSenhaTeste()
        {
            _usuario.Senha = "";
            _usuarioBusiness.ValidarLogin(_usuario);
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void NaoConsigoLogarComUsuarioOuSenhaIncorretosTeste()
        {
            _usuario.Senha = "123Senha";
            _usuarioBusiness.ValidarLogin(_usuario);
        }
    }
}
