using PrimeFactors.Resources;
using System.Windows.Controls;
using System.Windows.Input;

namespace PrimeFactors.Interfaces
{
    public interface IMainWindowViewModel
    {
        void CalculateAction();
        IMainWindowModel _MainWindowModel { get; set; }
        void ShowErrorMessage(string message);
        bool ValidateCalculationStart();
        void EnableInput(bool inputEnabled = true);
    }
}
