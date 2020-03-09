using PrimeFactors.Interfaces;
using PrimeFactors.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;

namespace PrimeFactors.Models
{
    public class AlgorithmItemModel : INotifyPropertyChanged
    {
        /// <summary>
        /// For testing purposes only the Algorithm is required, so we pass it into the constructor and error if it's not sent
        /// </summary>
        /// <param name="algorithm"></param>
        public AlgorithmItemModel(ICalculation algorithm)
        {
            if (algorithm == null)
            {
                throw new ArgumentNullException(nameof(algorithm));
            }

            Algorithm = algorithm;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private Action<PropertyChangedEventArgs> RaisePropertyChanged()
        {
            return args => PropertyChanged?.Invoke(this, args);
        }

        /// <summary>
        /// DisplayName is bound to the DisplayMemberPath of the AlgorithmsComboBox
        /// The user will see a list of the created algorithms Display Names in the combobox
        /// </summary>
        private string _DisplayName = default(string);
        public string DisplayName
        {
            get { return _DisplayName; }
            set { this.MutateVerbose(ref _DisplayName, value, RaisePropertyChanged()); }
        }

        /// <summary>
        /// HelpText is bound to the text property of the HelpTextTextBlock
        /// HelpText is also bound to the ToolTip property of the InputTextBox
        /// HelpText should hold a brief description of the type of input the Algorithm requires
        /// All HelpTextTextBlock starts with 'In the Input box: ', so you can simply write: 'Enter a non-negative integer.' for example.
        /// </summary>
        private string _HelpText = default(string);
        public string HelpText
        {
            get { return _HelpText; }
            set { this.MutateVerbose(ref _HelpText, value, RaisePropertyChanged()); }
        }

        /// <summary>
        /// Algorithm holds the defined algorithm
        /// All algorithms must implement the ICalculation interface
        /// </summary>
        private ICalculation _Algorithm = default(ICalculation);
        public ICalculation Algorithm
        {
            get { return _Algorithm; }
            set { this.MutateVerbose(ref _Algorithm, value, RaisePropertyChanged()); }
        }

        /// <summary>
        /// ValidationRules holds a list of Validation Rules that the Input of the selected algorithm will be checked against
        /// These rules aren't bound, they are run as the input is entered
        /// </summary>
        public List<ValidationRule> ValidationRules { get; set; } = new List<ValidationRule>();
    }
}
