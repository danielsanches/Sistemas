using Application.Services.UsuarioLogin;
using System.Windows;

namespace WpfApp.Views.Usuario
{
    /// <summary>
    /// Interaction logic for LogInView.xaml
    /// </summary>
    public partial class LogInView : Window
    {
        private readonly UsuarioService _usuarioService;

        public LogInView()
        {
            InitializeComponent();
            _usuarioService = new UsuarioService();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            var resposta = _usuarioService.Logar(new RequestUsuario
            {
                NomeLogin = TxtUsuario.Text,
                Senha = TxtSenha.Password
            });

            if (!resposta.Sucesso)
            {
                MessageBox.Show(resposta.Mensagem, "Atenção", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            App.Atenticado = true;
            Close();
        }
    }
}
