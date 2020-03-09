using PrimeFactors.Resources;
using System.Globalization;
using System.Windows.Controls;

namespace PrimeFactors.Validators
{
    public class NonNegativeIntegerValidationRule : ValidationRule
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

                long longToValidate = 0;
                if (long.TryParse(stringToValidate, out longToValidate))
                {
                    if (longToValidate >= 0)
                    {
                        return new ValidationResult(true, "");
                    }
                }

                return new ValidationResult(false, Utils.Message_NonNegativeInteger);
            }
            else if (value == null)
            {
                return new ValidationResult(true, "");
            }

            return new ValidationResult(false, Utils.Message_CouldNotValidate);
        }
    }
}
