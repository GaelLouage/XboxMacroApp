using SharpDX.XInput;
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
using System.Windows.Shapes;
using XboxMacroApp.Models;
using XboxMacroApp.Services.Interfaces;

namespace XboxMacroApp
{
    /// <summary>
    /// Interaction logic for FormButtonAssigner.xaml
    /// </summary>
    public partial class FormButtonAssigner : Window
    {
        private readonly IJsonSerivce _jsonSerivce;
        private ProgramModel _program;
        private GamepadButtonFlags _buttonChosen;
        public FormButtonAssigner(ProgramModel program, IJsonSerivce jsonSerivce)
        {
            InitializeComponent();
            _program = program;
            _jsonSerivce = jsonSerivce;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            // set the combobox with gamepad controller values
            var controllerEnums = Enum.GetValues(typeof(GamepadButtonFlags))
                .Cast<GamepadButtonFlags>();
            txtProgramName.Text = _program.FileName;
            cmbButtons.ItemsSource = controllerEnums;
            btnAssignKey.IsEnabled = false;
        }

        private async void btnAssignKey_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"Are you sure you want to assign?",
"Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                // write to db
                var (IsSuccess, Message) = await _jsonSerivce.UpdateKeyAsync(_program,_buttonChosen);
                if(!IsSuccess)
                {
                    MessageBox.Show($"{Message}", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                // go to main window
                var mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
           
        }

        private void cmbButtons_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnAssignKey.IsEnabled = true;
            //set the selected button
            var selected = (GamepadButtonFlags)cmbButtons.SelectedItem;
            _buttonChosen = selected;

        }
    }
}
