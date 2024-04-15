using Microsoft.Win32;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using XboxMacroApp.Extensions;
using XboxMacroApp.Helpers;

namespace XboxMacroApp
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

        private void LvPrograms_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void btnAddProgram_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var filePath = FileHelper.OpenFileDialogWithPath();
                var ProgramName = filePath.GetFileName();
                Process.Start(filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}