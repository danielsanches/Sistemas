namespace Application.Services.UsuarioLogin
{
    public class RequestUsuario
    {
        public string Nome { internal get; set; }
        public string Senha { internal get; set; }
        public string ConfirmaSenha { internal get; set; }
        public string NomeLogin { get; set; }
        public string Email { get; set; }
    }
}
