using System.Collections.Generic;

namespace BlazorBifurcations.Calculations
{
    public class CalculationStepResults
    {
        public double Fertility { get; private set; }
        public List<double> StableValues { get; private set; }

        public CalculationStepResults(double fertility)
        {
            Fertility = fertility;
            StableValues = new List<double>();
        }
    }
}