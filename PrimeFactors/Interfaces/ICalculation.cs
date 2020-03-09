using PrimeFactors.Resources;

namespace PrimeFactors.Interfaces
{
    public interface ICalculation
    {
        Utils.CalculationResult Calculate(string input);
    }
}
