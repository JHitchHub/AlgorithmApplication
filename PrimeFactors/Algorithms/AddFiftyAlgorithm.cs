using PrimeFactors.Interfaces;
using PrimeFactors.Resources;

namespace PrimeFactors.Algorithms
{
    public class AddFiftyAlgorithm : ICalculation
    {
        public Utils.CalculationResult Calculate(string input)
        {
            int intToCalculate = 0;
            if (int.TryParse(input, out intToCalculate))
            {
                intToCalculate += 50;
                return new Utils.CalculationResult(true, intToCalculate.ToString());
            }
            else
            {
                double dblToCalculate = 0;
                if (double.TryParse(input, out dblToCalculate))
                {
                    dblToCalculate += 50;
                    return new Utils.CalculationResult(true, dblToCalculate.ToString());
                }
                else
                {
                    return new Utils.CalculationResult(false, Utils.Message_IntegerOrDouble);
                }
            }
        }
    }
}
