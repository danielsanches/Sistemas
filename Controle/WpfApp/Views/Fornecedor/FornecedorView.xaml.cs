using Application.Services.Fornecedor;
using System.Windows;

namespace WpfApp.Views.Fornecedor
{
    /// <summary>
    /// Interaction logic for FornecedorView.xaml
    /// </summary>
    public partial class FornecedorView : Window
    {
        private FornecedorAppService _fornecedorAppService;

        public FornecedorView()
        {
            InitializeComponent();
            _fornecedorAppService = new FornecedorAppService();
            PreencherGrid();
        }

        private void PreencherGrid()
        {
            GridFornecedores.ItemsSource = _fornecedorAppService.ObterListaAtivos().ListaFornecedores;
        }

        private void BtnAdicionar_Click(object sender, RoutedEventArgs e)
        {
            new CadastroFornecedorView().ShowDialog();
        }

        private void BtnAlterar_Click(object sender, RoutedEventArgs e)
        {
            var fornecedor = ((Domain.Model.Fornecedor)GridFornecedores.SelectedItem);
            if (fornecedor == null)
            {
                MessageBox.Show("Favor selecionar um item na lista abaixo.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            var alteracaoView = new AlteracaoFornecedorView();
            alteracaoView.PreencherCampos(fornecedor);
            alteracaoView.ShowDialog();
            PreencherGrid();
        }
    }
}
