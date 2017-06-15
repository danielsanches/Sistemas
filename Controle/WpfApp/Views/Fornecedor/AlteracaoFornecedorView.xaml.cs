using Application.Services.Fornecedor;
using System.Windows;
using System.Windows.Input;

namespace WpfApp.Views.Fornecedor
{
    /// <summary>
    /// Interaction logic for AlteracaoFornecedorView.xaml
    /// </summary>
    public partial class AlteracaoFornecedorView : Window
    {
        private readonly FornecedorAppService _fornecedorAppService;
        private int _idFornecedor;

        public AlteracaoFornecedorView()
        {
            InitializeComponent();
            _fornecedorAppService = new FornecedorAppService();
        }

        public void PreencherCampos(Domain.Model.Fornecedor fornecedor)
        {
            _idFornecedor = fornecedor.Id;
            TxtFornecedor.Text = fornecedor.NomeFantasia;
            TxtFoneMovel.Text = fornecedor.Celular;
            TxtEmail.Text = fornecedor.Email;
            TxtFoneFixo.Text = fornecedor.FoneFixo;
            TxtCpfCnpj.Text = fornecedor.CpfCnpj;
            CheckStatus.IsChecked = fornecedor.Status.Equals(EnumSituacao.Ativo.ToString());
        }

        private void BtnSalvar_Click(object sender, RoutedEventArgs e)
        {
            var resposta = _fornecedorAppService.Alterar(new RequestFornecedor
            {
                Id = _idFornecedor,
                NomeFantazia = TxtFornecedor.Text,
                Celular = TxtFoneMovel.Text,
                Email = TxtEmail.Text,
                FoneFixo = TxtFoneFixo.Text,
                CpfCnpj = TxtCpfCnpj.Text,
                Status = CheckStatus.IsChecked.HasValue && CheckStatus.IsChecked.Value
                        ? EnumSituacao.Ativo.ToString()
                        : EnumSituacao.Invativo.ToString()
            });

            if (!resposta.Sucesso)
            {
                MessageBox.Show(resposta.Mensagem, "Atenção", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            MessageBox.Show(resposta.Mensagem, "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
            Close();
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TxtCpfCnpj_KeyDown(object sender, KeyEventArgs e)
        {
            OnlyNumber(sender, e);
        }

        private void TxtFoneMovel_KeyDown(object sender, KeyEventArgs e)
        {
            OnlyNumber(sender, e);
        }

        private void TxtFoneFixo_KeyDown(object sender, KeyEventArgs e)
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
