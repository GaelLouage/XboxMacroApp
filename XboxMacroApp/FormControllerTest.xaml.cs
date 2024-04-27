using SharpDX.XInput;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using XboxMacroApp.Dictionaries;
using XboxMacroApp.Helpers;
using XboxMacroApp.Services.Interfaces;
using XboxMacroApp.Singletons;

namespace XboxMacroApp
{
    /// <summary>
    /// Interaction logic for FormControllerTest.xaml
    /// </summary>
    public partial class FormControllerTest : Window
    {
        public FormControllerTest()
        {
            InitializeComponent();
            ControllerSingleton.Instance.ControllerTestTaskIsRunning = true;
            imgGoBack.Source = FileHelper.CombineCurrentDirectoryWithPath("leftArrow.png");
        }
        private async void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            await Task.Run(async () =>
            {
                try
                {
                    if (ControllerSingleton.Instance.Controller.IsConnected)
                    {
                        while (ControllerSingleton.Instance.ControllerTestTaskIsRunning)
                        {
                            var state = ControllerSingleton.Instance.Controller.GetState();
                            var getKeyStatePressValue = KeyStateDictionary.Get(state).FirstOrDefault(x => x.Value is true);

                            if (getKeyStatePressValue.Value is true && getKeyStatePressValue.Key != GamepadButtonFlags.None)
                            {
                                Dispatcher?.Invoke(() =>
                                {
                                    txtcontrollerTest.Text = $"key pressed: {getKeyStatePressValue.Key}";
                                });
                            }
                            await Task.Delay(125);
                        }
                    }
                }
                catch { }
            });
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch { }
        }

        private void txtMinimize_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void txtClose_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Environment.Exit(0);

        }

        private void imgGoBack_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var main = new MainWindow();
            main.Show();
            this.Close();
        }
    }
}
