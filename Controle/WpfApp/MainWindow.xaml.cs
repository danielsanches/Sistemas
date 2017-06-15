using System.Windows;
using WpfApp.Views.Fornecedor;
using WpfApp.Views.Usuario;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            new LogInView().ShowDialog();
            VerificarAtenticacao();
        }

        private void VerificarAtenticacao()
        {
            if (!App.Atenticado)
                Close();
        }

        private void BbtnGrupos_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnSubGrupos_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnFornecedor_Click(object sender, RoutedEventArgs e)
        {
            new FornecedorView().ShowDialog();
        }
    }
}
