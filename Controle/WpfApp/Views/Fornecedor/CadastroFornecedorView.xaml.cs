using Application.Services.Fornecedor;
using System.Windows;
using System.Windows.Input;

namespace WpfApp.Views.Fornecedor
{
    /// <summary>
    /// Interaction logic for CadastroFornecedorView.xaml
    /// </summary>
    public partial class CadastroFornecedorView : Window
    {
        private readonly FornecedorAppService _fornecedorAppService;

        public CadastroFornecedorView()
        {
            InitializeComponent();
            LimparCapos();
            _fornecedorAppService = new FornecedorAppService();
        }

        private void BtnSalvar_Click(object sender, RoutedEventArgs e)
        {
            var resposta = _fornecedorAppService.Cadastrar(new RequestFornecedor
            {
                NomeFantazia = TxtFornecedor.Text,
                Celular = TxtFoneMovel.Text,
                Email = TxtEmail.Text,
                FoneFixo = TxtFoneFixo.Text,
                CpfCnpj = TxtCpfCnpj.Text
            });

            if (!resposta.Sucesso)
            {
                MessageBox.Show(resposta.Mensagem, "Atenção", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            MessageBox.Show(resposta.Mensagem, "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
            LimparCapos();
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void LimparCapos()
        {
            TxtFornecedor.Text = string.Empty;
            TxtFoneMovel.Text = string.Empty;
            TxtEmail.Text = string.Empty;
            TxtFoneFixo.Text = string.Empty;
            TxtCpfCnpj.Text = string.Empty;
        }

        private void TxtCpfCnpj_KeyDown(object sender, KeyEventArgs e)
        {
            OnlyNumber(sender, e);
        }

        private void TxtFoneFixo_KeyDown(object sender, KeyEventArgs e)
        {
            OnlyNumber(sender, e);
        }

        private void TxtFoneMovel_KeyDown(object sender, KeyEventArgs e)
        {
            OnlyNumber(sender, e);
        }

        public void OnlyNumber(object sender, KeyEventArgs e)
        {
            if (!(e.Key >= Key.D0 && e.Key <= Key.D9) && !(e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) && e.Key != Key.Back)
            {
                e.Handled = true;
                return;
            }
        }        
    }
}
