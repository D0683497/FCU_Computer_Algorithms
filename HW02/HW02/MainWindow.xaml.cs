using System.Windows;

namespace HW02
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Page1 page1 = new Page1();
            Main.Navigate(page1);
        }
    }
}
