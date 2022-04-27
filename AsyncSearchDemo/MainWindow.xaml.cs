using AsyncSearchDemo.ViewModels;
using System.Windows;

namespace AsyncSearchDemo
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = MainViewModel.LoadViewModel();
        }
    }
}
