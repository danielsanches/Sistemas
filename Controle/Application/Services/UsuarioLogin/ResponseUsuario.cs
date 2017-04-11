namespace Application.Services.UsuarioLogin
{
    using Domain.Model;

    public class ResponseUsuario
    {
        public string Mensagem { get; internal set; }
        public bool Sucesso { get; internal set; }
        public Usuario Usuario { get; set; }
    }
}
