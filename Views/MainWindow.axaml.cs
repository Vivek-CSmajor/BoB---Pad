using Avalonia.Controls;
using BoB___Pad.ViewModels;

namespace BoB___Pad.Views;

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }
    }    


