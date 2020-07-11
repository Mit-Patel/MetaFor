using System;
using System.Windows;
using System.Windows.Media;

namespace MetaFor_Demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItemHome_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to Home Page
            frameMain.Source = new Uri("pgHome.xaml", UriKind.RelativeOrAbsolute);

            // Change the background color of the Active Page and inactive Page
            menuItemHome.Background = Application.Current.Resources["MenuItemActiveBgColor"] as SolidColorBrush;
            menuItemAboutUs.Background = Application.Current.Resources["MenuItemBgColor"] as SolidColorBrush; 
        }
    
        private void MenuItemAboutUs_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to AboutUs Page
            frameMain.Source = new Uri("pgAboutUs.xaml", UriKind.RelativeOrAbsolute);

            // Change the background color of the Active Page and inactive Page
            menuItemAboutUs.Background = Application.Current.Resources["MenuItemActiveBgColor"] as SolidColorBrush;
            menuItemHome.Background = Application.Current.Resources["MenuItemBgColor"] as SolidColorBrush;
        }               
    }
}
