using SharpDX.XInput;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using XboxMacroApp.Constants;
using XboxMacroApp.Dictionaries;
using XboxMacroApp.Extensions;
using XboxMacroApp.Helpers;
using XboxMacroApp.Models;
using XboxMacroApp.Services.Classes;
using XboxMacroApp.Services.Interfaces;
using System.Drawing;
using System.Windows.Interop;
using System.Windows.Media;
using XboxMacroApp.Singletons;

namespace XboxMacroApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IJsonSerivce _jsonService;
        private ProgramModel? _programModel;
        private bool _controllerIsConnected;
        public MainWindow(IJsonSerivce jsonSerivce)
        {
            // add the dependency
            _jsonService = jsonSerivce;
        }

        public MainWindow() :
            // constructor chaining to call other constructor on class
            this(new JsonSerivce(Constant.FILENAME))
        {
            InitializeComponent();
            UIHelper.UIUpdate(LvPrograms, imgApp, imgAppControllerOn,imgKey, imgTrash,imgPlus, imgInfo);
        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) 
        {
            // apply movement of mouse when no title bar is available
            try
            {
                DragMove();
            }
            catch {  }
     
        }
        private async void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                ControllerSingleton.Instance.ControllerTestTaskIsRunning = false;
                ControllerSingleton.Instance.ProgramRunnerTaskIsRunning = true;

                await Task.Delay(500);
                // get all the programs
                var programs = await _jsonService.GetProgramsAsync();
                //null check
                if (programs is null)
                {
                    return;
                }
                LvPrograms.ItemsSource = await _jsonService.GetProgramsAsync();
                // task on other thread to keep the button check running
                await Task.Run(async () =>
                {
                    while (ControllerSingleton.Instance.ProgramRunnerTaskIsRunning)
                    {
                        await ControllerState();
                        await Task.Delay(125);
                    }
                });
            }
            catch 
            {
                MessageBox.Show("An Error occured!",
                     "Error",
                     MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task ControllerState()
        {
            try
            {
                if (ControllerSingleton.Instance.Controller.IsConnected)
                {
                    var state = ControllerSingleton.Instance.Controller.GetState();
                    var getKeyStatePressValue = KeyStateDictionary.Get(state).FirstOrDefault(x => x.Value is true);

                    var getProgramWithKeyPresseValue = (await _jsonService.GetProgramsAsync())
                    .FirstOrDefault(x => x.AssignedKey == getKeyStatePressValue.Key);
                    if (getKeyStatePressValue.Value is true && getKeyStatePressValue.Key != GamepadButtonFlags.None)
                    {
                        if (getProgramWithKeyPresseValue is not null)
                        {
                            ProcessHelper.OpenFileWithAssociatedProgram(getProgramWithKeyPresseValue);
                        }
                    }
                }
                UIHelper.UiVisibilityOnConnectedCheck(Dispatcher,imgPlus,imgAppControllerOn,LvPrograms);
            }
            catch { }
        }

       

        private void UiVisibilityOnConnectedCheck(bool controllerConnected)
        {
            Dispatcher?.Invoke(() =>
            {
                imgPlus.IsEnabled = false;
                imgAppControllerOn.Visibility = Visibility.Visible;
                LvPrograms.Visibility = Visibility.Hidden;
                if (controllerConnected)
                {
                    imgAppControllerOn.Visibility = Visibility.Hidden;
                    LvPrograms.Visibility = Visibility.Visible;
                    imgPlus.IsEnabled = true;
                } 
            });
        }

        private void LvPrograms_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            imgKey.IsEnabled = true;
            imgTrash.IsEnabled = true;
            if ((LvPrograms.SelectedValue as ProgramModel) is ProgramModel program)
            {
                _programModel = program;
            }
        }
        #region MenuButtons
        // program buttons
        private async void imgTrash_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            imgTrash.IsEnabled = false;
            if (_programModel is not null)
            {
                var (Success, Message) = await _jsonService.DeleteProgramAsync(_programModel);
                if (Success)
                {
                    LvPrograms.ItemsSource = await _jsonService.GetProgramsAsync() ?? new List<ProgramModel>();
                    MessageBox.Show($"{Message}", "Success", MessageBoxButton.OK, MessageBoxImage.Hand);
                    return;
                }
                MessageBox.Show($"{Message}", "Failed", MessageBoxButton.OK, MessageBoxImage.Hand);
                return;
            }
        }

        private void imgKey_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_programModel is not null)
            {
                var btnAssignForm = new FormButtonAssigner(_programModel, _jsonService);

                btnAssignForm.Show();
                this.Hide();
            }
        }

        private async void imgPlus_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                // get the file dialog path of exe
                var filePath = FileHelper.OpenFileDialogWithPath();
                var isExtensionSupported = FileHelper.IsSupportedFileExtension(filePath);
                if(!isExtensionSupported)
                {
                    MessageBox.Show("File not supported!");
                    return;
                }
     
                // file name of the program
                var ProgramName = filePath.GetFileName();
                // add the new selected program to the file
                var (IsSuccess, Message) = await _jsonService.AddProgramAsync(
                    new Models.ProgramModel
                    {
                        FilePath = filePath,
                        FileName = ProgramName,
                    });
                // if added program is successfull
                if (IsSuccess)
                {
                    LvPrograms.ItemsSource = await _jsonService.GetProgramsAsync();
                    MessageBox.Show(Message, "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                if (!string.IsNullOrEmpty(Message))
                {
                    MessageBox.Show(Message, "Failed", MessageBoxButton.OK, MessageBoxImage.Information);
                }
              
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        private void txtMinimize_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void txtClose_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void imgApp_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var controllerForm = new FormControllerTest();
            ControllerSingleton.Instance.ProgramRunnerTaskIsRunning = false;
            controllerForm.Show();
            this.Hide();
        }

        private void imgInfo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var infoFile = "Readme.txt";
            try
            {
                if (File.Exists(infoFile))
                {
                    Process.Start("Notepad.exe", Path.Combine(Environment.CurrentDirectory, infoFile));
                }
            }
            catch 
            {
                MessageBox.Show($"An error occured. Try to open {infoFile} file manually");
            }
          
        }
    }
}