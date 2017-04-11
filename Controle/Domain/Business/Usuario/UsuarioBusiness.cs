namespace Domain.Business.Usuario
{
    using System;

    public class UsuarioBusiness
    {
        public void ValidarLogin(RequestUsuarioBusiness request)
        {
            if (string.IsNullOrWhiteSpace(request.NomeLogin))
                throw new InvalidOperationException("Favor informar o nome de usuário.");
            if (string.IsNullOrWhiteSpace(request.Senha))
                throw new InvalidOperationException("Favor informar a senha.");
            if (request.Usuario == null)
                throw new InvalidOperationException("Nome de usuário ou senha incorretos.");
            if (!request.Usuario.NomeLogin.Equals(request.NomeLogin) || !new Criptografia().AutenticarHashMd5(request.Senha, request.Usuario.Senha))
                throw new InvalidOperationException("Nome de usuário ou senha incorretos.");
        }

        public void ValidarCadastro(RequestUsuarioBusiness request)
        {
            if (string.IsNullOrWhiteSpace(request.Nome))
                throw new InvalidOperationException("Favor informar o nome de usuário.");
            if (string.IsNullOrWhiteSpace(request.NomeLogin))
                throw new InvalidOperationException("Favor informar o nome de login do usuário.");
            if (string.IsNullOrWhiteSpace(request.Senha))
                throw new InvalidOperationException("Favor informar a senha.");
            if (string.IsNullOrWhiteSpace(request.Email))
                throw new InvalidOperationException("Favor informar um email para cadastro.");
            if (!new ValidacoesCadastro().ValidarEmail(request.Email))
                throw new InvalidOperationException("Favor informar um email válido.");
            if (string.IsNullOrWhiteSpace(request.ConfirmaSenha))
                throw new InvalidOperationException("Favor confirmar a senha.");
            if (request.Senha != request.ConfirmaSenha)
                throw new InvalidOperationException("As senhas estão diferentes. Favor digitar novamente.");
        }
    }
}
