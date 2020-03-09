using Microsoft.TeamFoundation.MVVM;
using PrimeFactors.Interfaces;
using PrimeFactors.Resources;
using System;
using System.Security;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace PrimeFactors.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, IMainWindowViewModel
    {
        public IMainWindowModel _MainWindowModel { get; set; }

        public MainWindowViewModel(IMainWindowModel mainWindowModel)
        {
            try
            {
                if(mainWindowModel == null)
                {
                    throw new ArgumentNullException(nameof(mainWindowModel));
                }

                _MainWindowModel = mainWindowModel;

                SendStatusUpdate(Utils.Status_Ready);
            }
            catch (Exception ex)
            {
                //Logging Here
            }
        }

        private void SendStatusUpdate(string message)
        {
            _MainWindowModel.Status = message;
        }

        public async void ShowErrorMessage(string message)
        {
            try
            {
                _MainWindowModel.Output = "";

                await Task.Run(() =>
                {
                    System.Threading.Thread.Sleep(250);

                    _MainWindowModel.Output = message;

                    SendStatusUpdate("Error");
                });
            }
            catch (Exception ex)
            {
                _MainWindowModel.Output = ex.Message;
            }
        }

        public ICommand ExitCommand { get { return new RelayCommand(ExitAction); } }

        public void ExitAction()
        {
            try
            {
                Environment.Exit(0);
            }
            catch (SecurityException ex)
            {
                //Logging Here
            }
        }

        public ICommand CalculateCommand { get { return new RelayCommand(CalculateAction); } }

        public async void CalculateAction()
        {
            try
            {
                if (ValidateCalculationStart())
                {
                    DisplayCalculationStarted();

                    await Task.Run(() =>
                    {
                        //Some algorithms will complete too quickly for the user to see.
                        //We want to make sure the user knows the Calculate button has been clicked and that the input is being worked on.
                        //Adding a short thread sleep so the UI can update with processing messages before the results are displayed.
                        System.Threading.Thread.Sleep(250);

                        //Run the Calculate method from the selected Algorithm
                        Utils.CalculationResult calculationResult = RunCalculation();

                        if (calculationResult._Valid)
                        {
                            _MainWindowModel.Output = calculationResult._Result;
                            SendStatusUpdate(Utils.Status_Ready);                          
                        }
                        else
                        {
                            ShowErrorMessage(calculationResult._Result);
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }

            CalculateActionComplete();
        }

        public Utils.CalculationResult RunCalculation()
        {
            try
            {
                return _MainWindowModel.SelectedAlgorithm.Algorithm.Calculate(_MainWindowModel.Input);
            }
            catch (Exception ex)
            {
                return new Utils.CalculationResult(false, ex.Message);
            }
        }

        public bool ValidateCalculationStart()
        {
            try
            {
                if (_MainWindowModel.SelectedAlgorithm != null)
                {
                    if (_MainWindowModel.Input != null && _MainWindowModel.Input != "")
                    {
                        ValidationResult result = Utils.ValidateInput(_MainWindowModel.Input, _MainWindowModel.SelectedAlgorithm.ValidationRules);
                        if (result.IsValid)
                        {
                            ResetToReady();

                            return true;                            
                        }
                        else
                        {
                            ShowErrorMessage(result.ErrorContent.ToString());
                        }
                    }
                    else
                    {
                        ShowErrorMessage("No Input found.");
                    }
                }
                else
                {
                    ShowErrorMessage("No Algorithm selected.");
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }

            return false;
        }

        private void DisplayCalculationStarted()
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;

                SendStatusUpdate("Processing Algorithm");

                _MainWindowModel.Output = "";

                EnableInput(false);
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void ResetToReady()
        {
            try
            {
                SendStatusUpdate(Utils.Status_Ready);

                _MainWindowModel.Output = "";

                EnableInput();
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void CalculateActionComplete()
        {
            try
            {
                EnableInput();
                Mouse.OverrideCursor = Cursors.Arrow;
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        public void EnableInput(bool inputEnabled = true)
        {
            _MainWindowModel.InputEnabled = inputEnabled;
            _MainWindowModel.CalculateEnabled = inputEnabled;
        }
    }
}
