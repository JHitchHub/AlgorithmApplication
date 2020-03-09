using PrimeFactors.Resources;
using System.Globalization;
using System.Windows.Controls;

namespace PrimeFactors.Validators
{
    public class IntOrDoubleValidationRule : ValidationRule
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
                    return new ValidationResult(true, "");
                }

                double dblToValidate = 0;
                if (double.TryParse(stringToValidate, out dblToValidate))
                {
                    return new ValidationResult(true, "");
                }

                return new ValidationResult(false, Utils.Message_IntegerOrDouble);
            }
            else if (value == null)
            {
                return new ValidationResult(true, "");
            }

            return new ValidationResult(false, Utils.Message_CouldNotValidate);
        }
    }
}
