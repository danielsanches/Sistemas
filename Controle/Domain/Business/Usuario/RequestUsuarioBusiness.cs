namespace Domain.Business.Usuario
{
    using Domain.Model;

    public class RequestUsuarioBusiness
    {
        public string Nome { get; set; }
        public string NomeLogin { get; set; }
        public string Senha { get; set; }
        public string ConfirmaSenha { get; set; }
        public string Email { get; set; }

        public Usuario Usuario { get; set; }

    }
}