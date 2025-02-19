using Avalonia.Controls;
using GreetingApp.ViewModels;

namespace GreetingApp.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }
    }
}
