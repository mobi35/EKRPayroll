using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EyeKnowRight.Views
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class ShellView : Window
    {
        public ShellView()
        {
            InitializeComponent();
            ButtonOpen.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            FuckingGrid.Margin = new Thickness(270, 15, 0, 0);
        }
        private void ClosePanel(object sender, RoutedEventArgs e)
        {
            FuckingGrid.Margin = new Thickness(50, 15, 0, 0);
        }
        private void OpenPanel(object sender, RoutedEventArgs e)
        {
            FuckingGrid.Margin = new Thickness(270, 15, 0, 0);
        }
        private void AccountSelect(object sender, RoutedEventArgs e)
        {
            FuckingGrid.Children.Add(new AccountView());

        }
    }
}
