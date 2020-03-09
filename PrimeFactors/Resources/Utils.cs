using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PrimeFactors.Resources
{
    public static class Utils
    {
        //Constant message text
        public const string Message_NonNegativeInteger = "Input must be a non-negative integer.";
        public const string Message_GreaterThanOne = "Input must be greater than 1.";
        public const string Message_Integer = "Input must be an integer.";
        public const string Message_IntegerOrDouble = "Input must be an integer or double.";
        public const string Message_CouldNotValidate = "Could not validate input.";
        public const string Status_Ready = "Ready";

        /// <summary>
        /// ValidateInput runs through the list of Validation Rules associated with the selected algorithm
        /// </summary>
        /// <param name="inputToValidate"></param>
        /// <param name="rules"></param>
        /// <returns></returns>
        public static ValidationResult ValidateInput(string inputToValidate, List<ValidationRule> rules = null)
        {
            foreach (ValidationRule rule in rules)
            {
                if (rule != null)
                {
                    ValidationResult result = rule.Validate(inputToValidate, null);
                    if (result != null)
                    {
                        if (!result.IsValid)
                        {
                            return result;
                        }
                    }
                }            
            }

            return new ValidationResult(true, "");
        }

        /// <summary>
        /// MoveToNextControl moves focus to the UIElement parameter or to the next control in the visual tree
        /// </summary>
        /// <param name="keyboardFocus"></param>
        public static void MoveToNextControl(UIElement keyboardFocus = null)
        {
            TraversalRequest tRequest = new TraversalRequest(FocusNavigationDirection.Next);
            if (keyboardFocus == null)
            {
                keyboardFocus = Keyboard.FocusedElement as UIElement;
            }

            if (keyboardFocus != null)
            {
                keyboardFocus.MoveFocus(tRequest);
            }

            keyboardFocus.Focus();
        }

        /// <summary>
        /// A simple struct to hold the results from the algorithms calculate method
        /// </summary>
        public struct CalculationResult
        {
            public CalculationResult(bool valid, string result)
            {
                _Valid = valid;
                _Result = result;
            }

            public bool _Valid { get; }
            public string _Result { get; }
        }

        /// <summary>
        /// MutateVerbose is a neat way of simplifying bound properties setter code.
        /// </summary>
        /// <typeparam name="TField"></typeparam>
        /// <param name="instance"></param>
        /// <param name="field"></param>
        /// <param name="newValue"></param>
        /// <param name="raise"></param>
        /// <param name="propertyName"></param>
        public static void MutateVerbose<TField>(this INotifyPropertyChanged instance, ref TField field, TField newValue, Action<PropertyChangedEventArgs> raise, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<TField>.Default.Equals(field, newValue)) return;
            field = newValue;
            raise?.Invoke(new PropertyChangedEventArgs(propertyName));
        }
    }
}
