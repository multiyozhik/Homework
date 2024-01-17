using System.Windows;
using WpfClientApp.ViewModels;

namespace WpfClientApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ApplicationWindow : Window
    {
        public ApplicationWindow()
        {
            InitializeComponent();
            DataContext = new ApplicationVM();
        }
    }

    //xmlns:local="clr-namespace:WpfClientApp.ViewModels" d:DataContext="{d:DesignInstance Type=local:ApplicationVM}"
}
