using PrimeFactors.Interfaces;
using PrimeFactors.Resources;
using System;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace PrimeFactors.Views
{
    public partial class MainWindow : Window
    {
        public IMainWindowViewModel _MainWindowViewModel { get; set; }

        public MainWindow(IMainWindowViewModel mainWindowViewModel)
        {
            try
            {
                InitializeComponent();

                if (mainWindowViewModel == null)
                {
                    throw new ArgumentNullException(nameof(mainWindowViewModel));
                }

                _MainWindowViewModel = mainWindowViewModel;
                _MainWindowViewModel._MainWindowModel.PropertyChanged += _MainWindowModel_PropertyChanged;

                DataContext = _MainWindowViewModel;

                if(_MainWindowViewModel._MainWindowModel.Algorithms.Count < 1)
                {
                    _MainWindowViewModel.ShowErrorMessage("No Algorithms Found - Please close the application and create one or more Algorithms.");
                    _MainWindowViewModel.EnableInput(false);
                }
            }
            catch (Exception ex)
            {
                //Logging Here
            }
        }

        /// <summary>
        /// It makes for a good user experience to check the input if the user changes the selected algorithm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _MainWindowModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName.Equals("SelectedAlgorithm"))
            {
                if (_MainWindowViewModel._MainWindowModel.Input != null && _MainWindowViewModel._MainWindowModel.Input != "")
                {
                    _MainWindowViewModel.ValidateCalculationStart();
                }
            }
        }

        /// <summary>
        /// To make user interaction as easy as possible I like to provide as much keystroke help as possible.
        /// MVVM is supposed to have very little code behind the XAML, but some code behind is essential when you
        /// want to provide certain features. With this method, the code behind will check if the Enter or Return
        /// keys have been hit when the Input box has focus. If detected the UI will move focus away from the
        /// Input put and will try to Calculate the selected algorithm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InputTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (_MainWindowViewModel._MainWindowModel.CalculateEnabled)
            {
                if (e.Key.Equals(Key.Enter) || e.Key.Equals(Key.Return))
                {
                    Utils.MoveToNextControl(CalculateButton);
                    _MainWindowViewModel.CalculateAction();
                    e.Handled = true;
                }
            }
        }
    }
}
