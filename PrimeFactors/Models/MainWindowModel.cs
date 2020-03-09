using PrimeFactors.Interfaces;
using PrimeFactors.Resources;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace PrimeFactors.Models
{
    public class MainWindowModel : IMainWindowModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Action<PropertyChangedEventArgs> RaisePropertyChanged()
        {
            return args => PropertyChanged?.Invoke(this, args);
        }

        /// <summary>
        /// Algorithms is bound to the ItemSource property of the AlgorithmComboBox
        /// </summary>
        private ObservableCollection<AlgorithmItemModel> _Algorithms = new ObservableCollection<AlgorithmItemModel>();
        public ObservableCollection<AlgorithmItemModel> Algorithms
        {
            get { return _Algorithms; }
            set { this.MutateVerbose(ref _Algorithms, value, RaisePropertyChanged()); }
        }

        /// <summary>
        /// SelectedAlgorithm is bound to the SelectedItem property of the AlgorithmComboBox
        /// </summary>
        private AlgorithmItemModel _SelectedAlgorithm = default(AlgorithmItemModel);
        public AlgorithmItemModel SelectedAlgorithm
        {
            get { return _SelectedAlgorithm; }
            set { this.MutateVerbose(ref _SelectedAlgorithm, value, RaisePropertyChanged()); }
        }

        /// <summary>
        /// Input is bound to the text property of the Input TextBox
        /// The Input is checked against the algorithms ValidationRules as the user types
        /// </summary>
        private string _Input = default(string);
        public string Input
        {
            get { return _Input; }
            set { this.MutateVerbose(ref _Input, value, RaisePropertyChanged()); }
        }

        /// <summary>
        /// Output is bound to the text property of the Output TextBlock
        /// The Output TextBlock shows the output from the calculation, the result or error messages
        /// </summary>
        private string _Output = default(string);
        public string Output
        {
            get { return _Output; }
            set { this.MutateVerbose(ref _Output, value, RaisePropertyChanged()); }
        }

        /// <summary>
        /// Status is bound to the text property of the StatusTextBlock
        /// The Status TextBlock is used to display status messages to the user
        /// </summary>
        private string _Status = default(string);
        public string Status
        {
            get { return _Status; }
            set { this.MutateVerbose(ref _Status, value, RaisePropertyChanged()); }
        }

        /// <summary>
        /// InputEnabled is bound to the IsEnabled property of the InputTextBox and AlgorithmsComboBox
        /// </summary>
        private bool _InputEnabled = true;
        public bool InputEnabled
        {
            get { return _InputEnabled; }
            set { this.MutateVerbose(ref _InputEnabled, value, RaisePropertyChanged()); }
        }

        /// <summary>
        /// CalculateEnabled is bound to the IsEnabled property of the CalculateButton
        /// </summary>
        private bool _CalculateEnabled = true;
        public bool CalculateEnabled
        {
            get { return _CalculateEnabled; }
            set { this.MutateVerbose(ref _CalculateEnabled, value, RaisePropertyChanged()); }
        }
    }
}
