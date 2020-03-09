using PrimeFactors.Algorithms;
using PrimeFactors.Interfaces;
using PrimeFactors.Models;
using PrimeFactors.Resources;
using PrimeFactors.ViewModels;
using PrimeFactors.Views;
using System;
using System.Linq;
using System.Windows;
using Unity;

namespace PrimeFactors
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        UnityContainer container;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            container = new UnityContainer();

            CreateAlgorithms();

            container.RegisterType<IMainWindowModel, MainWindowModel>();
            container.RegisterType<IMainWindowViewModel, MainWindowViewModel>();

            MainWindow mainWindow = container.Resolve<MainWindow>();
            mainWindow.Show();
        }

        private void CreateAlgorithms()
        {
            if (container != null)
            {
                MainWindowModel mainWindowModel = new MainWindowModel();

                //To create a new Algorithm, add a new class that implements the ICalculation interface to the Algorithms folder.
                //Create the methods required to compute your algorithm. Use the Calculate method to receive the input string and to return the output CalculationResult.
                //Once you've created your algorithm add it to the Algorithms collection below.
                //Validation should be included in your Algorithm class but you can also add ValidationRules to the collection. This validation
                //will be performed when the SelectedAlgorithm changes and when the Calculate button is clicked (or Enter/Return is pressed in the Input box)
                //WARNING: If no algorithms are added the application will run but an error message will be displayed and the inputs will be locked.
                mainWindowModel.Algorithms.Add(new AlgorithmItemModel(new PrimeFactorsAlgorithm())
                {
                    DisplayName = "Prime Factors",
                    HelpText = "Enter any positive integer that's greater than 1.",
                    ValidationRules = { new Validators.NonNegativeIntegerValidationRule(), new Validators.GreaterThanOneValidationRule() }
                });
                mainWindowModel.Algorithms.Add(new AlgorithmItemModel(new AddFiftyAlgorithm())
                {
                    DisplayName = "Add Fifty To Input",
                    HelpText = "Enter any integer or double.",
                    ValidationRules = { new Validators.IntOrDoubleValidationRule() }
                });

                //Select the first Algorithm.
                mainWindowModel.SelectedAlgorithm = mainWindowModel.Algorithms.FirstOrDefault();

                container.RegisterInstance(mainWindowModel);
            }
        }
    }
}
