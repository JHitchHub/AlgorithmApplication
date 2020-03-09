using PrimeFactors.Resources;
using System.Globalization;
using System.Windows.Controls;

namespace PrimeFactors.Validators
{
    public class GreaterThanOneValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value is string)
            {
                string stringToValidate = value.ToString();

                if (string.IsNullOrWhiteSpace(stringToValidate))
                {
                    return new ValidationResult(true, "");
                }

                long intToValidate = 0;
                if (long.TryParse(stringToValidate, out intToValidate))
                {
                    if(intToValidate > 1)
                    {
                        return new ValidationResult(true, "");
                    }
                    else
                    {
                        return new ValidationResult(false, Utils.Message_GreaterThanOne);
                    }                    
                }

                return new ValidationResult(false, Utils.Message_Integer);
            }
            else if (value == null)
            {
                return new ValidationResult(true, "");
            }

            return new ValidationResult(false, Utils.Message_CouldNotValidate);
        }
    }
}
