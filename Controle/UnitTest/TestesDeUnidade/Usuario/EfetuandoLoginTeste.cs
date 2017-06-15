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
        private Criptografia _criptografia;

        [TestInitialize]
        public void Initializer()
        {
            _criptografia = new Criptografia();
            var cript = _criptografia.Criptografar("admin");
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
            UsuarioBusiness.ValidarLogin(_usuario);
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void NaoConsigoLogarSemSenhaTeste()
        {
            _usuario.Senha = "";
            UsuarioBusiness.ValidarLogin(_usuario);
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void NaoConsigoLogarComUsuarioOuSenhaIncorretosTeste()
        {
            _usuario.Senha = "123Senha";
            UsuarioBusiness.ValidarLogin(_usuario);
        }
    }
}
