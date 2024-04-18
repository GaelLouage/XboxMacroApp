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

namespace XboxMacroApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IJsonSerivce _jsonService;
        private Controller? _controller;
        private ProgramModel? _programModel;
        public MainWindow(IJsonSerivce jsonSerivce)
        {
            // add the dependency
            _jsonService = jsonSerivce;
            _controller = new Controller(UserIndex.One);
        }

        public MainWindow() :
            // constructor chaining to call other constructor on class
            this(new JsonSerivce(Constant.FILENAME))
        {
            InitializeComponent();
            btnAssigner.IsEnabled = false;
            btnDelete.IsEnabled = false;
        }
        private async void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!_controller.IsConnected)
                {
                    MessageBox.Show("No controller detected! Install a controller before using the app.",
                        "Controller not found!",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    Close();
                }
                // get all the programs
                var programs = await _jsonService.GetProgramsAsync();
                //null check
                if (programs is not null)
                    LvPrograms.ItemsSource = await _jsonService.GetProgramsAsync();
                // TODO:separate this method;
                // task on other thread to keep the button check running
                await Task.Run(async () =>
                {
                    while (true)
                    {
                        try
                        {
                            var state = _controller.GetState();
                            var getKeyStatePressValue = KeyStateDictionary.Get(state).FirstOrDefault(x => x.Value is true);

                            var getProgramWithKeyPresseValue = (await _jsonService.GetProgramsAsync())
                            .FirstOrDefault(x => x.AssignedKey == getKeyStatePressValue.Key);
                            if (getKeyStatePressValue.Value is true && getKeyStatePressValue.Key != GamepadButtonFlags.None)
                            {
                                if (getProgramWithKeyPresseValue is not null)
                                {
                                    // TODO: check if program is already running if no start else do nothing
                                        Process.Start(getProgramWithKeyPresseValue.FilePath);
                                }
                            }


                        }
                        catch { }
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
        // program buttons
        private async void btnAddProgram_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // get the file dialog path of exe
                var filePath = FileHelper.OpenFileDialogWithPath();
                // file name of the program
                var ProgramName = filePath.GetFileName();
                // add the new selected program to the file
                var (IsSuccess, Message) = await _jsonService.AddProgramAsync(
                    new Models.ProgramModel
                    {
                        FilePath = filePath,
                        FileName = ProgramName
                    });
                // if added program is successfull
                if (IsSuccess)
                {
                    LvPrograms.ItemsSource = await _jsonService.GetProgramsAsync();
                    MessageBox.Show(Message, "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                MessageBox.Show(Message, "Failed", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LvPrograms_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnAssigner.IsEnabled = true;
            btnDelete.IsEnabled = true;
            if ((LvPrograms.SelectedValue as ProgramModel) is ProgramModel program)
            {
                _programModel = program;
            }
        }
        // btns action
        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            btnDelete.IsEnabled = false;
            if (_programModel is not null)
            {
                var (Success, Message) = await _jsonService.DeleteProgramAsync( _programModel);
                if(Success)
                {
                    LvPrograms.ItemsSource = await _jsonService.GetProgramsAsync() ?? new List<ProgramModel>();
                    MessageBox.Show($"{Message}","Success", MessageBoxButton.OK, MessageBoxImage.Hand);
                    return;
                }
                MessageBox.Show($"{Message}", "Failed", MessageBoxButton.OK, MessageBoxImage.Hand);
                return;
            }
          
        }
        private void btnAssigner_Click(object sender, RoutedEventArgs e)
        {
            if (_programModel is not null)
            {
                var btnAssignForm = new FormButtonAssigner(_programModel, _jsonService);
                btnAssignForm.Show();
                this.Hide();
            }
        }
        // tab buttons
        private void txtPrograms_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            InnerGridPrograms.Visibility = Visibility.Visible;
        }

        private void txtAssingButtons_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            InnerGridPrograms.Visibility = Visibility.Hidden;
        }
    }
}