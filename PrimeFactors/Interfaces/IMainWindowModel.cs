using PrimeFactors.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace PrimeFactors.Interfaces
{
    public interface IMainWindowModel
    {
        ObservableCollection<AlgorithmItemModel> Algorithms { get; set; }
        AlgorithmItemModel SelectedAlgorithm { get; set; }
        string Input { get; set; }
        string Output { get; set; }
        string Status { get; set; }
        bool InputEnabled { get; set; }
        bool CalculateEnabled { get; set; }

        event PropertyChangedEventHandler PropertyChanged;
    }
}
